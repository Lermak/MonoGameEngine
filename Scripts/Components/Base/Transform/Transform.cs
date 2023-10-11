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
                    float f = parent.Scale.Length();
                    if (staticAttach)
                        return hf_Math.GetPosFromPoint(hf_Math.RadToDeg(parent.radians + radFromParent), distanceToParent * parent.Scale.X, parent.position);
                    else
                        return (position * parent.Scale) + parent.position;
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
            set { radians = value % hf_Math.DegToRad(360); }
        }
        public float RotationDegrees { get { return hf_Math.RadToDeg(Radians); } }
        public Vector2 Scale { get { 
                if(parent == null)
                    return scale; 
                else
                {
                    return scale * parent.Scale;
                }
            }
            set { scale = value; }
        }
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
            radians = (radians + hf_Math.DegToRad(degree)) % hf_Math.DegToRad(360);
        }
        public void SetRotation(float degree)
        {
            radians = hf_Math.DegToRad(degree) % hf_Math.DegToRad(360);
        }
        public Vector2 WorldPosition()
        {
            return Position * RenderingManager.GameScale * new Vector2(1,-1);
        }
        public void Attach(Transform t, bool isStatic)
        {
            staticAttach = isStatic;
            startingRotation = t.radians;
            radFromParent = hf_Math.GetAngleRad(t.position, position) - t.radians;
            distanceToParent = Vector2.Distance(position, t.position);
            position = position - t.position;
            scale = scale / t.Scale;
            parent = t;
        }
        public void Detach()
        {
            position = Position;
            radians = Radians;
            scale = Scale;
            parent = null;
            radFromParent = 0;
            startingRotation = 0;
        }

        public Vector2 GetReletivePosition()
        {
            return position;
        }
    }
}
