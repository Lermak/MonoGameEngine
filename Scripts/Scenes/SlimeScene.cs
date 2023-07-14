using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGame_Core.Scripts
{
    public class SlimeScene : Scene
    {
        protected override void loadContent()
        {
            ResourceManager.AddSong("Melody", "Music/TestSong");
            ResourceManager.AddSoundEffect("TestHit", "Sound/TestHit");

            ResourceManager.AddTexture("SlimeSpriteSheet", "Slime");
        }

        protected override void loadObjects()
        {
            WorldObject slime = InitWorldObject(new WorldObject("SlimeSpriteSheet", "Slime", new string[] { }, new Vector2(), 0, new Vector2(48,48)));
            slime.ComponentHandler.Add(new AnimationData(slime, .125f));
            slime.ComponentHandler.Add(new Movement(slime, "movement", 400, 360));
            slime.BehaviorHandler.Add("Animate", Behaviors.Animate);
            slime.BehaviorHandler.Add("Move", Behaviors.MoveWithRot);

            WorldObject slime2 = InitWorldObject(new WorldObject("SlimeSpriteSheet", "Slime2", new string[] { }, new Vector2(100,100), 0, new Vector2(48, 48)));
            slime2.ComponentHandler.Add(new AnimationData(slime2, .125f));
            slime2.BehaviorHandler.Add("Animate", Behaviors.Animate);
        }
    }
}
