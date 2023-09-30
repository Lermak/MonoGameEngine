using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class SystemInfo : WorldObject
    {
        FontRenderer systemName;
        FontRenderer systemType;
        public SystemInfo(string texID, string name, Vector2 pos) : base(texID, name, new string[] { "SystemInfo" }, pos, 2)
        {


            SpriteRenderer.Visible = false;
            systemName = (FontRenderer)AddComponent(new FontRenderer("SystemName", this, "Test Name", "BaseFont", new Vector2(100, 40), 1));
            systemName.DrawOffset = new Vector2(0, ResourceManager.GetTextureSize("SystemInfo").Y/2-25);
            systemName.TextScale = 2;
            systemType = (FontRenderer)AddComponent(new FontRenderer("SystemType", this, "BBBBB", "BaseFont", new Vector2(100, 80), 1));
            systemType.DrawOffset = new Vector2(0, ResourceManager.GetTextureSize("SystemInfo").Y / 2 - 75);
            systemType.TextScale = 2;

            AddBehavior("DisplaySystemInfo", GalaxyMapBehaviors.DisplaySystemInfo);
        }

        public override void Initilize()
        {
            systemName.Initilize();
            systemType.Initilize();
            base.Initilize();
        }
    }
}
