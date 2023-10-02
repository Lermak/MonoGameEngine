using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using static System.Net.Mime.MediaTypeNames;


namespace MonoGame_Core.Scripts
{
    public class InventoryItem : WorldObject 
    {
        public Vector2 GridToPos { 
            get {
                if (ShapeData.GridPosition.X < 0 || ShapeData.GridPosition.Y < 0) return Transform.Position;
                else
                {
                    Vector2 dist = Transform.Position - (((CollisionBox)GetComponent("myBox")).BottomLeft() + ShapeData.CornerOffset);
                    return InventoryGrid.Inventory.CellZero // Top left world position
                        + new Vector2(ShapeData.GridPosition.X, -ShapeData.GridPosition.Y) * InventoryGridData.TILE_SIZE // add position in grid
                        + new Vector2(InventoryGridData.TILE_SIZE, -InventoryGridData.TILE_SIZE) / 2 // add half a tile
                        + dist;
                }
            } 
        }

        public Vector2 PosToGrid { 
            get {
                Vector2 pos = ((CollisionBox)GetComponent("myBox")).BottomLeft() + ShapeData.CornerOffset;
                return pos;
            } 
        }
        public InventoryItemShapeData ShapeData { get { return (InventoryItemShapeData)componentHandler.Get("inventoryItemShape"); } }
        public ItemData ItemData { get { return (ItemData)componentHandler.Get("ItemData"); } }

        public InventoryItem(string name, string texID, Vector2 pos, InventoryItemShapeData.Shapes shape) : base(texID, name, new string[] { "inventoryItem" }, pos, 2)
        {
            AddComponent(new CollisionBox(this, "myBox", true, ResourceManager.GetTextureSize(texID)));
            AddComponent(new InventoryItemShapeData(this, shape));
            ItemData itemData = (ItemData)AddComponent(ItemData.Random(this));
            //AddComponent(new ItemCombatData(this, "CombatData", 1, 1, 1, ""));

            AddBehavior("Purchase", VisitSystemBehaviors.Purchase, new Component[] { itemData });
            AddBehavior("Pickup", InventoryItemBehaviors.PickupItem);
            AddBehavior("FollowMouse", InventoryItemBehaviors.FollowMouse);
            AddBehavior("Rotate", InventoryItemBehaviors.RotateItem);
        }

        public void Restore()
        {
            SpriteRenderer.Cameras = new System.Collections.Generic.List<Camera>() { CameraManager.MainCamera };
            SceneManager.CurrentScene.InitWorldObject(this);
        }
    }
}
