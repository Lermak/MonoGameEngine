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
        public FontRenderer(GameObject go, string text, string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, Color clr, int uo) : base(go, texID, t, off, drawArea, orderInLayer, clr, 0, uo)
        {
            name = "fontRenderer";
            this.text = text;
        }

        public override void Draw(SpriteBatch sb, Camera c)
        {
            Vector2 stringSize = SceneManager.CurrentScene.Fonts[Texture].MeasureString(text) * 0.5f;

            if (isHUD)
            {
                sb.DrawString(SceneManager.CurrentScene.Fonts[Texture],
                    text,
                    ScreenPosition(c),
                    new Color(Color.R - (int)RenderingManager.GlobalFade, Color.G - (int)RenderingManager.GlobalFade, Color.B - (int)RenderingManager.GlobalFade, Color.A),
                    -(Transform.Radians + addedRotation),
                    stringSize,
                    RenderingManager.WindowScale * Transform.Scale * textScale,
                    SpriteEffect,
                    1);
            }
            else
            {
                sb.DrawString(SceneManager.CurrentScene.Fonts[Texture],
                    text,
                    ScreenPosition(c),
                    new Color(Color.R - (int)RenderingManager.GlobalFade, Color.G - (int)RenderingManager.GlobalFade, Color.B - (int)RenderingManager.GlobalFade, Color.A),
                    -(Transform.Radians + addedRotation),
                    stringSize,
                    RenderingManager.GameScale * Transform.Scale * textScale,
                    SpriteEffect,
                    (float)transform.Layer / 256);
            }
        }

    }
}
