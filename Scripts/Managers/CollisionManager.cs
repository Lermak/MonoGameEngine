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
        public enum CollisionType { AABB, SAT, TileMap }
        public static CollisionType CollisionDetection = CollisionType.SAT;

        public static List<Collider> ActiveStaticColliders;
        public static List<Collider> ActiveMovingColliders;
        public static bool[,,] TileMap;
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

        static bool AABBCollision(CollisionBox b1, CollisionBox b2)
        {
            Transform t1 = ((WorldObject)b1.GameObject).Transform;
            RigidBody rb1 = ((WorldObject)b1.GameObject).RigidBody;

            Transform t2 = ((WorldObject)b2.GameObject).Transform;
            RigidBody rb2 = ((WorldObject)b2.GameObject).RigidBody;



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
                foreach (Camera c in CameraManager.Cameras)
                {
                    IEnumerable<Collider> ab = ActiveMovingColliders;
                    IEnumerable<Collider> sb = ActiveStaticColliders;
                    Vector2 p = new Vector2();

                    foreach(Collider a in ab)
                    {
                        sb = sb.OrderBy(s => Vector2.Distance(s.Transform.Position, a.Transform.Position))
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
            ActiveMovingColliders.Clear();
            ActiveStaticColliders.Clear();
        }
    }
}
