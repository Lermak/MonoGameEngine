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
        protected List<Camera> cameras = new List<Camera>() { Globals.CameraManager.Cameras[0] };
        protected SpriteEffects flip = SpriteEffects.None;
        protected int currentFrame = 0;
        protected int currentAnimation = 0;
        protected float timeSinceFrameChange = 0;
        protected bool isHUD = false;
        protected bool visible = true;
        protected float addedRotation = 0;
        protected Vector2 drawOffset = new Vector2();

        public int Frames { get { return (int)(Globals.ResourceManager.Textures[texture].Width / drawArea.X); } }
        public int Animations { get { return (int)(Globals.ResourceManager.Textures[texture].Height / drawArea.Y); } }

        public int CurrentFrame
        {
            get { return currentFrame; }
            set 
            {
                if (value > Frames || value < 0)
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
                if (Globals.ResourceManager.Textures.ContainsKey(value))
                    texture = value;
                else
                    texture = null;
            } 
        }
        public int OrderInLayer { get { return orderInLayer; } set { orderInLayer = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public Transform Transform { get { return transform; } }
        public Vector2 Offset { get { return offset; } set { offset = value; } }
        public Vector2 DrawArea { get { return drawArea; } }
        public string Shader { get { return shader; } 
            set {
                if (Globals.ResourceManager.Effects.ContainsKey(value))
                    shader = value;
                else
                    shader = "";
            } 
        }
        public SpriteEffects Flip { get { return flip; } set { flip = value; } }
        public bool IsHUD { get { return isHUD; } set { isHUD = value; } }
        public bool Visible { get { return visible; } set { visible = value; } }
        public float AddedRotation { get { return addedRotation; } set { addedRotation = value; } }
        public List<Camera> Cameras { get { return cameras; } set { cameras = value; } }
        public float Hypotenuse { get { return hf_Math.Hypot(drawArea.X / 2, drawArea.Y / 2); } }
        public Vector2 DrawOffset { get { return drawOffset; } set { drawOffset = value; } }
        public SpriteRenderer(GameObject go, string texID, int orderInLayer) : base(go, "spriteRenderer")
        {
            Texture = texID;
            transform = (Transform)go.ComponentHandler.Get("transform");
            offset = new Vector2();
            this.orderInLayer = orderInLayer;
            this.drawArea = Globals.ResourceManager.GetTextureSize(texID);
            color = Color.White;    
        }
        public SpriteRenderer(GameObject go, string texID, int orderInLayer, Vector2 drawArea) : base(go, "spriteRenderer")
        {
            Texture = texID;
            transform = (Transform)go.ComponentHandler.Get("transform");
            offset = new Vector2();
            this.orderInLayer = orderInLayer;
            this.drawArea = drawArea;
            color = Color.White;
        }

        public SpriteRenderer(GameObject go, string name, string texID, int orderInLayer, Vector2 drawArea) : base(go, name)
        {
            Texture = texID;
            transform = (Transform)go.ComponentHandler.Get("transform");
            offset = new Vector2();
            this.orderInLayer = orderInLayer;
            this.drawArea = drawArea;
            color = Color.White;
        }

        public Rectangle DrawRect()
        {
            return new Rectangle(currentFrame * (int)DrawArea.X + (int)offset.X, //left x pos for the frame
                                currentAnimation * (int)DrawArea.Y + (int)offset.Y, //top y pos for the animation
                                (int)DrawArea.X, //width of frame
                                (int)DrawArea.Y); //height of frame
        }

        public override void Initilize()
        {
            Globals.RenderingManager.Sprites.Add(this);
            base.Initilize();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Globals.RenderingManager.Sprites.Remove(this);
        }

        public void SetDrawArea(float width, float height)
        {
            drawArea.X = width;
            drawArea.Y = height;
        }

        public virtual void Draw(SpriteBatch sb, Camera c)
        {
            float zOrder = 0;
            if (isHUD)
                zOrder = 1;
            else
                zOrder = (float)transform.Layer / 256f;

            sb.Draw(Globals.ResourceManager.Textures[Texture],
                ScreenPosition(c),
                DrawRect(),
                new Color(Color.R - (int)Globals.RenderingManager.GlobalFade, Color.G - (int)Globals.RenderingManager.GlobalFade, Color.B - (int)Globals.RenderingManager.GlobalFade, Color.A),
                -(Transform.Radians + addedRotation),
                new Vector2(drawArea.X / 2, drawArea.Y / 2),
                Globals.RenderingManager.GameScale * Transform.Scale,
                Flip,
                zOrder);
        }

        protected Vector2 ScreenPosition(Camera camera)
        {
            if (isHUD)
                return (Transform.WorldPosition() + hf_Math.WorldPosition(DrawOffset)) + new Vector2(Globals.SCREEN_WIDTH / 2, Globals.SCREEN_HEIGHT / 2) * Globals.RenderingManager.WindowScale;
            else
                return (Transform.WorldPosition() + hf_Math.WorldPosition(DrawOffset)) - camera.Transform.WorldPosition() + new Vector2(Globals.SCREEN_WIDTH / 2, Globals.SCREEN_HEIGHT / 2) * Globals.RenderingManager.WindowScale;
        }
    }
}
