using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class Switch : WorldObject  {
        public Switch(string onDeselectedTexture, string onSelectedTexture, string offDeselectedTexture, string offSelectedTexture,string name, Vector2 pos, byte layer, BehaviorHandler.Act onClick) : base(offDeselectedTexture, name, new string[] { "switch" }, pos, layer) {
            SwitchData s = (SwitchData)ComponentHandler.Add(new SwitchData(
                
                this,
                "switchData",
                onDeselectedTexture,onSelectedTexture,
                offDeselectedTexture,offSelectedTexture

            ));

        }
    }
}
