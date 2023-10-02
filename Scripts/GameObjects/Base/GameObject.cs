using Microsoft.Xna.Framework;
using System.Collections.Generic;

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

        public string Name { get { return name; } }
        public bool ToDestroy { get { return destroy; } }
        public string[] Tags { get { return tags; } }
        public GameObject Parent { get { return this.parent; } set { this.parent = value; } }
        public List<GameObject> Children { get { return this.children; } set { this.children= value; } }

        public ComponentHandler ComponentHandler { get { return componentHandler; } }
        public BehaviorHandler BehaviorHandler { get { return behaviorHandler; } }

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

        public void Destroy()
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
    }
}
