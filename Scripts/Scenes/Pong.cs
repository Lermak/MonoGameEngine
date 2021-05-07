using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoGame_Core.Scripts
{
    public class Pong : Scene
    {
        protected override void loadContent()
        {
            Textures["Paddle"] = Content.Load<Texture2D>("Images/Pong/Paddle");
            Textures["Ball"] = Content.Load<Texture2D>("Images/Pong/Ball");

            Fonts["BasicFont"] = Content.Load<SpriteFont>("Fonts/TestFont");

            GameObjects.Add("TopWall", new Wall("TopWall", new Vector2(0, 100 - RenderingManager.HEIGHT / 2), new Vector2(RenderingManager.WIDTH, 200)));
            GameObjects.Add("BottomWall", new Wall("BottomWall", new Vector2(0, 100 + RenderingManager.HEIGHT / 2), new Vector2(RenderingManager.WIDTH, 200)));
            GameObjects.Add("LeftWall", new Wall("LeftWall", new Vector2(-RenderingManager.WIDTH/2-100, 0), new Vector2(200, RenderingManager.HEIGHT)));
            GameObjects.Add("RightWall", new Wall("RightWall", new Vector2(RenderingManager.WIDTH/2+100,0), new Vector2(200, RenderingManager.HEIGHT)));

            GameObjects.Add("PlayerScore", new Score("BasicFont", "PlayerScore", new Vector2(-RenderingManager.WIDTH / 2 + 200, -RenderingManager.HEIGHT / 2 + 50), new Vector2(100, 50)));
            GameObjects.Add("OpponentScore", new Score("BasicFont", "PlayerScore", new Vector2(RenderingManager.WIDTH / 2 - 200, -RenderingManager.HEIGHT / 2 + 50), new Vector2(100, 50)));
            
            GameObjects.Add("Ball", new Ball("Ball", "Ball", new Vector2(), new Vector2(40, 40), 1));
            GameObjects.Add("Player", new PlayerPaddle("Paddle", "Player", new Vector2(40,120), new Vector2(60 - RenderingManager.WIDTH / 2, 0), 1));
            GameObjects.Add("AI", new AIPaddle("Paddle", "AI", new Vector2(40, 120), new Vector2(RenderingManager.WIDTH / 2 - 60, 0), 1));
        }
    }
}
