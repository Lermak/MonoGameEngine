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
        public WorldObject(string texID, string name, string[] tags, Vector2 pos, byte layer) : base(name, tags)
        {           
            componentHandler.Add(new CollisionHandler(this));           
            componentHandler.Add(new Transform(this, pos, 0, layer));
            componentHandler.Add(new RigidBody(this, RigidBody.RigidBodyType.Dynamic));
            componentHandler.Add(new SpriteRenderer(this, 
                                            texID,
                                            0));
        }
        public WorldObject(string texID, string name, string[] tags, Vector2 pos, byte layer, Vector2 drawArea) : base(name, tags)
        {
            componentHandler.Add(new CollisionHandler(this));
            componentHandler.Add(new Transform(this, pos, 0, layer));
            componentHandler.Add(new RigidBody(this, RigidBody.RigidBodyType.Dynamic));
            componentHandler.Add(new SpriteRenderer(this,
                                            texID,
                                            0,
                                            drawArea));
        }

        public void AddCollision(CollisionActions ca)
        {
            this.CollisionHandler.myActions.Add(ca);
        }

        public virtual WorldObject AddChild(WorldObject wo, bool AttachTransform, bool AttachStatic=true)
        {
            if (AttachTransform)
            {
                wo.Transform.Attach(Transform, AttachStatic);
            }
            return (WorldObject)base.AddChild(wo);
        }
        public virtual WorldObject SetParent(WorldObject wo, bool AttachTransform, bool AttachStatic = true)
        {
            if (AttachTransform)
            {
                Transform.Attach(wo.Transform, AttachStatic);
            }
            return (WorldObject)wo.AddChild(wo);
        }
        public virtual WorldObject RemoveChild(WorldObject wo)
        {
            if(wo.Transform.Parent == Transform)
            {
                wo.Transform.Detach();
            }
            return (WorldObject)base.RemoveChild(wo);
        }
        public virtual WorldObject RemoveParent()
        {
            Transform.Detach();       
            return (WorldObject)base.RemoveChild(this);
        }
    }
}
