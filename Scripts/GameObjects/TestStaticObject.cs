 using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class TestStaticObject : WorldObject
    {
        public TestStaticObject(string texID, Vector2 pos, string name, byte layer) : base(texID, name, new string[] { "testStaticObject" }, new Vector2(40,40), pos, layer)
        {
            ComponentHandler.Add(new CollisionBox(this, "myBox", true));
            //SpriteRenderer.Shader = "BlueShader";
        }
    }
}
