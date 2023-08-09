using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class SwitchData : Component
    {
        public string SwitchOnTexID;
        public string SwitchOffTexID;
        // manages the switch trigger state
        public bool SwitchOn;
        
        public SwitchData(
            GameObject gameObject,
            string name,
            string switchOnTex,
            string switchOffTex
            ) : base(gameObject, name)
        {
            // grab & store the texture IDs for the switch
            SwitchOffTexID = switchOffTex;
            SwitchOnTexID = switchOnTex;
            // default switch to the "off" state
            SwitchOn = false;
            
        }
    }
}
