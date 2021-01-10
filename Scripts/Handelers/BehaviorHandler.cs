using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Linq;

<<<<<<< HEAD
namespace MonoGame_Core.Scripts
=======
namespace GEJam.Scripts
>>>>>>> c1b8f6f68bc0e41355e957b11df0ccaba139105d
{
    public class BehaviorHandler
    {
        public Dictionary<string, Behavior> Behaviors;

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
