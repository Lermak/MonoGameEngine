using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public static class InventoryItemBehaviors
    {
        public static void RotateItem(float dt, GameObject go, Component[] c = null)
        {
            InventoryItem item = (InventoryItem)go;
            if (item.ShapeData.FollowMouse)
            {
                if (InputManager.IsTriggered(ConfigurationManager.Configuration.Keybindings["rot_left"]))
                {
                    item.Transform.Rotate(90);
                    item.ShapeData.rotateLeft();
                }
                else if (InputManager.IsTriggered(ConfigurationManager.Configuration.Keybindings["rot_right"]))
                {
                    item.Transform.Rotate(-90);
                    item.ShapeData.rotateRight();
                }
            }
        }
        public static void PickupItem(float dt, GameObject go, Component[] c = null)
        {
            InventoryItem item = (InventoryItem)go;
            Collider col = (Collider)go.GetComponent("myBox");
            Vector2 v = InputManager.MousePos;

            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                col.ContainsPoint(v))
            {
                if (!item.ShapeData.FollowMouse)
                {
                    if (InventoryItemShapeData.CanGrab)
                    {
                        InventoryItemShapeData.CanGrab = false;

                        item.ShapeData.FollowMouse = true;
                        if (item.ShapeData.Placed)
                        {
                            item.ShapeData.Placed = false;
                            InventoryGrid.Inventory.RemoveItemFromPosition(item);
                        }
                    }
                }
                else
                {
                    if (InventoryGrid.Inventory.CanPlaceItem(item))
                    {
                        InventoryItemShapeData.CanGrab = true;
                        item.ShapeData.FollowMouse = false;
                        item.ShapeData.Placed = true;
                        InventoryGrid.Inventory.PlaceItem(item);
                        item.Transform.SetPosition(item.GridToPos);
                    }
                    else if(!InventoryGrid.Inventory.IsInsideGrid(item))
                    {
                        InventoryItemShapeData.CanGrab = true;
                        item.ShapeData.FollowMouse = false;
                        item.ShapeData.Placed = false;
                    }
                    else
                    {
                        CoroutineManager.Add(Coroutines.Shake(.1f, 10, 15, item.SpriteRenderer), "BadPlacement", 0, true);
                    }
                }
            }
        }
        public static void FollowMouse(float dt, GameObject go, Component[] c = null)
        {
            InventoryItem item = (InventoryItem)go;
            if(item.ShapeData.FollowMouse)
            {
                item.Transform.SetPosition(InputManager.MousePos);
            }
        }
    }
}
