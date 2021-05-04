using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class TestStaticObject : WorldObject
    {
        public TestStaticObject(string texID, string tag) : base(texID, tag, new Vector2(40,40), new Vector2(100, 100))
        {
            ComponentHandler.AddComponent(new CollisionBox(this, 0, "myBox", true));
            //SpriteRenderer.Shader = SceneManager.CurrentScene.Effects["BlueShader"];
        }
    }
}
