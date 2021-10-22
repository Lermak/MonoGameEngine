using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class TextWriter : GameObject
    {

        public Transform Transform { get { return (Transform)componentHandler.GetComponent("transform"); } }
        public FontRenderer FontRenderer { get { return (FontRenderer)componentHandler.GetComponent("fontRenderer"); } }
        public TextWriter(string fontID, string name, string[] tags, string text, Vector2 pos, Vector2 size, Color c, byte layer) : base(name, tags)
        {
            componentHandler.AddComponent(new Transform(this, pos, size.X, size.Y, 0, layer));
            componentHandler.AddComponent(new FontRenderer(this, 
                                            text,
                                            fontID,
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
