using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public class WorldObject : GameObject
    {
        string tag;
        public CollisionHandler CollisionHandler;
        public Transform Transform { get { return ((Transform)componentHandler.Components["transform"]); } }
        public SpriteRenderer SpriteRenderer{ get { return ((SpriteRenderer)componentHandler.Components["spriteRenderer"]); } }
        public WorldObject(string texID, string tag) : base(tag)
        {
            CollisionHandler = new CollisionHandler(this);
            this.tag = tag;
            componentHandler.AddComponent("transform", new Transform(0, new Vector2(0,0)));
            componentHandler.AddComponent("spriteRenderer", new SpriteRenderer(texID,
                                                            (Transform)componentHandler.Components["transform"],
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

        public override void Update(GameTime gt)
        {
            CollisionHandler.Update(gt);
            base.Update(gt);
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
