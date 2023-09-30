using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class PurchaseShop : WorldObject
    {
        public PurchaseShop(string texID, string name, Vector2 pos) : base(texID, name, new string[] { "SellShop" }, pos, 1)
        {
            SpriteRenderer.Visible = false;
        }
    }
}
