using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using static System.Net.Mime.MediaTypeNames;


namespace MonoGame_Core.Scripts
{
    public class InventoryItem : GameObject { 
        public enum SHAPE
        {
            Line,
            Square,
            T,
            J,
            L,
            S,
            Z,
        }
        public enum DIRECTION
        {
            Right = 0,
            Up = 1,
            Left = 2,
            Down = 3,
        }

        public SHAPE shape;
        public DIRECTION direction;
        public string[] blocks;


        
        
        public InventoryItem(string name, SHAPE s, DIRECTION dir, Vector2 position, string[] tags) : base(name, new string[] { "inventoryItem" })
        {
            shape = s;
            direction = dir;
            AddComponent(new Transform(this, InventoryGrid.gridToWorld(position), 0 , 0));

            this.blocks = new string[4] { "","","",""};
            for (int i = 0; i < 4; i++) {
                this.blocks[i] = (Guid.NewGuid().ToString());
                SceneManager.CurrentScene.InitWorldObject(new WorldObject("Block", blocks[i], new string[] { }, new Vector2(), 1));
            }
            

            Vector2[] positions = new Vector2[] { };
            switch (s)
            {
                case SHAPE.Line:
                    positions = new Vector2[4] { new Vector2(0,0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0)};
                    break;
                case SHAPE.Square:
                    positions = new Vector2[4] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1,1) };
                    break;
                case SHAPE.T:
                    positions = new Vector2[4] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(1, 1) };
                    break;
                case SHAPE.J:
                    positions = new Vector2[4] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, 2) };
                    break;
                case SHAPE.L:
                    positions = new Vector2[4] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 2) };
                    break;
                case SHAPE.S:
                    positions = new Vector2[4] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1) };
                    break;
                case SHAPE.Z:
                    positions = new Vector2[4] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, -1), new Vector2(2, -1) };
                    break;
            }


            for (int i = 0; i < 4; i++)
            {
                ((Transform)(SceneManager.CurrentScene.GetObjectByName(blocks[i]).GetComponent("transform"))).Attach((Transform)this.GetComponent("transform"), false);
                ((Transform)(SceneManager.CurrentScene.GetObjectByName(blocks[i]).GetComponent("transform"))).SetPosition(positions[i] * Globals.TILE_SIZE);
            }
            
        }
        public void rotateLeft()
        {
            for(int i = 0; i < 4; i++)
            {
                Vector2 oldPos = ((Transform)(SceneManager.CurrentScene.GetObjectByName(blocks[i]).GetComponent("transform"))).GetReletivePosition();
                Vector2 newPos = new Vector2(-oldPos.Y, oldPos.X);
                ((Transform)(SceneManager.CurrentScene.GetObjectByName(blocks[i]).GetComponent("transform"))).SetPosition(newPos);
                this.direction += 1;
            }
        }

        public void rotateRight()
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 oldPos = ((Transform)(SceneManager.CurrentScene.GetObjectByName(blocks[i]).GetComponent("transform"))).GetReletivePosition();
                Vector2 newPos = new Vector2(oldPos.Y, -oldPos.X);
                ((Transform)(SceneManager.CurrentScene.GetObjectByName(blocks[i]).GetComponent("transform"))).SetPosition(newPos);
                this.direction -= 1;
            }
        }

        public override void Initilize()
        {
            base.Initilize();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
        }
    }
}
