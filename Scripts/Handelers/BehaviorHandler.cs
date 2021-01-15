using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Linq;

namespace MonoGame_Core.Scripts
{
    public class BehaviorHandler
    {
        public Dictionary<string, Behavior> Behaviors;

        public Behavior GetBehavior(string t)
        {
            return Behaviors[t];
        }

        public BehaviorHandler()
        {
            Behaviors = new Dictionary<string, Behavior>();
        }

        public void AddBehavior(string tag, Behavior b)
        {
            Behaviors.Add(tag, b);
        }

        public void Inizilize()
        {
            Behaviors.OrderBy(b => b.Value.UpdateOrder);
        }

        public void Update(GameTime gt)
        {
            foreach(Behavior b in Behaviors.Values)
            {
                b.Update(gt);
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
