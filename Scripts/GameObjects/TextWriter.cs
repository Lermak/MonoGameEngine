using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class TextWriter : GameObject
    {

        public Transform Transform { get { return (Transform)componentHandler.GetComponent("transform"); } }
        public FontRenderer FontRenderer { get { return (FontRenderer)componentHandler.GetComponent("fontRenderer"); } }
        public TextWriter(string fontID, string tag, string text, Vector2 pos, Vector2 size, Color c, byte layer) : base(tag)
        {
            componentHandler.AddComponent(new Transform(this, 0, pos, size.X, size.Y, 0, layer));
            componentHandler.AddComponent(new FontRenderer(this, 
                                            text,
                                            fontID,
                                            Transform,
                                            new Vector2(0, 0),
                                            size,
                                            0,
                                            c,
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
    }
}
