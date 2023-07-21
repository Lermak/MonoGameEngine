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

        public SwitchData(GameObject go, string name, string onSelectedTex, string onDeselectedTex, string offSelectedTex, string offDeselectedTex) : base(go, name)
        {
            OffDeselectedTexID = offDeselectedTex;
            OffSelectedTexID = offSelectedTex;
            OnDeselectedTexID = onDeselectedTex;
            OnSelectedTexID = onSelectedTex;
        }
    }
}
