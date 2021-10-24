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
        protected string texture;
        protected int orderInLayer;
        protected Color color;
        protected Transform transform;
        protected Vector2 offset;
        protected Vector2 drawArea;
        protected string shader = "";
        protected List<Camera> cameras = new List<Camera>() { CameraManager.Cameras[0] };
        protected SpriteEffects flip = SpriteEffects.None;
        protected int currentFrame = 0;
        protected int currentAnimation = 0;
        protected float timeSinceFrameChange = 0;
        protected bool isHUD = false;
        protected bool visible = true;
        protected float addedRotation = 0;

        public int Frames { get { return (int)(ResourceManager.Textures[texture].Width / drawArea.X); } }
        public int Animations { get { return (int)(ResourceManager.Textures[texture].Height / drawArea.Y); } }

        public int CurrentFrame
        {
            get { return currentFrame; }
            set 
            {
                if (value > Frames)
                {
                    currentFrame = 0;
                }
                else
                    currentFrame = value;
            }
        }

        public int CurrentAnimation
        {
            get { return currentAnimation; }
            set
            {
                if (value > Animations)
                {
                    currentAnimation = 0;
                }
                else
                    currentAnimation = value;
            }
        }

        public virtual string Texture { get { return texture; } 
            set {
                if (ResourceManager.Textures.ContainsKey(value))
                    texture = value;
                else
                    texture = null;
            } 
        }
        public int OrderInLayer { get { return orderInLayer; } }
        public Color Color { get { return color; } }
        public Transform Transform { get { return transform; } }
        public Vector2 Offset { get { return offset; } }
        public Vector2 DrawArea { get { return drawArea; } }
        public string Shader { get { return shader; } 
            set {
                if (ResourceManager.Effects.ContainsKey(value))
                    shader = value;
                else
                    shader = "";
            } 
        }
        public SpriteEffects Flip { get { return flip; } set { flip = value; } }
        public bool IsHUD { get { return isHUD; } set { isHUD = value; } }
        public bool Visible { get { return visible; } set { visible = value; } }
        public float AddedRotation { get { return addedRotation; } set { addedRotation = value; } }
        public List<Camera> Cameras { get { return cameras; } }
        public float Hypotenuse { get { return hf_Math.Hypotenuse(drawArea.X / 2, drawArea.Y / 2); } }
        public SpriteRenderer(GameObject go, string texID, int orderInLayer) : base(go, "spriteRenderer")
        {
            Texture = texID;
            transform = (Transform)go.ComponentHandler.Get("transform");
            offset = new Vector2();
            this.orderInLayer = orderInLayer;
            this.drawArea = new Vector2(ResourceManager.Textures[texID].Width, ResourceManager.Textures[texID].Height);
            color = Color.White;

            RenderingManager.Sprites.Add(this);
        }
        public SpriteRenderer(GameObject go, string texID, int orderInLayer, Vector2 drawArea) : base(go, "spriteRenderer")
        {
            Texture = texID;
            transform = (Transform)go.ComponentHandler.Get("transform");
            offset = new Vector2();
            this.orderInLayer = orderInLayer;
            this.drawArea = drawArea;
            color = Color.White;

            RenderingManager.Sprites.Add(this);
        }

        public Rectangle DrawRect()
        {
            return new Rectangle(currentFrame * (int)DrawArea.X + (int)offset.X, //left x pos for the frame
                                currentAnimation * (int)DrawArea.Y + (int)offset.Y, //top y pos for the animation
                                (int)DrawArea.X, //width of frame
                                (int)DrawArea.Y); //height of frame
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
                sb.Draw(ResourceManager.Textures[Texture],
                    ScreenPosition(c),
                    DrawRect(),
                    new Color(Color.R - (int)RenderingManager.GlobalFade, Color.G - (int)RenderingManager.GlobalFade, Color.B - (int)RenderingManager.GlobalFade, Color.A),
                    -(Transform.Radians + addedRotation),
                    new Vector2(drawArea.X / 2, drawArea.Y / 2),
                    RenderingManager.WindowScale * Transform.Scale,
                    Flip,
                    1);
            }
            else
            {
                sb.Draw(ResourceManager.Textures[Texture],
                    ScreenPosition(c),
                    DrawRect(),
                    new Color(Color.R - (int)RenderingManager.GlobalFade, Color.G - (int)RenderingManager.GlobalFade, Color.B - (int)RenderingManager.GlobalFade, Color.A),
                    -(Transform.Radians + addedRotation),
                    new Vector2(drawArea.X / 2, drawArea.Y / 2),
                    RenderingManager.GameScale * Transform.Scale,
                    Flip,
                    (float)transform.Layer / 256f);
            }
        }

        protected Vector2 ScreenPosition(Camera camera)
        {
            if (isHUD)
                return Transform.WorldPosition() + (new Vector2(Globals.SCREEN_WIDTH / 2, Globals.SCREEN_HEIGHT / 2) * RenderingManager.WindowScale);

            else
                return (Transform.WorldPosition() - camera.Position + (new Vector2(Globals.SCREEN_WIDTH / 2, Globals.SCREEN_HEIGHT / 2) * RenderingManager.WindowScale));
        }
    }
}
