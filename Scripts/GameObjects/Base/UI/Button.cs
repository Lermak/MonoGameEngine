using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class Button : WorldObject
    {
        public Button(string deselectedTexture, string selectedTexture, string name, Vector2 pos, byte layer, BehaviorHandler.Act onClick) : base(deselectedTexture, name, new string[] { "button" }, pos, layer)
        {
            SpriteRenderer.IsHUD = true;
            CollisionBox cb = (CollisionBox)AddComponent(new CollisionBox(this, "myBox", true, SceneManager.CurrentScene.ResourceManager.GetTextureSize(deselectedTexture), new List<string> { "myBox" }));
            ButtonData b = (ButtonData)componentHandler.Add(new ButtonData(this, "buttonData", selectedTexture, deselectedTexture));
            behaviorHandler.Add("Hover", Behaviors.ButtonSwapImagesOnHover, new Component[] { Transform, b });
            if (onClick != null)
                behaviorHandler.Add("OnClick", onClick);
        }
    }
}
