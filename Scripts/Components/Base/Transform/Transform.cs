using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class Transform : Component
    {
        Vector2 position;
        float radians;
        Vector2 scale = new Vector2(1,1);
        Transform parent;
        bool staticAttach;
        float radFromParent = 0f;
        float startingRotation = 0f;
        float distanceToParent = 0f;
        byte layer;

        public Vector2 Position { get {
                if (parent == null)
                    return position;
                else
                {
                    if (staticAttach)
                        return hf_Math.GetPosFromPoint(hf_Math.RadToDeg(parent.radians + radFromParent), distanceToParent, parent.position);
                    else
                        return position + parent.position;
                }

            } 
        }    
        public float Radians { get {
                if (parent == null)
                    return radians;
                else
                {
                    if (staticAttach)
                        return radians + parent.radians - startingRotation;
                    else
                        return radians;
                }
            }
            set { radians = value; }
        }
        public Vector2 Scale { get { return scale; } }
        public Transform Parent { get { return parent; } }
        public byte Layer { get { return layer; } set { layer = value; } }
        public Transform(GameObject go, Vector2 pos, float degrees, byte l) : base(go, "transform")
        {
            radians = hf_Math.DegToRad(degrees);
            position = pos;

            layer = l;
        }

        public void SetScale(float x, float y)
        {
            scale = new Vector2(x, y);
        }
        public void Move(Vector2 dist)
        {
            position += dist;
        }

        public void SetPosition(Vector2 pos)
        {
            position = pos;
        }
        public void Rotate(float degree)
        {
            radians += hf_Math.DegToRad(degree);
        }
        public void SetRotation(float degree)
        {
            radians = hf_Math.DegToRad(degree);
        }
        public Vector2 WorldPosition()
        {
            return Position * RenderingManager.GameScale * new Vector2(1,-1);
        }
        public void Attach(Transform t, bool isStatic)
        {
            staticAttach = isStatic;
            parent = t;
            startingRotation = t.radians;
            radFromParent = hf_Math.DegToRad(hf_Math.GetAngleDeg(t.position, position)) - t.radians;
            distanceToParent = Vector2.Distance(position, t.position);
            position = position - t.position;
        }
        public void Detach()
        {
            position = Position;
            radians = Radians;
            parent = null;
            radFromParent = 0;
            startingRotation = 0;          
        }
    }
}
