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
        public List<Component> Components { get { return components; } }

        public ComponentHandler(GameObject go)
        {
            gameObject = go;
            components = new List<Component>();
        }

        public Component Get(string t)
        {
            return components.Where(c => c.Name == t).First();
        }

        public void Remove(Component c)
        {
            c.OnDestroy();
            components.Remove(c);
        }

        public List<Component> GetOfType(string type)
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

        public Component Add(Component c)
        {
            if(components.Where(x => x.Name == c.Name).Count() > 0)
            {
                throw new System.Exception("Duplicate component " + c.Name + " created on object");
            }
            components.Add(c);

            return components[components.Count - 1];
        }

        public void Initilize()
        {
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
