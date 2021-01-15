using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class ComponentHandler
    {
        Dictionary<string, Component> components;
        public ComponentHandler()
        {
            components = new Dictionary<string, Component>();
        }

        public Component GetComponent(string t)
        {
            return components[t];
        }

        public void AddComponent(string tag, Component c)
        {
            components.Add(tag, c);
        }

        public void Initilize()
        {
            components.OrderBy(c => c.Value.UpdateOrder);
        }

        public void Update(GameTime gt)
        {
            foreach(Component c in components.Values)
            {
                c.Update(gt);
            }
        }

        public void OnDestroy()
        {
            foreach(Component c in components.Values)
            {
                c.OnDestroy();
            }
            components.Clear();
        }
    }
}
