using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class ComponentHandler
    {
        Dictionary<string, Component> components;
        GameObject gameObject;
        public GameObject GameObject { get { return gameObject; } }
        public ComponentHandler(GameObject go)
        {
            gameObject = go;
            components = new Dictionary<string, Component>();
        }

        public Component GetComponent(string t)
        {
            return components[t];
        }

        public void RemoveComponent(string t)
        {
            components[t].OnDestroy();
            components.Remove(t);
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

        public Component AddComponent(Component c)
        {
            components.Add(c.Name, c);

            return components[c.Name];
        }

        public void Initilize()
        {
            components.OrderBy(c => c.Value.UpdateOrder);
            foreach (Component c in components.Values)
                c.Initilize();
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
