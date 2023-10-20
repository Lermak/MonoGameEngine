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
            if (Player.ShipState == ShipData.ShipState.Sorting)
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
        }
        public static void PickupItem(float dt, GameObject go, Component[] c = null)
        {
            if (Player.ShipState == ShipData.ShipState.Sorting)
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
                            item.SpriteRenderer.OrderInLayer = 5;
                            if (item.ShapeData.Placed)
                            {
                                item.ShapeData.Placed = false;
                                Player.Inventory.RemoveItemFromPosition(item);
                            }
                        }
                    }
                    else
                    {
                        if (Player.Inventory.CanPlaceItem(item))
                        {
                            InventoryItemShapeData.CanGrab = true;
                            item.ShapeData.FollowMouse = false;
                            item.ShapeData.Placed = true;
                            Player.Inventory.PlaceItem(item);
                            item.SpriteRenderer.OrderInLayer = 1;
                            item.Transform.SetPosition(item.GridToPos);
                            item.SetParent(Player.Ship, true);
                        }
                        else if (!Player.Inventory.IsInsideGrid(item))
                        {
                            item.SpriteRenderer.OrderInLayer = 1;
                            InventoryItemShapeData.CanGrab = true;
                            item.ShapeData.FollowMouse = false;
                            item.ShapeData.Placed = false;
                        }
                        else
                        {
                            Globals.CoroutineManager.Add(Coroutines.Shake(.1f, 10, 15, item.SpriteRenderer), "BadPlacement", 0, true);
                        }
                    }
                }
            }
        }
        public static void FollowMouse(float dt, GameObject go, Component[] c = null)
        {
            if (Player.ShipState == ShipData.ShipState.Sorting)
            {
                InventoryItem item = (InventoryItem)go;
                if (item.ShapeData.FollowMouse)
                {
                    item.Transform.SetPosition(InputManager.MousePos);
                }
            }
        }
    }
}
