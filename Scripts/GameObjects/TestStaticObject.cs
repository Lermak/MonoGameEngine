using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class TestStaticObject : WorldObject
    {
        public TestStaticObject(string texID, string tag) : base(texID, tag)
        {
            Transform.Resize(40, 40);
            SpriteRenderer.SetDrawArea(40, 40);
            ComponentHandler.AddComponent(new CollisionBox(this, 0, "myBox", true));
            Transform.Place(new Vector2(100, 100));

            //SpriteRenderer.Shader = SceneManager.CurrentScene.Effects["BlueShader"];
        }
    }
}
