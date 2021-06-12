using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public class ComponentHandler
    {
        List<Component> components;
        GameObject gameObject;
        public GameObject GameObject { get { return gameObject; } }
        public ComponentHandler(GameObject go)
        {
            gameObject = go;
            components = new List<Component>();
        }

        public Component GetComponent(string t)
        {
            return components.Where(c => c.Name == t).First();
        }

        public void RemoveComponent(Component c)
        {
            c.OnDestroy();
            components.Remove(c);
        }

        public List<Component> GetComponentsOfType(string type)
        {
            List<Component> cl = new List<Component>();

            foreach (Component c in components)
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
            components.Add(c);

            return components[components.Count - 1];
        }

        public void Initilize()
        {
            components.OrderBy(c => c.UpdateOrder);
            foreach (Component c in components)
                c.Initilize();
        }

        public void OnDestroy()
        {
            foreach(Component c in components)
            {
                c.OnDestroy();
            }
            components.Clear();
        }
    }
}
