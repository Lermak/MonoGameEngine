using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public class ComponentHandler
    {
        public Dictionary<string, Component> Components;
        public ComponentHandler()
        {
            Components = new Dictionary<string, Component>();
        }

        public void AddComponent(string tag, Component c)
        {
            Components.Add(tag, c);
        }

        public void Initilize()
        {
            Components.OrderBy(c => c.Value.UpdateOrder);
        }

        public void Update(GameTime gt)
        {
            foreach(Component c in Components.Values)
            {
                c.Update(gt);
            }
        }

        public void OnDestroy()
        {
            foreach(Component c in Components.Values)
            {
                c.OnDestroy();
            }
            Components.Clear();
        }
    }
}
