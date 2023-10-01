using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace MonoGame_Core.Scripts
{
    public class InventoryItemShapeData : Component
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
            Block,
            TwoLine,
            ThreeLine,
            LHook,
            RHook
        }

        public SHAPE shape;

        public Vector2 GridPosition = new Vector2(-1, -1);
        public Vector2[] GridCells;
        public bool Placed = false;
        public bool FollowMouse = false;
        public Vector2 CornerOffset = new Vector2(InventoryGridData.TILE_SIZE, InventoryGridData.TILE_SIZE) / 2;

        public static bool CanGrab = true;

        public InventoryItemShapeData(GameObject go, SHAPE shape) : base(go, "inventoryItemShape")
        {
            switch (shape)
            {
                case SHAPE.Line:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0) };
                    break;
                case SHAPE.Square:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };
                    break;
                case SHAPE.T:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(1, 1) };
                    break;
                case SHAPE.J:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 2) };
                    break;
                case SHAPE.L:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 2) };
                    break;
                case SHAPE.S:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1) };
                    break;
                case SHAPE.Z:
                    GridCells = new Vector2[] { new Vector2(0, 1), new Vector2(0, 2), new Vector2(1, 2), new Vector2(2, 2) };
                    break;
                case SHAPE.Block:
                    GridCells = new Vector2[] { new Vector2(0, 0) };
                    break;
                case SHAPE.TwoLine:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0) };
                    break;
                case SHAPE.ThreeLine:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0) };
                    break;
                case SHAPE.LHook:
                    GridCells = new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };
                    break;
                case SHAPE.RHook:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1) };
                    break;
            }
        }

        public void rotateLeft()
        {
            for (int i = 0; i < GridCells.Length; i++)
            {
                GridCells[i] = new Vector2(GridCells[i].Y, -GridCells[i].X);
            }
            CornerOffset = new Vector2(-CornerOffset.Y, CornerOffset.X);
        }


        public void rotateRight()
        {
            for (int i = 0; i < GridCells.Length; i++)
            {
                GridCells[i] = new Vector2(-GridCells[i].Y, GridCells[i].X);
            }
            CornerOffset = new Vector2(CornerOffset.Y, -CornerOffset.X);
        }
    }
}
