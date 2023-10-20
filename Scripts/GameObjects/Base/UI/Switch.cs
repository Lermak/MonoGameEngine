using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class Switch : WorldObject
    {
        public Switch(string switchOnTex, string switchOffTex, string name, Vector2 pos, byte layer, BehaviorHandler.Act onClick) : base(switchOffTex, name, new string[] { "switch", "ui" }, pos, layer)
        {

            SpriteRenderer.IsHUD = true;
            CollisionBox collisionBox = (CollisionBox)AddComponent(new CollisionBox(this,"myBox",true,ResourceManager.GetTextureSize(switchOffTex), new List<string> { "myBox" }));

            SwitchData swData = (SwitchData)ComponentHandler.Add(new SwitchData(this,"swData",switchOnTex,switchOffTex));

            BehaviorHandler.Add("Shake",Behaviors.ShakeOnClick,new Component[] { Transform, swData, collisionBox });
            

            if (onClick != null)
            {
                behaviorHandler.Add("OnClick", onClick);
            }
        }
    }
}
