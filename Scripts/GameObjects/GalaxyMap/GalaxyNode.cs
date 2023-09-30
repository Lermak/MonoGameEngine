using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class GalaxyNode : WorldObject
    {
        public GalaxyNode(string texID, string name, GalaxyData.GalaxyType type, Vector2 pos, int row) : base(texID, name, new string[] { "GalaxyNode" }, pos, 1)
        {
            RigidBody.AngularVelocity = .2f;
            AddComponent(new GalaxyData(this, "GalaxyData", type, row));
            AddComponent(new CollisionCircle("myCircle", this, ResourceManager.GetTextureSize(texID).Length()/2, new Vector2(), true));
            AddBehavior("Visit", GalaxyMapBehaviors.VisitSystem);
        }
        public GalaxyNode(string texID, string name, Vector2 pos, int row) : base(texID, name, new string[] { "GalaxyNode" }, pos, 1)
        {
            Random r = new Random();
            GalaxyData.GalaxyType t = (GalaxyData.GalaxyType)r.Next(Enum.GetNames(typeof(GalaxyData.GalaxyType)).Length-1);
            RigidBody.AngularVelocity = .2f;
            GalaxyData g = (GalaxyData)AddComponent(new GalaxyData(this, "GalaxyData", t, row));
            AddComponent(new CollisionCircle("myCircle", this, ResourceManager.GetTextureSize(texID).Length()/2, new Vector2(), true));
            AddBehavior("Visit", GalaxyMapBehaviors.VisitSystem);
        }
    }
}
