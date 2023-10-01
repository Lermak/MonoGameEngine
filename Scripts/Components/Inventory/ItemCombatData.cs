using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace MonoGame_Core.Scripts
{
    public class ItemCombatData : Component
    {
        //TODO: Use the data (and add more) to generate a Bullet object.
        //Other items in the inventory might modify these values
        //
        private int damage;
        public float DamageMult;
        public int DamageBoost;
        public int Damage { get { return (int)((damage + DamageBoost) * DamageMult); } }

        private float speed; //travel speed
        public float SpeedMult;
        public float SpeedBoost;
        public int Speed { get { return (int)((speed + SpeedBoost) * SpeedMult); } }

        private float reloadSpeed; //Use as number of shots per second (ReloadSpeed * GameTime). This way we can do % increases
        public float ReloadMult;
        public float ReloadSpeed { get { return reloadSpeed * ReloadMult; } }


        //DEAL WITH THESE LAST, MAY NOT BE INCLUDED FEATURE
        public List<BehaviorHandler.Act> ShotBehaviors = null; //This will store special bullet proerties like "homing shot"
        public List<Component> ShotComponents = null;// Components that would be needed for the above behaviors

        public string BulletTexID;

        public ItemCombatData(GameObject go, string name, int damage, float speed, float reload, string bulletTexID) : base(go, name)
        {
            this.damage = damage;
            this.speed = speed;
            reloadSpeed = reload;
            BulletTexID = bulletTexID;

            Reset();
        }

        public void Reset() //call reset every time we rebuild the ship then re-calculate values from inventory
        {
            this.DamageBoost = 0;
            this.DamageMult = 1;
            this.SpeedBoost = 0;
            this.SpeedMult = 1;
            this.ReloadMult = 1;
            ShotBehaviors = new List<BehaviorHandler.Act>();
            ShotComponents = new List<Component>();
        }
    }
}
