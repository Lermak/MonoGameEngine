using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGame_Core.Scripts
{
    public class Score : TextWriter
    {
        public int Points = 0;
        public Score(string fontID, string tag, Vector2 position, Vector2 size) : base(fontID, tag, "", position, size, Color.White)
        {

        }

        public override void Update(float gt)
        {
            ((FontRenderer)componentHandler.GetComponent("fontRenderer")).Text = "Score: " + Points;
            base.Update(gt);
        }
    }
}
