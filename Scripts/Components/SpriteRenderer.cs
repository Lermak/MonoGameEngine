﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MonoGame_Core.Scripts
{
    public class SpriteRenderer : Component
    {
        protected string texture;
        protected int orderInLayer;
        protected Color color;
        protected Transform transform;
        protected Vector2 offSet;
        protected Vector2 drawArea;
        protected string shader = "";
        protected List<Camera> cameras = new List<Camera>() { CameraManager.Cameras[0] };
        protected SpriteEffects spriteEffect = SpriteEffects.None;
        protected int frames;
        protected int currentFrame = 0;
        protected float timeSinceFrameChange = 0;
        protected bool isHUD = false;
        protected bool visible = true;
        protected float addedRotation = 0;

        public virtual string Texture { get { return texture; } 
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
        public Vector2 Offset { get { return offSet; } }
        public Vector2 DrawArea { get { return drawArea; } }
        public string Shader { get { return shader; } 
            set {
                if (SceneManager.CurrentScene.Effects.ContainsKey(value))
                    shader = value;
                else
                    shader = "";
            } 
        }
        public SpriteEffects SpriteEffect { get { return spriteEffect; } }
        public bool IsHUD { get { return isHUD; } set { isHUD = value; } }
        public bool Visible { get { return visible; } set { visible = value; } }
        public float AddedRotation { get { return addedRotation; } set { addedRotation = value; } }
        public List<Camera> Cameras { get { return cameras; } }
        public SpriteRenderer(GameObject go, string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, Color clr, int frames, int uo) : base(go, uo, "spriteRenderer")
        {
            Texture = texID;
            transform = t;
            offSet = off;
            this.orderInLayer = orderInLayer;
            this.drawArea = drawArea;
            color = clr;
            this.frames = frames;

            RenderingManager.Sprites.Add(this);
        }
        public SpriteRenderer(GameObject go, string texID, Transform t, Vector2 off, Vector2 drawArea, int orderInLayer, int frames, int uo) : base(go, uo, "spriteRenderer")
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

        public virtual void Draw(SpriteBatch sb, Camera c)
        {
            if (isHUD)
            {
                sb.Draw(SceneManager.CurrentScene.Textures[Texture],
                    ScreenPosition(c),
                    DrawRect(),
                    new Color(Color.R - (int)RenderingManager.GlobalFade, Color.G - (int)RenderingManager.GlobalFade, Color.B - (int)RenderingManager.GlobalFade, Color.A),
                    Transform.Rotation + addedRotation,
                    new Vector2(Transform.Width / 2, Transform.Height / 2),
                    RenderingManager.WindowScale * Transform.Scale,
                    SpriteEffect,
                    1);
            }
            else
            {
                sb.Draw(SceneManager.CurrentScene.Textures[Texture],
                    ScreenPosition(c),
                    DrawRect(),
                    new Color(Color.R - (int)RenderingManager.GlobalFade, Color.G - (int)RenderingManager.GlobalFade, Color.B - (int)RenderingManager.GlobalFade, Color.A),
                    Transform.Rotation + addedRotation,
                    new Vector2(Transform.Width / 2, Transform.Height / 2),
                    RenderingManager.GameScale * Transform.Scale,
                    SpriteEffect,
                    (float)transform.Layer / 256f);
            }
        }

        protected Vector2 ScreenPosition(Camera camera)
        {
            if (isHUD)
                return Transform.WorldPosition(offSet) + (new Vector2(RenderingManager.WIDTH / 2, RenderingManager.HEIGHT / 2) * RenderingManager.WindowScale);

            else
                return (Transform.WorldPosition(offSet) - camera.Position + (new Vector2(RenderingManager.WIDTH / 2, RenderingManager.HEIGHT / 2) * RenderingManager.WindowScale));
        }
    }
}
