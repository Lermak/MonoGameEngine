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
        public static void ShootOnClick(float dt, GameObject go, Component[] c = null)
        {
            Vector2 mousePos = InputManager.MousePos;
            Transform shipTF = (Transform)go.GetComponent("transform");
            ShipData shipData = (ShipData)go.GetComponent("ShipData");

            if (InputManager.IsTriggered(InputManager.MouseKeys.Left))
            {
                SceneManager.CurrentScene.AddWorldObject(new Bullet("BulletTex", "", shipTF.Position, shipTF.RotationDegrees));
            }
        }
        /// <summary>
        /// rotates an object towards a player's position.
        /// 
        /// Player transform should be <b>c[0]</b>
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="go"></param>
        /// <param name="c"></param>
        public static void PointToPlayer(float dt, GameObject go, Component[] c=null) {
            // todo: scaff
        }

    }
}