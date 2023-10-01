using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class ScrollingCredits : WorldObject
    {
        struct CreditLine
        {
            public int Size;
            public string Text;
            public CreditLine(int s, string t)
            {
                Size = s;
                Text = t;
            }
        }
        public ScrollingCredits(string texID, string name, Vector2 pos) : base(texID, name, new string[] { "Credits" }, pos, 0)
        {
            SpriteRenderer.Visible = false;
            List<CreditLine> credits = new List<CreditLine>
            {
                new CreditLine(5, "Programmers"),
                new CreditLine(3, ""),
                new CreditLine(3, "Blake"),
                new CreditLine(3, "Corbin"),
                new CreditLine(3, "Rhen"),
                new CreditLine(5, ""),
                new CreditLine(5, "Assets"),
                new CreditLine(3, "peakpx.com/en/hd-wallpaper-desktop-avecz"),
                new CreditLine(3, "pinterest.com/pin/40321359156160993/"),
                new CreditLine(3, "pexels.com/photo/stars-during-night-time-176851/"),
                new CreditLine(3, "anyrgb.com/en-clipart-nbvst"),
                new CreditLine(3, "hotpot.ai/s/art-generator/"),
                new CreditLine(3, "craiyon.com"),
                new CreditLine(3, "bing.com/images/create"),
                new CreditLine(3, "Kenny Game Assets")
            };

            int i = 1;
            int yPos = 0;
            foreach(CreditLine cl in credits)
            {
                FontRenderer f = (FontRenderer)AddComponent(new FontRenderer("Line" + i, this, cl.Text, "BaseFont", new Vector2(Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT), 1));
                f.TextScale = cl.Size;
                f.DrawOffset = new Vector2(0, yPos);
                yPos -= 20 * cl.Size;
                ++i;
            }

            FontRenderer thanks = (FontRenderer)AddComponent(new FontRenderer("Thanks", this, "Thanks For Playing", "BaseFont", new Vector2(Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT), 1));
            thanks.TextScale = 10;
            thanks.DrawOffset = new Vector2(0, yPos - Globals.SCREEN_HEIGHT/2);
            FontRenderer end = (FontRenderer)AddComponent(new FontRenderer("Return", this, "Press 'Space' to Return To Main Menu", "BaseFont", new Vector2(Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT), 1));
            end.TextScale = 2;
            end.DrawOffset = new Vector2(0, yPos - Globals.SCREEN_HEIGHT + end.TextScale*20);

            Transform.SetPosition(new Vector2(0, yPos));

            CoroutineManager.Add(Coroutines.CreditScroll(3, this.Transform, -yPos + ResourceManager.GetTextureSize(texID).Y, this.RigidBody), "ScrollCredits", 0, true);
        }
    }
}
