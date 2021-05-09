using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Diagnostics;
using System.Linq;


namespace MonoGame_Core.Scripts
{
    public static class CollisionManager
    {
        public enum CollisionType { AABB, SAT, TileMapFree, TileMap, IsometricFree, Isometric }
        public static CollisionType CollisionDetection = CollisionType.SAT;

        public static List<Collider> ActiveStaticColliders;
        public static List<Collider> ActiveMovingColliders;
        public static bool[,,] TileMap;
        public static Vector2 TileSize;
        public static void Initilize()
        {
            Clear();
        }

        public static void Clear()
        {
            ActiveStaticColliders = new List<Collider>();
            ActiveMovingColliders = new List<Collider>();
        }

        public static void AddStaticCollider(Collider c)
        {
            ActiveStaticColliders.Add(c);
        }

        public static void AddMovingCollider(Collider c)
        {
            ActiveMovingColliders.Add(c);
        }

        static bool AABBCollision(CollisionBox b1, CollisionBox b2, out Vector2 p)
        {
            p = new Vector2();

            foreach(Vector2 v in b1.Verticies())
            if (v.X > b2.TopLeft().X
                && v.X < b2.TopRight().X
                && v.Y > b2.TopRight().Y
                && v.Y < b2.BottomLeft().Y)
                {
                    if (b1.Transform.Position.X > b2.Transform.Position.X)
                        p.X = b1.Transform.Position.X - b2.Transform.Position.X;
                    else
                        p.X = b2.Transform.Position.X - b1.Transform.Position.X;

                    if (b1.Transform.Position.Y > b2.Transform.Position.Y)
                        p.X = b1.Transform.Position.Y - b2.Transform.Position.Y;
                    else
                        p.X = b2.Transform.Position.Y - b1.Transform.Position.Y;

                    return true;
                }



            return false;
        }

        static bool SphereToBoxCollision(CollisionSphere s1, CollisionBox b2, out Vector2 p)
        {
            p = new Vector2();
            return false;
        }
        static bool distanceHuristic(Collider b1, Collider b2)
        {
            if (Vector2.Distance(b1.Transform.Position, b2.Transform.Position) > b1.Radius + b2.Radius)
                return false;
            else return true;
        }
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
            ActiveMovingColliders.Clear();
            ActiveStaticColliders.Clear();
        }

        private static void PerformSATCollision()
        {
            foreach (Camera c in CameraManager.Cameras)
            {
                IEnumerable<Collider> ab = ActiveMovingColliders;
                IEnumerable<Collider> sb = ActiveStaticColliders;
                Vector2 p = new Vector2();

                foreach (Collider a in ab)
                {
                    sb = ActiveStaticColliders.OrderBy(s => Vector2.Distance(s.Transform.Position, a.Transform.Position))
                        .Where(s => s.Transform.Layer == a.Transform.Layer)
                        .Where(s => Vector2.Distance(s.Transform.Position, a.Transform.Position) < s.Transform.Radius + a.Transform.Radius);

                    ab = ActiveMovingColliders.OrderBy(s => Vector2.Distance(s.Transform.Position, a.Transform.Position))
                        .Where(s => s.Transform.Layer == a.Transform.Layer)
                        .Where(s => Vector2.Distance(s.Transform.Position, a.Transform.Position) < s.Transform.Radius + a.Transform.Radius);

                    for (int t = 0; t < 4; ++t)
                    {
                        foreach (Collider s in ab)
                        {
                            if (a.GameObject != s.GameObject)
                            {
                                if (distanceHuristic(a, s))
                                {
                                    if (SATcollision(a, s, out p))
                                    {
                                        ((CollisionHandler)a.GameObject.ComponentHandler.GetComponent("collisionHandler")).RunCollisionActions(a, s, p);
                                        p = new Vector2();
                                    }
                                }
                            }
                        }

                        foreach (Collider s in sb)
                        {
                            if (a.GameObject != s.GameObject)
                            {
                                if (distanceHuristic(a, s))
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
            }
        }

        private static void PerformFreeTileCollision()
        {
            Vector2 p = new Vector2();
            foreach(Collider c in ActiveMovingColliders)
            {
                Vector2 pos = c.Transform.Position + c.Offset;
                Vector2 gridPos = new Vector2((pos.X + TileSize.X / 2 + TileMap.GetUpperBound(0) * TileSize.X / 2)/TileSize.X, (pos.Y + TileSize.Y / 2 + TileMap.GetUpperBound(1) * TileSize.Y / 2) / TileSize.Y);

                List<Vector2> collisionChecks = new List<Vector2>();
                for (int y = (int)gridPos.Y - 1; y <= (int)gridPos.Y + 1; y++)//check surrounding tiles on y axis
                    if (y >= 0 && y < TileMap.GetUpperBound(1))//if they are within the tilemap
                        for (int x = (int)gridPos.X - 1; x <= (int)gridPos.X + 1; x++)//and the surrounding tiles on the x axis
                            if (x >= 0 && x < TileMap.GetUpperBound(0))//that are within the tile map
                                if (TileMap[x, y, c.Transform.Layer] == true)//if there is collision there
                                {
                                    collisionChecks.Add(new Vector2(TileSize.X * x - TileSize.X / 2 - TileSize.X * TileMap.GetUpperBound(0) / 2, TileSize.Y * y - TileSize.Y / 2 - TileSize.Y * TileMap.GetUpperBound(1) / 2));
                                }

                IEnumerable<Vector2> cc = collisionChecks.OrderBy(s => Vector2.Distance(s, c.Transform.Position));

                foreach(Vector2 v in cc)
                {
                    //create a collision box
                    CollisionBox cb = new CollisionBox(new Transform(0, v, TileSize.X, TileSize.Y, 0, c.Transform.Layer), "TileWall"); ;
                    //test collision against it
                    if(SATcollision(c, cb, out p))
                        ((CollisionHandler)c.GameObject.ComponentHandler.GetComponent("collisionHandler")).RunCollisionActions(c, cb, p);
                }
            }
        }
    }
}
