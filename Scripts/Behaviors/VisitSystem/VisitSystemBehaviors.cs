using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public static class VisitSystemBehaviors
    {
        public static void ReturnToMap(float dt, GameObject go, Component[] c = null)
        {
            Collider col = (Collider)go.GetComponent("myBox");
            Vector2 v = InputManager.MousePos;
            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                col.ContainsPoint(v))
            {
                SceneManager.ChangeScene(new GalaxyMap());
            }
        }
        public static void GotoShip(float dt, GameObject go, Component[] c = null)
        {
            Collider col = (Collider)go.GetComponent("myBox");
            Vector2 v = InputManager.MousePos;
            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                col.ContainsPoint(v))
            {
                CameraManager.MainCamera.Transform.SetPosition(new Vector2(Globals.SCREEN_WIDTH + 200, 0));
            }
        }
        public static void GotoSystem(float dt, GameObject go, Component[] c = null)
        {
            Collider col = (Collider)go.GetComponent("myBox");
            Vector2 v = InputManager.MousePos;
            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                col.ContainsPoint(v))
            {
                CameraManager.MainCamera.Transform.SetPosition(new Vector2());
            }
        }
        public static void Trade(float dt, GameObject go, Component[] c = null)
        {
            SpriteRenderer sellMenu = (SpriteRenderer)c[0];
            SpriteRenderer buyMenu = (SpriteRenderer)c[1];

            Collider col = (Collider)go.GetComponent("myBox");
            Vector2 v = InputManager.MousePos;
            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                col.ContainsPoint(v))
            {
                FontRenderer f = (FontRenderer)go.GetComponent("fontRenderer");
                if (f.Text == "Trade")
                {
                    f.Text = "Close";
                }
                else
                {
                    f.Text = "Trade";
                }
                sellMenu.Visible = !sellMenu.Visible;
                buyMenu.Visible = !buyMenu.Visible;
            }
        }
        public static void Purchase(float dt, GameObject go, Component[] c = null)
        {
            ItemData id = (ItemData)c[0];
            Collider col = (Collider)go.GetComponent("myBox");

            Vector2 v = InputManager.MousePos;
            
            if (InputManager.IsTriggered(InputManager.MouseKeys.Left) &&
                col.ContainsPoint(v))
            {
                if (!id.Sell && InventoryGrid.Inventory.Money >= id.PurchasePrice)
                {
                    id.Sell = true;
                    InventoryGrid.Inventory.Money -= id.PurchasePrice;
                }
            }
        }

        public static void DisplayItemInfo(float dt, GameObject go, Component[] c = null)
        {
            List<GameObject> items = SceneManager.CurrentScene.GetObjectsByTag("inventoryItem");
            bool flag = false;
            WorldObject wo = (WorldObject)go;
            FontRenderer itemName = (FontRenderer)(wo.GetComponent("ItemName"));
            FontRenderer itemDesc = (FontRenderer)(wo.GetComponent("ItemDesc"));
            foreach (GameObject g in items)
            {
                if (((CollisionBox)g.GetComponent("myBox")).ContainsPoint(InputManager.MousePos))
                {
                    flag = true;
                    wo.SpriteRenderer.Visible = true;
                    itemName.Visible = true;
                    itemDesc.Visible = true;
                    Vector2 tSize = ResourceManager.GetTextureSize("SystemInfo");
                    wo.Transform.SetPosition(InputManager.MousePos + new Vector2(InputManager.MousePos.X > Globals.SCREEN_WIDTH * .25f ? -tSize.X : tSize.X, InputManager.MousePos.Y > Globals.SCREEN_HEIGHT * .25f ? -tSize.Y : tSize.Y) / 2);
                    ItemData id = (ItemData)(g.GetComponent("ItemData"));
                    itemName.Text = id.DisplayName;
                    itemDesc.Text = id.ItemType.ToString() + "\nBuy: $" + id.PurchasePrice + "\nSell: $" + id.SellPrice;
                }
            }
            if (!flag)
            {
                wo.SpriteRenderer.Visible = false;
                itemName.Visible = false;
                itemDesc.Visible = false;
            }
        }
    }
}