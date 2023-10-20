using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Linq;

namespace MonoGame_Core.Scripts
{
    public class BehaviorHandler
    {
        public delegate void Act(float uo, GameObject go, Component[] c);
        public struct Behavior
        {
            public string Name;
            public Component[] Components;
            public Act Run;

            public Behavior(string name, Act a, Component[] c = null)
            {
                Name = name;
                Components = c != null ? c : new Component[] { };
                Run = a;
            }
        }

        List<Behavior> behaviors;
        GameObject gameObject;
        public List<Behavior> Behaviors { get { return behaviors; } }
        public GameObject GameObject { get { return gameObject; } }

        public Behavior Get(string t)
        {
            return behaviors.Where(b => b.Name == t).FirstOrDefault();
        }

        public BehaviorHandler(GameObject go)
        {
            gameObject = go;
            behaviors = new List<Behavior>();
        }

        public void Add(string name, Act b, Component[] c = null)
        {
            behaviors.Add(new Behavior(name, b, c));
        }

        public void Inizilize()
        {
        }

        public void Update(float dt)
        {
            if (SceneManager.SceneState == SceneManager.State.Running)
            {
                foreach (Behavior b in Behaviors)
                {
                    b.Run(dt, gameObject, b.Components);
                }
            }
        }

        public void OnDestroy()
        {
        }
    }
}
