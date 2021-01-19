using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Diagnostics;

namespace MonoGame_Core.Scripts
{
    public static class CollisionManager
    {
        public static List<CollisionHandler> collisionHandlers;

        public static void Initilize()
        {

        }

        static bool CheckCollision(CollisionBox b1, CollisionBox b2)
        {
            Transform t1 = ((WorldObject)b1.MyObject).Transform;
            RigidBody rb1 = ((WorldObject)b1.MyObject).RigidBody;

            Transform t2 = ((WorldObject)b2.MyObject).Transform;
            RigidBody rb2 = ((WorldObject)b2.MyObject).RigidBody;



            return false;
        }
/*
        public static bool SATcollision(CollisionBox boxOne, CollisionBox boxTwo, out Vector2 penitrationVector)
        {
            //find the rotated vectors to use for dot products and put them in a list
            //use a radius of 1 for a unit circle
            List<Vector3> linesToCheck = new List<Vector3>();
            linesToCheck.Add(boxOne.getRotationPosition(boxOne.rotationInDegrees, 1, new Vector2(0, 0)));
            linesToCheck.Add(boxOne.getRotationPosition(boxOne.rotationInDegrees + 90, 1, new Vector2(0, 0)));//add 90 because rectangles have only right angles and you need the next line's vector

            linesToCheck.Add(boxTwo.getRotationPosition(boxTwo.rotationInDegrees, 1, new Vector2(0, 0)));
            linesToCheck.Add(boxTwo.getRotationPosition(boxTwo.rotationInDegrees + 90, 1, new Vector2(0, 0)));

            float minPenValue = float.MaxValue;//used to find the depth of penetration collision happens at. 
                                               //if all points overlap (i.e. they collide) the smallest value is the penetration amount.
            Vector3[] boxOneVectors = new Vector3[] { boxOne.topLeft(), boxOne.topRight(), boxOne.bottomLeft(), boxOne.bottomRight() };
            Vector3[] boxTwoVectors = new Vector3[] { boxTwo.topLeft(), boxTwo.topRight(), boxTwo.bottomLeft(), boxTwo.bottomRight() };

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
            foreach (Vector3 line in linesToCheck)
            {
                //set min and max values to thier opposing values so that any value will be larger or smaller respectively
                boxOneMin = float.MaxValue;
                boxOneMax = float.MinValue;
                boxTwoMin = float.MaxValue;
                boxTwoMax = float.MinValue;

                //find the smallest and largest dot product for both boxs
                foreach (Vector3 point in boxOneVectors)
                {
                    boxOneMin = Math.Min(boxOneMin, Vector3.Dot(line, point));
                    boxOneMax = Math.Max(boxOneMax, Vector3.Dot(line, point));
                }
                foreach (Vector3 point in boxTwoVectors)
                {
                    boxTwoMin = Math.Min(boxTwoMin, Vector3.Dot(line, point));
                    boxTwoMax = Math.Max(boxTwoMax, Vector3.Dot(line, point));
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
                                                                       //add .00001 to counteract inconsistantcy in floating point rounding
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
*/
        public static void Update(float gt)
        {

        }
    }
}
