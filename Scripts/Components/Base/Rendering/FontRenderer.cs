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
        public float TextScale { get { return textScale; } set { textScale = value; } }
        public override string Texture
        {
            get { return texture; }
            set
            {
                if (Globals.ResourceManager.Fonts.ContainsKey(value))
                    texture = value;
                else
                    texture = null;
            }
        }
        public FontRenderer(string name, GameObject go, string text, string texID, Vector2 drawArea, int orderInLayer) : base(go, name, texID, orderInLayer, drawArea)
        {
            this.text = text;
        }

        public override void Draw(SpriteBatch sb, Camera c)
        {
            Vector2 stringSize = Globals.ResourceManager.Fonts[Texture].MeasureString(text) * 0.5f;

            if (isHUD)
            {
                sb.DrawString(Globals.ResourceManager.Fonts[Texture],
                    text,
                    ScreenPosition(c),
                    new Color(Color.R - (int)Globals.RenderingManager.GlobalFade, Color.G - (int)Globals.RenderingManager.GlobalFade, Color.B - (int)Globals.RenderingManager.GlobalFade, Color.A),
                    -(Transform.Radians + addedRotation),
                    stringSize,
                    Globals.RenderingManager.WindowScale * Transform.Scale * textScale,
                    Flip,
                    1);
            }
            else
            {
                sb.DrawString(Globals.ResourceManager.Fonts[Texture],
                    text,
                    ScreenPosition(c),
                    new Color(Color.R - (int)Globals.RenderingManager.GlobalFade, Color.G - (int)Globals.RenderingManager.GlobalFade, Color.B - (int)Globals.RenderingManager.GlobalFade, Color.A),
                    -(Transform.Radians + addedRotation),
                    stringSize,
                    Globals.RenderingManager.GameScale * Transform.Scale * textScale,
                    Flip,
                    (float)transform.Layer / 256);
            }
        }

    }
}
