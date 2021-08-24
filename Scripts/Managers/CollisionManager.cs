using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Diagnostics;
using System.Linq;


namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// Maintains a collection of moving and static colliders
    /// moving colliders check collision with all other colliders
    /// static colliders don't check collision
    /// When collision is detected, a collision event is posted to the detecting object's collisionHandler component
    /// </summary>
    public static class CollisionManager
    {
        //CollisionType will determine what collision detection method to utilize
        public enum CollisionType { AABB, SAT, TileMapFree, IsometricFree }
        public static CollisionType CollisionDetection = CollisionType.SAT;

        public static Quadtree ActiveColliders;
        public static Quadtree PassiveColliders;

        //These can be vistigial variabls in a non-tile based game. 
        public static bool[,,] TileMap;
        public static Vector2 TileSize;

        public static void Initilize()
        {
            ActiveColliders = new Quadtree(new Rectangle(-(int)SceneManager.CurrentScene.Size.X / 2, -(int)SceneManager.CurrentScene.Size.Y / 2, (int)SceneManager.CurrentScene.Size.X, (int)SceneManager.CurrentScene.Size.Y), null);
            PassiveColliders = new Quadtree(new Rectangle(-(int)SceneManager.CurrentScene.Size.X / 2, -(int)SceneManager.CurrentScene.Size.Y / 2, (int)SceneManager.CurrentScene.Size.X, (int)SceneManager.CurrentScene.Size.Y), null);
        }

        /// <summary>
        /// Remove all elements from the moving and static collider lists
        /// </summary>
        public static void Clear()
        {
            ActiveColliders = new Quadtree(new Rectangle(-(int)SceneManager.CurrentScene.Size.X / 2, -(int)SceneManager.CurrentScene.Size.Y / 2, (int)SceneManager.CurrentScene.Size.X, (int)SceneManager.CurrentScene.Size.Y), null);
            PassiveColliders = new Quadtree(new Rectangle(-(int)SceneManager.CurrentScene.Size.X / 2, -(int)SceneManager.CurrentScene.Size.Y / 2, (int)SceneManager.CurrentScene.Size.X, (int)SceneManager.CurrentScene.Size.Y), null);
        }

        /// <summary>
        /// Collision for Axis Aligned Bounding Box collision detection
        /// </summary>
        /// <param name="b1">The current detecting collider</param>
        /// <param name="b2">The collider to compair against</param>
        /// <param name="p">The vector of penitration</param>
        /// <returns>true when collision has occured</returns>
        static bool AABBCollision(CollisionBox b1, CollisionBox b2, out Vector2 p)
        {
            p = new Vector2();

            foreach(Vector2 v in b1.Verticies())
            if (v.X > b2.TopLeft().X
                && v.X < b2.TopRight().X
                && v.Y < b2.TopRight().Y
                && v.Y > b2.BottomLeft().Y)
                {
                    if (b1.Transform.Position.X > b2.Transform.Position.X)
                        p.X = b1.Transform.Position.X - b2.Transform.Position.X;
                    else
                        p.X = b2.Transform.Position.X - b1.Transform.Position.X;

                    if (b1.Transform.Position.Y < b2.Transform.Position.Y)
                        p.Y = b1.Transform.Position.Y - b2.Transform.Position.Y;
                    else
                        p.Y = b2.Transform.Position.Y - b1.Transform.Position.Y;

                    return true;
                }

            return false;
        }

        /// <summary>
        /// Determines if a sphere and box are overlaping
        /// </summary>
        /// <param name="s1">The detecting sphere</param>
        /// <param name="b2">The box to check against</param>
        /// <param name="p">The penetration vector</param>
        /// <returns>true when collision occurs</returns>
        static bool SphereToBoxCollision(CollisionSphere s1, CollisionBox b2, out Vector2 p)
        {
            p = new Vector2();
            return false;
        }

        /// <summary>
        /// Determine if two objects are close enough to have collision be possible
        /// </summary>
        /// <param name="b1">Collision Box 1</param>
        /// <param name="b2">Collision Box 2</param>
        /// <returns>true if collision is possible</returns>
        static bool distanceHuristic(Collider b1, Collider b2)
        {
            if (Vector2.Distance(b1.Transform.Position, b2.Transform.Position) > b1.Radius + b2.Radius)
                return false;
            else return true;
        }

        /// <summary>
        /// Perform Seperating Axis Theorum collision detection
        /// </summary>
        /// <param name="c1">Current checking collider</param>
        /// <param name="c2">Collider to check against</param>
        /// <param name="penitrationVector">The penetration vector</param>
        /// <returns>true if collision occurs</returns>
        public static bool SATcollision(Collider c1, Collider c2, out Vector2 penitrationVector)
        {
            //find the rotated vectors to use for dot products and put them in a list
            //use a radius of 1 for a unit circle
            List<Vector2> axies = new List<Vector2>();
            axies.AddRange(c1.Axies());
            axies.AddRange(c2.Axies());

            float minPenValue = float.MaxValue;//used to find the depth of penetration collision happens at. 
                                               //if all points overlap (i.e. they collide) the smallest value is the penetration amount.
            penitrationVector = new Vector2();

            //variables to hold the min and max points of each box on each angle
            float boxOneMin;
            float boxOneMax;
            float boxTwoMin;
            float boxTwoMax;

            float floatEpsilon = .01f;

            //check each vector in the list to each point(vector) of both boxs
            //make a min and max value for each box's dot product
            //check and see if those lines overlap
            foreach (Vector2 line in axies)
            {
                //set min and max values to thier opposing values so that any value will be larger or smaller respectively
                boxOneMin = float.MaxValue;
                boxOneMax = float.MinValue;
                boxTwoMin = float.MaxValue;
                boxTwoMax = float.MinValue;

                //find the smallest and largest dot product for both boxs
                foreach (Vector2 point in c1.Verticies())
                {
                    boxOneMin = Math.Min(boxOneMin, Vector2.Dot(line, point));
                    boxOneMax = Math.Max(boxOneMax, Vector2.Dot(line, point));
                }
                foreach (Vector2 point in c2.Verticies())
                {
                    boxTwoMin = Math.Min(boxTwoMin, Vector2.Dot(line, point));
                    boxTwoMax = Math.Max(boxTwoMax, Vector2.Dot(line, point));
                }

                //check if either line's min or max is contained in the other
                if (!(
                    (boxOneMin >= boxTwoMin && boxOneMin <= boxTwoMax) ||
                    (boxOneMax <= boxTwoMax && boxOneMax >= boxTwoMin) ||
                    (boxTwoMin >= boxOneMin && boxTwoMin <= boxOneMax) ||
                    (boxTwoMax <= boxOneMax && boxTwoMax >= boxOneMin)
                    ))
                {
                    penitrationVector = new Vector2();
                    return false;//if any of these are untrue collision doesn't occur
                }

                //check if this is the smallest amount of overlap
                //if it is make this line the penitration vector and change minPenValue to the new penitration distance
                if ((boxOneMax - boxTwoMin) + floatEpsilon < minPenValue)
                {
                    penitrationVector = -line;
                    minPenValue = boxOneMax - boxTwoMin + floatEpsilon;//based on what side is overlaping you will get different values
                                                                       //add .01 to counteract inconsistantcy in floating point rounding
                }
                if ((boxTwoMax - boxOneMin) + floatEpsilon < minPenValue)
                {
                    penitrationVector = line;
                    minPenValue = boxTwoMax - boxOneMin + floatEpsilon;
                }

            }
            penitrationVector *= minPenValue;//multiply the PenValue to the vector to get how far in what direction collision occured
            return true;
        }

        /// <summary>
        /// Perform collision detection for all currently moving colliders
        /// Type of collision detection is determined by CollisionType
        /// </summary>
        /// <param name="gt">Game Time</param>
        public static void Update(float gt)
        {
            if (CollisionDetection == CollisionType.SAT)
            {
                PerformSATCollision();
            }
            else if(CollisionDetection == CollisionType.TileMapFree)
            {
                PerformFreeTileCollision();
            }
            //ActiveMovingColliders.Clear();
            //ActiveStaticColliders.Clear();
            ActiveColliders = new Quadtree(new Rectangle(-(int)SceneManager.CurrentScene.Size.X / 2, -(int)SceneManager.CurrentScene.Size.Y / 2, (int)SceneManager.CurrentScene.Size.X, (int)SceneManager.CurrentScene.Size.Y), null);
        }

        /// <summary>
        /// Performs the collision detection on all moving colliders utilizing the SATCollision method
        /// </summary>
        private static void PerformSATCollision()
        {
            Vector2 p = new Vector2();

            foreach (Collider a in ActiveColliders.GetColliders())
            {
                Rectangle r = new Rectangle(new Point((int)(a.Transform.Position.X - a.Transform.Width / 2), (int)(a.Transform.Position.Y - a.Transform.Height / 2)), new Point((int)a.Transform.Width, (int)a.Transform.Height));
                List<Quadtree> quads = PassiveColliders.GetQuads(r);
                List<Quadtree> aQuads = ActiveColliders.GetQuads(r);

                List<Collider> quadTreeColliders = new List<Collider>();
                foreach (Quadtree q in quads)
                {
                    quadTreeColliders.AddRange(q.GetColliders());
                }
                foreach (Quadtree q in aQuads)
                {
                    quadTreeColliders.AddRange(q.GetColliders());
                }

                IEnumerable<Collider> toCheck = quadTreeColliders.Where(c => c.Transform.Layer == a.Transform.Layer)
                    .Where(c => distanceHuristic(c, a) == true)
                    .OrderBy(c => Vector2.Distance(a.Transform.Position, c.Transform.Position));

                for (int t = 0; t < 4; ++t)
                {
                    foreach (Collider s in toCheck)
                    {
                        if (a.GameObject != s.GameObject)
                        {
                            if (SATcollision(a, s, out p))
                            {
                                ((CollisionHandler)a.GameObject.ComponentHandler.GetComponent("collisionHandler")).RunCollisionActions(a, s, p);
                                p = new Vector2();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Performs collision detection with the tilemap for any moving objects
        /// This collision should only be used if objects are not constrained to move by the tilemap grid, but can move freely
        /// </summary>
        private static void PerformFreeTileCollision()
        {
            Vector2 p = new Vector2();
            foreach(Collider c in ActiveColliders.GetColliders())
            {
                Vector2 pos = c.Transform.Position + c.Offset;
                Vector2 gridPos = new Vector2((pos.X + TileSize.X / 2 + TileMap.GetUpperBound(0) * TileSize.X / 2)/TileSize.X, (TileMap.GetUpperBound(1) * TileSize.Y / 2 - pos.Y + TileSize.Y / 2) / TileSize.Y);

                List<Vector2> collisionChecks = new List<Vector2>();
                for (int y = (int)gridPos.Y - 1; y <= (int)gridPos.Y + 1; y++)//check surrounding tiles on y axis
                    if (y >= 0 && y < TileMap.GetUpperBound(1))//if they are within the tilemap
                        for (int x = (int)gridPos.X - 1; x <= (int)gridPos.X + 1; x++)//and the surrounding tiles on the x axis
                            if (x >= 0 && x < TileMap.GetUpperBound(0))//that are within the tile map
                                if (TileMap[x, y, c.Transform.Layer] == true)//if there is collision there
                                {
                                    collisionChecks.Add(new Vector2(TileSize.X * x - TileSize.X / 2 - TileSize.X * TileMap.GetUpperBound(0) / 2, TileSize.Y * TileMap.GetUpperBound(1) / 2 - TileSize.Y * y + TileSize.Y / 2));
                                }

                //perform collision resolution in order of closest to furthest
                IEnumerable<Vector2> cc = collisionChecks.OrderBy(s => Vector2.Distance(s, c.Transform.Position));

                foreach(Vector2 v in cc)
                {
                    GameObject go = new GameObject("TileWall");
                    //create a collision box
                    CollisionBox cb = new CollisionBox(go, new Transform(go, 0, v, TileSize.X, TileSize.Y, 0, c.Transform.Layer), "TileWall");
                    //test collision against it
                    if(SATcollision(c, cb, out p))
                        ((CollisionHandler)c.GameObject.ComponentHandler.GetComponent("collisionHandler")).RunCollisionActions(c, cb, p);
                }
            }
        }
    }
}
