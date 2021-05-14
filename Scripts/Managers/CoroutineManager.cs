using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    /// <summary>
    /// Class to manage the run cycles of coroutines
    /// </summary>
    public static class CoroutineManager
    {
        /// <summary>
        /// Determines if a coroutine should progress or wait
        /// </summary>
        public enum CoroutineState { Paused, Running }


        public struct Coroutine
        {
            public string Name;
            public CoroutineState State;
            public float TimeSinceLast;
            public float TimeBetweenSteps;
            public IEnumerator<bool> Routine;

            /// <summary>
            /// Create a new coroutine
            /// </summary>
            /// <param name="routine">The enumerator function to iterate through</param>
            /// <param name="name">The coroutine name</param>
            /// <param name="timeBetween">Delay in seconds between iterations</param>
            /// <param name="start">Start immediately</param>
            public Coroutine(IEnumerator<bool> routine, string name, float timeBetween, bool start)
            {
                if (start)
                    State = CoroutineState.Running;
                else
                    State = CoroutineState.Paused;
                Routine = routine;
                Name = name;
                TimeBetweenSteps = timeBetween;
                TimeSinceLast = 0;
            }
        }

        static List<string> keys = new List<string>();
        static Dictionary<string, Coroutine> coroutines = new Dictionary<string, Coroutine>();

        /// <summary>
        /// Remove all coroutines from the list
        /// </summary>
        public static void Clear()
        {
            coroutines.Clear();
        }

        public static void AddCoroutine(IEnumerator<bool> coroutine, string name, float timeBetween, bool start)
        {
            if (!coroutines.ContainsKey(name))
            {
                coroutines[name] = new Coroutine(coroutine, name, timeBetween, start);
            }
        }

        /// <summary>
        /// Check if a coroutine is currently running
        /// </summary>
        /// <param name="coroutine">The name of the coroutine</param>
        /// <returns>true if the coroutine' CoroutineStat is Running</returns>
        public static bool IsRunning(string coroutine)
        {
            return coroutines.ContainsKey(coroutine) && coroutines[coroutine].State == CoroutineState.Running;
        }

        /// <summary>
        /// Changes the named coroutine's CoroutineState to Paused
        /// </summary>
        /// <param name="coroutine">The coroutine's name</param>
        public static void Pause(string coroutine)
        {
            if (coroutines.ContainsKey(coroutine))
            {
                Coroutine c = coroutines[coroutine];
                c.State = CoroutineState.Paused;
                coroutines[coroutine] = c;
            }
        }

        /// <summary>
        /// Changes the named coroutine's CoroutineState to Running
        /// </summary>
        /// <param name="coroutine">The coroutine's name</param>
        public static void Start(string coroutine)
        {
            if (coroutines.ContainsKey(coroutine))
            {
                Coroutine c = coroutines[coroutine];

                c.State = CoroutineState.Running;

                coroutines[coroutine] = c;
            }
        }

        /// <summary>
        /// Removes a coroutine from the list of coroutines
        /// </summary>
        /// <param name="coroutine">The coroutine's name</param>
        public static void Stop(string coroutine)
        {
            if (coroutines.ContainsKey(coroutine))
            {
                coroutines.Remove(coroutine);
            }
        }

        public static void Initilize()
        {
            coroutines = new Dictionary<string, Coroutine>();
        }

        /// <summary>
        /// Iterate through all coroutines that are currently running by one loop, provided there has been enough delay
        /// If a coroutine is finished, remove it from the list
        /// </summary>
        /// <param name="gt">Game Time</param>
        public static void Update(float gt)
        {
            List<string> k = new List<string>(coroutines.Keys);
            List<string> toRemove = new List<string>();

            for (int i = 0; i < coroutines.Count; ++i)
            {
                Coroutine c = coroutines[k[i]];
                if (c.State == CoroutineState.Running)
                {
                    c.TimeSinceLast += gt;
                    if (c.TimeSinceLast > c.TimeBetweenSteps)
                    {
                        c.Routine.MoveNext();
                        if (c.Routine.Current)
                        {
                            toRemove.Add(c.Name);
                        }
                    }
                }

                coroutines[k[i]] = c;
            }

            for(int i = 0; i < toRemove.Count; ++i)
            {
                coroutines.Remove(toRemove[i]);
            }
        }
    }
}
