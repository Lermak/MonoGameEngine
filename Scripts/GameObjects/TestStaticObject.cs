 using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class TestStaticObject : WorldObject
    {
        public TestStaticObject(string texID, byte layer) : base(texID, "StaticTest", new string[] { "testStaticObject" }, new Vector2(40,40), new Vector2(100, 100), layer)
        {
            ComponentHandler.AddComponent(new CollisionBox(this, "myBox", true));
            //SpriteRenderer.Shader = "BlueShader";
        }
    }
}
