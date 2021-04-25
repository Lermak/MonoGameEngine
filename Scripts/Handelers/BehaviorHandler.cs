using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Linq;

namespace MonoGame_Core.Scripts
{
    public class BehaviorHandler
    {
        Dictionary<string, Behavior> behaviors;
        public Dictionary<string, Behavior> Behaviors { get { return behaviors; } }

        public Behavior GetBehavior(string t)
        {
            return Behaviors[t];
        }

        public List<Behavior> GetBehaviorsOfType(string type)
        {
            List<Behavior> bl = new List<Behavior>();

            foreach(Behavior b in behaviors.Values)
            {
                if (b.Type == type)
                {
                    bl.Add(b);
                }
            }

            return bl;
        }

        public BehaviorHandler()
        {
            behaviors = new Dictionary<string, Behavior>();
        }

        public Behavior AddBehavior(Behavior b)
        {
            behaviors.Add(b.Name, b);

            return behaviors[b.Name];
        }

        public void Inizilize()
        {
            Behaviors.OrderBy(b => b.Value.UpdateOrder);
        }

        public void Update(float gt)
        {
            if (SceneManager.SceneState == SceneManager.State.Running)
            {
                foreach (Behavior b in Behaviors.Values)
                {
                    b.Update(gt);
                }
            }
        }

        public void OnDestroy()
        {
            foreach(Behavior b in Behaviors.Values)
            {
                b.OnDestroy();
            }
            Behaviors.Clear();
        }
    }
}
