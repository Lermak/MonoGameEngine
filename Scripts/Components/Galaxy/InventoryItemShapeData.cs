using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace MonoGame_Core.Scripts
{
    public class InventoryItemShapeData : Component
    {
        public enum Shapes
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


        public Shapes shape;

        public Vector2 GridPosition = new Vector2(-1, -1);
        public Vector2[] GridCells;
        public bool Placed = false;
        public bool FollowMouse = false;
        public Vector2 CornerOffset = new Vector2(InventoryGridData.TILE_SIZE, InventoryGridData.TILE_SIZE) / 2;

        public static bool CanGrab = true;

        public InventoryItemShapeData(GameObject go, Shapes shape) : base(go, "inventoryItemShape")
        {
            switch (shape)
            {
                case Shapes.Line:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0) };
                    break;
                case Shapes.Square:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };
                    break;
                case Shapes.T:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(1, 1) };
                    break;
                case Shapes.J:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 2) };
                    break;
                case Shapes.L:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 2) };
                    break;
                case Shapes.S:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1) };
                    break;
                case Shapes.Z:
                    GridCells = new Vector2[] { new Vector2(0, 1), new Vector2(0, 2), new Vector2(1, 2), new Vector2(2, 2) };
                    break;
                case Shapes.Block:
                    GridCells = new Vector2[] { new Vector2(0, 0) };
                    break;
                case Shapes.TwoLine:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0) };
                    break;
                case Shapes.ThreeLine:
                    GridCells = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0) };
                    break;
                case Shapes.LHook:
                    GridCells = new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };
                    break;
                case Shapes.RHook:
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
