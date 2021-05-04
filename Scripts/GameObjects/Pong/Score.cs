using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class Score : WorldObject
    {
        public int Points = 0;
        public Score(string texID, string tag, Vector2 position, Vector2 size) : base(texID, tag, size)
        {
            componentHandler.RemoveComponent("spriteRenderer");
            componentHandler.AddComponent(new FontRenderer("test", "fontRenderer", "BasicFont", Transform, new Vector2(), new Vector2(Transform.Width, Transform.Height), 2, 0, Color.White, 0));
            Transform.Place(position);
        }

        public override void Update(float gt)
        {
            ((FontRenderer)componentHandler.GetComponent("fontRenderer")).Text = "Score: " + Points;
            base.Update(gt);
        }
    }
}
