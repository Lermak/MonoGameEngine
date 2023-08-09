using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class Switch : WorldObject
    {
        public Switch(string onDeselectedTexture, string onSelectedTexture, string offDeselectedTexture, string offSelectedTexture, string name, Vector2 pos, byte layer, BehaviorHandler.Act onClick) : base(offDeselectedTexture, name, new string[] { "switch" }, pos, layer)
        {

            SpriteRenderer.IsHUD = true;
            CollisionBox cb = (CollisionBox)AddComponent(new CollisionBox(
                this,
                "myBox",
                true,
                ResourceManager.GetTextureSize(offDeselectedTexture)
                )
            );

            SwitchData swData = (SwitchData)ComponentHandler.Add( new SwitchData(
                this,
                "switchData",
                onDeselectedTexture, onSelectedTexture,
                offDeselectedTexture, offSelectedTexture
                )
            );

            behaviorHandler.Add(
                "Hover",
                Behaviors.SwitchSwapImagesOnHover,
                new Component[] { Transform, swData, cb }
            );
            behaviorHandler.Add(
                "Switch",
                Behaviors.SwitchSwapImagesOnClick,
                new Component[] { Transform, swData, cb }
            );
            
            if (onClick != null) {
                behaviorHandler.Add("OnClick", onClick);
            }
        }
    }
}
