using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class UIBar : WorldObject
    {
        public UIBar(string texID, string name, Vector2 pos) : base(texID, name, new string[] { "UIBar" }, pos, 1)
        {

        }
    }
}
