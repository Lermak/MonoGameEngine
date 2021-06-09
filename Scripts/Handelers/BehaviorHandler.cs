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
            public Component[] Components;
            public Act Run;

            public Behavior(Component[] c, Act a)
            {
                Components = c;
                Run = a;
            }
        }

        Dictionary<string, Behavior> behaviors;
        GameObject gameObject;
        public Dictionary<string, Behavior> Behaviors { get { return behaviors; } }
        public GameObject GameObject { get { return gameObject; } }

        public Behavior GetBehavior(string t)
        {
            return Behaviors[t];
        }

        //public List<Behavior> GetBehaviorsOfType(string type)
        //{
        //    List<Behavior> bl = new List<Behavior>();

        //    foreach (Behavior b in behaviors.Values)
        //    {
        //        if (b.Type == type)
        //        {
        //            bl.Add(b);
        //        }
        //    }

        //    return bl;
        //}

        public BehaviorHandler(GameObject go)
        {
            gameObject = go;
            behaviors = new Dictionary<string, Behavior>();
        }

        public void AddBehavior(string name, Act b, Component[] c)
        {
            behaviors[name] = new Behavior(c, b);
        }

        public void Inizilize()
        {
        }

        public void Update(float gt)
        {
            if (SceneManager.SceneState == SceneManager.State.Running)
            {
                foreach (Behavior b in Behaviors.Values)
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
