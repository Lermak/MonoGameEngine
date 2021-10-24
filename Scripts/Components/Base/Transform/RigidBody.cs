using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class RigidBody : Component
    {
        public enum RigidBodyType { Static, Dynamic }

        Transform transform;
        RigidBodyType bodyType;
        float angularVelocity;
        Vector2 moveVelocity;
        float mass;

        public Transform Transform { get { return transform; } }
        public RigidBodyType BodyType { get { return bodyType; } }
        public float AngularVelocity { get { return angularVelocity; } set { angularVelocity = value; } }
        public Vector2 MoveVelocity { get { return moveVelocity; } set { moveVelocity = value; } }
        public float Mass { get { return mass; } }
        public float Force { get { return mass * moveVelocity.Length(); } }

        public RigidBody(GameObject go, RigidBodyType rbt) : base(go, "rigidBody")
        {
            transform = (Transform)go.ComponentHandler.Get("transform");
            bodyType = rbt;
            angularVelocity = 0;
            moveVelocity = new Vector2(0, 0);
            mass = 0;

            gameObject.AddBehavior("rigidBody", Update);
        }

        private static void Update(float gt, GameObject go, Component[] c)
        {
            Transform t = (Transform)go.GetComponent("transform");
            RigidBody rb = (RigidBody)go.GetComponent("rigidBody");
            t.Move(rb.MoveVelocity);
            t.Rotate(rb.AngularVelocity);
        }
    }
}
