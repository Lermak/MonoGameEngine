using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public class JumpGate : GalaxyNode
    {
        public JumpGate(string texID, string name, Vector2 pos) : base(texID, name, GalaxyData.GalaxyType.JumpGate, pos, 5)
        {
            RigidBody.AngularVelocity = -.05f;
        }
    }
}
