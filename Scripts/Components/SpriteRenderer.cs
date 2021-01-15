using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MonoGame_Core.Scripts
{
    public class SpriteRenderer : Component
    {
        public string Texture;
        public int OrderInLayer;
        public Color Color;
        public Transform Transform;
        Vector2 offSet;
        Vector2 drawArea;
        int frames;
        int currentFrame = 0;
        float timeSinceFrameChange = 0;
        public bool IsHUD = false;
        public bool Visible = true;
        public bool Posted = false;
        public float Layer = 0;
        public SpriteRenderer(string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, Color clr, int frames, int uo) : base(uo)
        {
            Texture = texID;
            Transform = t;
            this.OrderInLayer = orderInLayer;
            this.drawArea = drawArea;
            Color = clr;
            this.frames = frames;
        }
        public SpriteRenderer(string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, int frames, int uo) : base(uo)
        {
            Texture = texID;
            Transform = t;
            this.OrderInLayer = orderInLayer;
            this.drawArea = drawArea;
            Color = Color.White;
            this.frames = frames;
        }

        public void Post()
        {
            Posted = true;
            RenderingManager.AddSpriteToDraw(this);
        }

        public Rectangle DrawRect()
        {
            return new Rectangle(currentFrame * (int)drawArea.X, 0, (int)drawArea.X, (int)drawArea.Y);
        }

        public override void Update(GameTime gt)
        {
            if (!Posted)
            {
                Post();
            }
            timeSinceFrameChange += (float)gt.ElapsedGameTime.TotalSeconds;
            if (timeSinceFrameChange > 1)
            {
                timeSinceFrameChange = 0;

                currentFrame++;

                if (currentFrame >= frames)
                    currentFrame = 0;
            }
            base.Update(gt);
        }

        public Vector2 WorldPosition()
        {
            return (Transform.Position - new Vector2(Transform.Width/2, Transform.Height/2) + offSet) * RenderingManager.Scale;
        }

    }
}
