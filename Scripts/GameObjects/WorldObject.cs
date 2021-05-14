using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    public class WorldObject : GameObject
    {
        public RigidBody RigidBody { get { return (RigidBody)componentHandler.GetComponent("rigidBody"); } }
        public Transform Transform { get { return (Transform)componentHandler.GetComponent("transform"); } }
        public SpriteRenderer SpriteRenderer{ get { return (SpriteRenderer)componentHandler.GetComponent("spriteRenderer"); } }
        public CollisionHandler CollisionHandler { get { return (CollisionHandler)componentHandler.GetComponent("collisionHandler"); } }
        public WorldObject(string texID, string tag, Vector2 size, Vector2 pos, byte layer) : base(tag)
        {           
            componentHandler.AddComponent(new CollisionHandler(0, this));           
            componentHandler.AddComponent(new Transform(0, pos, size.X, size.Y, 0, layer));
            componentHandler.AddComponent(new RigidBody(this, RigidBody.RigidBodyType.Static, 0));
            componentHandler.AddComponent(new SpriteRenderer(texID,
                                            Transform,
                                            new Vector2(0, 0),
                                            size,
                                            0,
                                            0,
                                            0));
    }

        public override void Initilize()
        {
            base.Initilize();
        }

        public override void Update(float gt)
        {
            base.Update(gt);
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
