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
        public Vector2 DrawArea;
        int frames;
        int currentFrame = 0;
        float timeSinceFrameChange = 0;
        public bool IsHUD = false;
        public bool Visible = true;
        public bool Posted = false;
        public float Layer = 0;
        public SpriteRenderer(string name, string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, Color clr, int frames, int uo) : base(uo, name)
        {
            Texture = texID;
            Transform = t;
            this.OrderInLayer = orderInLayer;
            this.DrawArea = drawArea;
            Color = clr;
            this.frames = frames;
        }
        public SpriteRenderer(string name, string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, int frames, int uo) : base(uo, name)
        {
            Texture = texID;
            Transform = t;
            this.OrderInLayer = orderInLayer;
            this.DrawArea = drawArea;
            Color = Color.White;
            this.frames = frames;
        }
        public SpriteRenderer(string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, int frames, int uo) : base(uo, "spriteRenderer")
        {
            Texture = texID;
            Transform = t;
            this.OrderInLayer = orderInLayer;
            this.DrawArea = drawArea;
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
            return new Rectangle(currentFrame * (int)DrawArea.X, 0, (int)DrawArea.X, (int)DrawArea.Y);
        }

        public override void Update(float gt)
        {
            if (!Posted)
            {
                Post();
            }
            timeSinceFrameChange += gt;
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
            return ((Transform.Position - new Vector2(Transform.Width/2, Transform.Height/2) + offSet)) * RenderingManager.GameScale;
        }

    }
}
