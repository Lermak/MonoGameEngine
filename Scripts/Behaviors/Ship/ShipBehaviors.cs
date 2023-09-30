using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// To be used inside a Ship class
    /// </summary>
    public static class ShipBehaviors
    {
        public static void ShootOnClick(float dt, GameObject go, Component[] c = null) {
            Vector2 mousePos = InputManager.MousePos;
            Transform shipTF = (Transform) go.GetComponent("transform");
            ShipData shipData = (ShipData) go.GetComponent("ShipData");

            if (InputManager.IsTriggered(InputManager.MouseKeys.Left)) {
                if ( shipData.weaponAmmo >= 1) {
                    
                    // todo: do the shooting
                    shipData.weaponAmmo -= 1;
                } // else dont shoot
            } // else dont do anything
        }
        public static void MoveWASD(float dt, GameObject go, Component[] c = null) {
            // todo:
        }
    }
}