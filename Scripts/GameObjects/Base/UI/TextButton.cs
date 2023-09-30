using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace MonoGame_Core.Scripts
{ 
    public class TextButton : Button
    {
        public TextButton(string name, string deselectedTexture, string selectedTexture, string text, int size, Vector2 pos, Vector2 fontOffset, byte layer, BehaviorHandler.Act onClick, Component[] onClickComponents = null) : base(deselectedTexture, selectedTexture, name, pos, layer, onClick, onClickComponents)
        {
            FontRenderer f = (FontRenderer)AddComponent(new FontRenderer("fontRenderer", this, text, "BaseFont", this.SpriteRenderer.DrawArea, layer+1));
            f.Offset = fontOffset;
            f.TextScale = size;
            f.Color = Color.Black;
        }
    }
}
