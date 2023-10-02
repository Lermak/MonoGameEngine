using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// To be used inside a Ship class
    /// </summary>
    public static class BulletBehaviors
    {
        /// <summary>
        /// <i>Bullet-specific.</i><br />
        /// 
        /// moves a <b>Bullet</b> towards a rotation factoring in its speed percentage
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="go"></param>
        /// <param name="c"></param>
        public static void MoveShipTowardRotation(float dt, GameObject go, Component[] c = null)
        {
            Transform st = (Transform)go.GetComponent("transform");
            RigidBody srb = (RigidBody)go.GetComponent("rigidBody");
            BulletData bdata = (BulletData)go.GetComponent("BulletData");

            srb.MoveVelocity = hf_Math.RadToUnit(st.Radians + 90 * (float)Math.PI / 180) * dt * bdata.speed;
        }

    }
}