using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace MonoGame_Core.Scripts
{
    public class InventoryItemData : Component
    {
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
            Up = 1,
            Left = 2,
            Down = 3,
        }

        public SHAPE shape;
        public DIRECTION direction;

        // todo: scaff/actually put stuff here
        public (int X,int Y) position;
        public (int X, int Y)[] offset;
        public InventoryItemData(GameObject go, (int X, int Y)position, SHAPE shape, DIRECTION dir) : base(go, Guid.NewGuid().ToString())
        {


            switch (shape)
            {
                case SHAPE.Line:
                    offset = new (int X, int Y)[] { (0, 0), (1, 0), (2, 0), (3, 0) };
                    break;
                case SHAPE.Square:
                    offset = new (int X, int Y)[] { (0, 0), (1, 0), (0, 1), (1, 1) };
                    break;
                case SHAPE.T:
                    offset = new (int X, int Y)[] { (0, 0), (1, 0), (2, 0), (1, 1) };
                    break;
                case SHAPE.J:
                    offset = new (int X, int Y)[] { (0, 0), (1, 0), (1, 1), (1, 2) };
                    break;
                case SHAPE.L:
                    offset = new (int X, int Y)[] { (0, 0), (1, 0), (0, 1), (0, 2) };
                    break;
                case SHAPE.S:
                    offset = new (int X, int Y)[] { (0, 0), (1, 0), (1, 1), (2, 1) };
                    break;
                case SHAPE.Z:
                    offset = new (int X, int Y)[] { (0, 0), (1, 0),(1, -1), (2, -1) };
                    break;
            }


            while (dir != 0)
            {
                rotateCounterClockwise();
            }
        }

        public void rotateClockwise() {
            for (int i = 0; i < 4; i++)
            {
                offset[i] = (offset[i].Y, -offset[i].X);
            }
            direction--;
        }


        public void rotateCounterClockwise() { 
            for (int i = 0; i < 4; i++)
            {
                offset[i] = (-offset[i].Y, offset[i].X);
            }
            direction++;
        }
    }
}
