using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MonoGame_Core.Scripts
{
    public class GameObject
    {
        protected ComponentHandler componentHandler;
        protected BehaviorHandler behaviorHandler;
        protected string name;
        protected string[] tags;
        protected bool destroy = false;
        protected List<GameObject> children;
        protected GameObject parent;

        public string Name { get { return name; } set { name = value; } }
        public bool ToDestroy { get { return destroy; } set { ToDestroy = value; } }
        public string[] Tags { get { return tags; } set { tags = value; } }

        public ComponentHandler ComponentHandler { get { return componentHandler; } }
        public BehaviorHandler BehaviorHandler { get { return behaviorHandler; } }
        public GameObject Parent { get { return parent; } set { parent = value; } }

        public GameObject(string name, string[] tags)
        {
            this.name = name;
            this.tags = tags;
            behaviorHandler = new BehaviorHandler(this);
            componentHandler = new ComponentHandler(this);
        }

        public virtual void Initilize()
        {
            componentHandler.Initilize();
            behaviorHandler.Inizilize();
        }

        public virtual void Update(float dt)
        {
            if (destroy)
            {
                OnDestroy();
            }
            else
            {
                behaviorHandler.Update(dt);
            }
        }

        public virtual void Destroy()
        {
            destroy = true;
        }

        public virtual void OnDestroy()
        {
            behaviorHandler.OnDestroy();
            componentHandler.OnDestroy();
        }

        public virtual Component GetComponent(string name)
        {
            return componentHandler.Get(name);
        }
        public virtual Component AddComponent(Component c)
        {
            return componentHandler.Add(c);
        }
        public virtual void AddBehavior(string name, BehaviorHandler.Act b, Component[] c = null)
        {
            behaviorHandler.Add(name, b, c);
        }
        public virtual void GetBehavior(string name)
        {
            behaviorHandler.Get(name);
        }

        public virtual GameObject GetChild(string name)
        {
            GameObject go = children.Where(e => e.Name == name).FirstOrDefault();
            return go;
        }
        public virtual GameObject AddChild(GameObject go)
        {
            go.Parent = this;
            children.Add(go);
            return go;
        }
        public virtual GameObject RemoveChild(GameObject go)
        {
            go.Parent = null;
            children.Remove(go);
            return go;
        }
        public virtual GameObject RemoveChild(string name)
        {
            GameObject go = GetChild(name);
            if (go != null)
            {
                go.Parent = null;
                children.Remove(go);
                return go;
            }
            return null;
        }
        public virtual List<GameObject> GetChildren(string tag)
        {
            return children.Where(e => e.Tags.Contains(tag)).ToList();
        }
    }
}
