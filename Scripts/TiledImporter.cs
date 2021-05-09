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
        public static void LoadFromFile(Scene s, string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            int width = int.Parse(doc.ChildNodes[1].Attributes[4].Value);
            int height = int.Parse(doc.ChildNodes[1].Attributes[5].Value);
            int imageWidth = int.Parse(doc.ChildNodes[1].Attributes[6].Value);
            int imageHeight = int.Parse(doc.ChildNodes[1].Attributes[7].Value);
            string map = doc.ChildNodes[1].ChildNodes[1].ChildNodes[0].ChildNodes[0].Value;

            int[,] mapArr = new int[width, height];

            string[] rows = map.Trim().Split(new char[] { '\n' });

            CollisionManager.TileMap = new bool[width, height, doc.ChildNodes[1].ChildNodes.Count-1];
            CollisionManager.TileSize = new Vector2(imageWidth, imageHeight);
            CollisionManager.CollisionDetection = CollisionManager.CollisionType.TileMapFree;

            for (int y = 0; y < height; ++y)
            {
                string[] row = rows[y].Split(new char[] { ',' });
                for (int x = 0; x < width; ++x)
                {
                    mapArr[x, y] = int.Parse(row[x]);
                    int l = int.Parse(doc.ChildNodes[1].ChildNodes[0].Attributes[0].Value)-1;
                    string name = "TileX" + x + "Y" + y + "L" + doc.ChildNodes[1].ChildNodes[0].Attributes[0].Value;
                    Vector2 pos = new Vector2(imageWidth * x - width * imageWidth / 2, imageHeight * y - height * imageHeight / 2);
                    switch (int.Parse(row[x]))
                    {
                        case 2:
                            SceneManager.CurrentScene.GameObjects.Add(name, new TestStaticObject("Test"));
                            ((WorldObject)SceneManager.CurrentScene.GameObjects[name]).Transform.Place(pos);
                            CollisionManager.TileMap[x, y, l] = true;
                            break;
                    }
                    
                }
            }

            return;
        }
    }
}
