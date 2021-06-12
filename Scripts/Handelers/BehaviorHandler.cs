using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Linq;

namespace MonoGame_Core.Scripts
{
    public class BehaviorHandler
    {
        public delegate void Act(float uo, Component[] c);
        public struct Behavior
        {
            public string Name;
            public Component[] Components;
            public Act Run;

            public Behavior(string name, Component[] c, Act a)
            {
                Name = name;
                Components = c;
                Run = a;
            }
        }

        List<Behavior> behaviors;
        GameObject gameObject;
        public List<Behavior> Behaviors { get { return behaviors; } }
        public GameObject GameObject { get { return gameObject; } }

        public Behavior GetBehavior(string t)
        {
            return behaviors.Where(b => b.Name == t).First();
        }

        public BehaviorHandler(GameObject go)
        {
            gameObject = go;
            behaviors = new List<Behavior>();
        }

        public void AddBehavior(string name, Act b, Component[] c)
        {
            behaviors.Add(new Behavior(name, c, b));
        }

        public void Inizilize()
        {
        }

        public void Update(float gt)
        {
            if (SceneManager.SceneState == SceneManager.State.Running)
            {
                foreach (Behavior b in Behaviors)
                {
                    b.Run(gt, b.Components);
                }
            }
        }

        public void OnDestroy()
        {
        }
    }
}
