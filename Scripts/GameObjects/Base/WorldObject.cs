﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    public class WorldObject : GameObject
    {
        public RigidBody RigidBody { get { return (RigidBody)componentHandler.Get("rigidBody"); } }
        public Transform Transform { get { return (Transform)componentHandler.Get("transform"); } }
        public SpriteRenderer SpriteRenderer{ get { return (SpriteRenderer)componentHandler.Get("spriteRenderer"); } }
        public CollisionHandler CollisionHandler { get { return (CollisionHandler)componentHandler.Get("collisionHandler"); } }
        public WorldObject(string texID, string name, string[] tags, Vector2 size, Vector2 pos, byte layer) : base(name, tags)
        {           
            componentHandler.Add(new CollisionHandler(this));           
            componentHandler.Add(new Transform(this, pos, size.X, size.Y, 0, layer));
            componentHandler.Add(new RigidBody(this, RigidBody.RigidBodyType.Static));
            componentHandler.Add(new SpriteRenderer(this, 
                                            texID,
                                            new Vector2(0, 0),
                                            size,
                                            0));
        }

        public override void Initilize()
        {
            base.Initilize();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }
    }
}
