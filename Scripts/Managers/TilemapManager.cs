using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public static class TilemapManager
    {
        public struct Tile
        {
            GameObject g;
        }
        static int[,] tileMap;
        public static int[,] TileMap { get { return tileMap; } }

        public static Dictionary<int, GameObject> TileMapItems = new Dictionary<int, GameObject>();

        public static void Initilize()
        {

        }

        public static void LoadDictionary(string file)
        {

        }

        public static void LoadResources(Scene s, string file)
        {

        }

        public static void LoadMap(Scene s, string file)
        {

        }

        public static void LoadObjects(Scene s, string file)
        {

        }
    }
}
