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
        private string texture;
        public int orderInLayer;
        Color color;
        Transform transform;
        Vector2 offSet;
        Vector2 drawArea;
        string shader;
        int target = -1;
        SpriteEffects spriteEffect = SpriteEffects.None;
        int frames;
        int currentFrame = 0;
        float timeSinceFrameChange = 0;
        bool isHUD = false;
        bool visible = true;
        byte layer = 0;

        public string Texture { get { return texture; } 
            set {
                if (SceneManager.CurrentScene.Textures.ContainsKey(value))
                    texture = value;
                else
                    texture = null;
            } 
        }
        public int OrderInLayer { get { return orderInLayer; } }
        public Color Color { get { return color; } }
        public Transform Transform { get { return transform; } }
        public Vector2 Offset { get { return Offset; } }
        public Vector2 DrawArea { get { return drawArea; } }
        public string Shader { get { return shader; } 
            set {
                if (SceneManager.CurrentScene.Effects.ContainsKey(value))
                    shader = value;
                else
                    shader = "";
            } 
        }
        public int Target { get { return target; } 
            set {
                if (value < RenderingManager.RenderTargets.Count)
                    target = value;
                else
                    target = -1;
            } }
        public SpriteEffects SpriteEffect { get { return spriteEffect; } }
        public bool IsHUD { get { return isHUD; } }
        public bool Visible { get { return visible; } set { visible = value; } }
        public byte Layer { get { return layer; } set { layer = value; } }
        public SpriteRenderer(string name, string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, Color clr, int frames, int uo) : base(uo, name)
        {
            Texture = texID;
            transform = t;
            offSet = off;
            this.orderInLayer = orderInLayer;
            this.drawArea = drawArea;
            color = clr;
            this.frames = frames;
        }
        public SpriteRenderer(string name, string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, int frames, int uo) : base(uo, name)
        {
            Texture = texID;
            transform = t;
            offSet = off;
            this.orderInLayer = orderInLayer;
            this.drawArea = drawArea;
            color = Color.White;
            this.frames = frames;

            RenderingManager.Sprites.Add(this);
        }
        public SpriteRenderer(string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, int frames, int uo) : base(uo, "spriteRenderer")
        {
            Texture = texID;
            transform = t;
            offSet = off;
            this.orderInLayer = orderInLayer;
            this.drawArea = drawArea;
            color = Color.White;
            this.frames = frames;

            RenderingManager.Sprites.Add(this);
        }

        public Rectangle DrawRect()
        {
            return new Rectangle(currentFrame * (int)DrawArea.X, 0, (int)DrawArea.X, (int)DrawArea.Y);
        }

        public override void Update(float gt)
        {
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

        public override void OnDestroy()
        {
            base.OnDestroy();
            RenderingManager.Sprites.Remove(this);
        }

        public void SetDrawArea(float width, float height)
        {
            drawArea.X = width;
            drawArea.Y = height;
        }
    }
}
