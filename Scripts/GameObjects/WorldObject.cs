using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Core.Scripts
{
    public class WorldObject : GameObject
    {
        string tag;

        public string Tag { get { return tag; } }
        public RigidBody RigidBody { get { return (RigidBody)componentHandler.GetComponent("rigidBody"); } }
        public Transform Transform { get { return (Transform)componentHandler.GetComponent("transform"); } }
        public SpriteRenderer SpriteRenderer{ get { return (SpriteRenderer)componentHandler.GetComponent("spriteRenderer"); } }
        public CollisionHandler CollisionHandler { get { return (CollisionHandler)componentHandler.GetComponent("collisionHandler"); } }
        public WorldObject(string texID, string tag) : base(tag)
        {
            this.tag = tag;
            //componentHandler.AddComponent(new CollisionHandler(0, this));           
            componentHandler.AddComponent(new Transform(0, new Vector2(0,0), 0, 0, 0));
            componentHandler.AddComponent(new RigidBody(this, RigidBody.RigidBodyType.Dynamic, 0));
            componentHandler.AddComponent(new SpriteRenderer(texID,
                                            (Transform)componentHandler.GetComponent("transform"),
                                            new Vector2(0, 0),
                                            new Vector2(0, 0),
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
