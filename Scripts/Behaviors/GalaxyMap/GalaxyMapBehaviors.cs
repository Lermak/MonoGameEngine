using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public static class GalaxyMapBehaviors
    {
        public static void TravelOnClick(float dt, GameObject go, Component[] c = null)
        {
            ShipMarkerData sd = (ShipMarkerData)go.GetComponent("ShipMarkerData");
            List<GameObject> galaxies = SceneManager.CurrentScene.GetObjects("GalaxyNode");

            if (InputManager.IsTriggered(InputManager.MouseKeys.Left))
            {
                foreach (GameObject g in galaxies)
                {
                    if (sd.Row < ((GalaxyData)g.GetComponent("GalaxyData")).Row)
                    {
                        if (((CollisionCircle)g.GetComponent("myCircle")).ContainsPoint(InputManager.MousePos))
                        {
                            sd.InFlight = true;
                            CoroutineManager.Add(GalaxyMapCoroutines.TravelToGalaxy((WorldObject)go, (Transform)g.GetComponent("transform")), "ShipTravel", 0, true);
                        }
                    }
                }
            }
        }
        public static void ShipMarkerIdleBob(float dt, GameObject go, Component[] c = null)
        {
            WorldObject s = (WorldObject)go;
            ShipMarkerData sd = (ShipMarkerData)go.GetComponent("ShipMarkerData");
            if (!sd.InFlight)
            {
                if (sd.bobDir)
                {
                    s.RigidBody.MoveVelocity = new Vector2(0, .1f);
                    if (s.Transform.Position.Y > sd.idePos.Y + 5)
                    {
                        sd.bobDir = !sd.bobDir;
                    }
                }
                else
                {
                    s.RigidBody.MoveVelocity = new Vector2(0, -.1f);
                    if (s.Transform.Position.Y < sd.idePos.Y)
                    {
                        sd.bobDir = !sd.bobDir;
                    }
                }
            }
        }
        public static void VisitSystem(float dt, GameObject go, Component[] c = null)
        {
            GalaxyData gd= (GalaxyData)go.GetComponent("GalaxyData");
            if (gd.GoShopping)
            {
                gd.GoShopping = false;
                SceneManager.ChangeScene(new VisitSystem(gd.SystemType));
            }
        }
        public static void DisplaySystemInfo(float dt, GameObject go, Component[] c = null)
        {
            List<GameObject> galaxies = SceneManager.CurrentScene.GetObjects("GalaxyNode");
            bool flag = false;
            WorldObject wo = (WorldObject)go;
            FontRenderer systemName = (FontRenderer)(wo.GetComponent("SystemName"));
            FontRenderer systemType = (FontRenderer)(wo.GetComponent("SystemType"));
            foreach (GameObject g in galaxies)
            {
                if (((CollisionCircle)g.GetComponent("myCircle")).ContainsPoint(InputManager.MousePos))
                {
                    flag = true;
                    wo.SpriteRenderer.Visible = true;
                    systemName.Visible = true;
                    systemType.Visible = true;
                    Vector2 tSize = ResourceManager.GetTextureSize("SystemInfo");
                    wo.Transform.SetPosition(InputManager.MousePos + new Vector2(InputManager.MousePos.X > Globals.SCREEN_WIDTH * .25f ? -tSize.X : tSize.X, InputManager.MousePos.Y > Globals.SCREEN_HEIGHT * .25f ? -tSize.Y : tSize.Y) /2);
                    GalaxyData gd = (GalaxyData)(g.GetComponent("GalaxyData"));
                    systemName.Text = gd.SystemName;
                    
                    if (gd.SystemType == GalaxyData.GalaxyType.Farming)
                    {
                        systemType.Text = "Farming";
                    }
                    else if (gd.SystemType == GalaxyData.GalaxyType.Industry)
                    {
                        systemType.Text = "Industry";
                    }
                    else if (gd.SystemType == GalaxyData.GalaxyType.Luxury)
                    {
                        systemType.Text = "Luxury";
                    }
                    else if (gd.SystemType == GalaxyData.GalaxyType.JumpGate)
                    {
                        systemType.Text = "Jump Gate";
                    }
                    
                }
            }
            if(!flag)
            {
                wo.SpriteRenderer.Visible = false;
                systemName.Visible = false;
                systemType.Visible = false;
            }
        }
    }
}
