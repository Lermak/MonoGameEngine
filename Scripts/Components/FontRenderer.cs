using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class FontRenderer : SpriteRenderer
    {
        private string text;
        float textScale = 5;
        public string Text { get { return text; } set { text = value; } }
        public override string Texture
        {
            get { return texture; }
            set
            {
                if (SceneManager.CurrentScene.Fonts.ContainsKey(value))
                    texture = value;
                else
                    texture = null;
            }
        }
        public FontRenderer(string text, string name, string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, Color clr, int frames, int uo) : base(name, texID, t, off, drawArea, orderInLayer, clr, frames, uo)
        {
            this.text = text;
        }

        public override void Draw(SpriteBatch sb)
        {
            Vector2 stringSize = SceneManager.CurrentScene.Fonts[Texture].MeasureString(text) * 0.5f;

            sb.DrawString(SceneManager.CurrentScene.Fonts[Texture],
                text,
                (WorldPosition() - Camera.Position + (new Vector2(RenderingManager.WIDTH / 2, RenderingManager.HEIGHT / 2) * RenderingManager.WindowScale)),
                Color.Blue,
                Transform.Rotation,
                stringSize,
                RenderingManager.WindowScale * Transform.Scale * textScale,
                SpriteEffect,
                1);
        }

    }
}
