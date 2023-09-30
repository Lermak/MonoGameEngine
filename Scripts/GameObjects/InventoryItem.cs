using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace MonoGame_Core.Scripts
{
    public class InventoryItem : GameObject { 
            public enum SHAPE
        {
            Line,
            Square,
            T,
            J,
            L,
            S,
            Z,
        }
        public enum DIRECTION
        {
            Right = 0,
            Up = 90,
            Left = 180,
            Down = 270,
        }

        public SHAPE shape;
        public DIRECTION direction;
        public Vector2[] blocks;
        
        public Vector2 gridPos;
        public InventoryItem(string name, SHAPE s, DIRECTION dir, Vector2 position, string[] tags) : base(name, new string[] { "inventoryItem" })
        {
            shape = s;
            direction = dir;
            this.gridPos = position;
            switch(s)
            {
                case SHAPE.Line:
                    blocks = new Vector2[3] { new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0)};
                    break;
                case SHAPE.Square:
                    blocks = new Vector2[3] { new Vector2(1, 0), new Vector2(0, 1), new Vector2(1,1) };
                    break;
                case SHAPE.T:
                    blocks = new Vector2[3] { new Vector2(1, 0), new Vector2(2, 0), new Vector2(1, 1) };
                    break;
                case SHAPE.J:
                    blocks = new Vector2[3] { new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 2) };
                    break;
                case SHAPE.L:
                    blocks = new Vector2[3] { new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 2) };
                    break;
                case SHAPE.S:
                    blocks = new Vector2[3] { new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1) };
                    break;
                case SHAPE.Z:
                    blocks = new Vector2[3] { new Vector2(1, 0), new Vector2(1, -1), new Vector2(2, -1) };
                    break;

            }


        }

        public override void Initilize()
        {
            base.Initilize();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }
    }
}
