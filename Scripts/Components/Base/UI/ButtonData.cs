using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class ButtonData : Component
    {
        public string DeselectedTexID;
        public string SelectedTexID;

        public ButtonData(GameObject go, int uo, string name, string selectedTex, string deselectedTex) : base(go, uo, name)
        {
            DeselectedTexID = deselectedTex;
            SelectedTexID = selectedTex;
        }
    }
}
