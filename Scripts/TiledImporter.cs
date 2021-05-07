using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MonoGame_Core.Scripts
{
    public static class TiledImporter
    {
        struct tiled
        {
            int width;
            int height;
            int imageWidth;
            int imageHeight;

        }

        public static Dictionary<int, GameObject> TileMapItems = new Dictionary<int, GameObject>();

        public static void Initilize()
        {

        }

        public static void LoadFromFile(Scene s, string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            int width = int.Parse(doc.ChildNodes[1].Attributes[4].Value);
            int height = int.Parse(doc.ChildNodes[1].Attributes[5].Value);
            int imageWidth = int.Parse(doc.ChildNodes[1].Attributes[6].Value);
            int imageHeight = int.Parse(doc.ChildNodes[1].Attributes[7].Value);
            string map = doc.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].Value;

            int[,] mapArr = new int[width, height];

            string[] rows = map.Trim().Split(new char[] { '\n' });

            for (int y = 0; y < height; ++y)
            {
                string[] row = rows[y].Split(new char[] { ',' });
                for (int x = 0; x < width; ++x)
                {
                    mapArr[x, y] = int.Parse(row[x]);
                    switch (int.Parse(row[x]))
                    {
                        case 0:
                            SceneManager.CurrentScene.GameObjects.Add("TileX"+x+"Y"+y+"L"+doc.ChildNodes[1].ChildNodes[0].Attributes[0].Value, new TestStaticObject("Test", "staticTest"));
                            ((WorldObject)SceneManager.CurrentScene.GameObjects["TileX" + x + "Y" + y + "L" + doc.ChildNodes[1].ChildNodes[0].Attributes[0].Value]).Transform.Place(new Vector2(40 * x, 40 * y));
                            break;
                    }
                    
                }
            }

            return;
        }


    }
}
