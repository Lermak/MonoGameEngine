using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class GalaxyData : Component
    {
        public enum GalaxyType { Farming, Industry, Luxury, JumpGate };

        public GalaxyType SystemType;
        public int Row;
        public bool GoShopping = false;
        public string SystemName;

        public GalaxyData(GameObject go, string name, GalaxyType type, int row) : base(go, name)
        {
            string[] namePartOne = { "Bal", "Vat", "Ra", "En", "Ve" };
            string[] namePartTwo = { "jit", "nor", "cux", "" };
            string[] namePartThree = { "um", "es", "er", "" };

            Random r = new Random();
            string n = namePartOne[r.Next(namePartOne.Length)] + namePartThree[r.Next(namePartThree.Length)] + namePartTwo[r.Next(namePartTwo.Length)];
            SystemName = n;
            SystemType = type;
            Row = row;
        }
    }
}
