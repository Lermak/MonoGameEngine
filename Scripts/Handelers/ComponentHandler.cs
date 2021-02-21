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

        public List<Component> GetComponentsOfType(string type)
        {
            List<Component> cl = new List<Component>();

            foreach (Component c in components.Values)
            {
                if (c.Type == type)
                {
                    cl.Add(c);
                }
            }

            return cl;
        }

        public void AddComponent(Component c)
        {
            components.Add(c.Name, c);
        }

        public void Initilize()
        {
            components.OrderBy(c => c.Value.UpdateOrder);
        }

        public void Update(float gt)
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
