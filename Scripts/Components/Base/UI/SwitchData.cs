using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class SwitchData : Component
    {
        public string OffDeselectedTexID;
        public string OffSelectedTexID;
        public string OnDeselectedTexID;
        public string OnSelectedTexID;
        // manages the switch trigger state
        public bool SwitchOn;
        
        public SwitchData(
            GameObject go,
            string name,
            string onSelectedTex,
            string onDeselectedTex,
            string offSelectedTex,
            string offDeselectedTex
            ) : base(go, name)
        {
            // grab & store the texture IDs for the switch
            OffDeselectedTexID = offDeselectedTex;
            OffSelectedTexID = offSelectedTex;
            OnDeselectedTexID = onDeselectedTex;
            OnSelectedTexID = onSelectedTex;
            SwitchOn = false;
            
        }
    }
}
