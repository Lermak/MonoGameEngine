using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class ItemInfo : WorldObject
    {
        FontRenderer ItemName;
        FontRenderer ItemStats;
        public ItemInfo(string texID, string name, Vector2 pos) : base(texID, name, new string[] { "SystemInfo" }, pos, 4)
        {
            SpriteRenderer.Visible = false;
            ItemName = (FontRenderer)AddComponent(new FontRenderer("ItemName", this, "", "BaseFont", new Vector2(100, 40), 1));
            ItemName.DrawOffset = new Vector2(0, Globals.ResourceManager.GetTextureSize("SystemInfo").Y / 2 - 25);
            ItemName.TextScale = 2;
            ItemStats = (FontRenderer)AddComponent(new FontRenderer("ItemDesc", this, "", "BaseFont", new Vector2(100, 80), 1));
            ItemStats.DrawOffset = new Vector2(0, Globals.ResourceManager.GetTextureSize("SystemInfo").Y / 2 - 110);
            ItemStats.TextScale = 2;

            AddBehavior("DisplaySystemInfo", VisitSystemBehaviors.DisplayItemInfo);
        }
    }
}
