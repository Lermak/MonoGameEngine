using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public static class CoroutineManager
    {
        public enum CoroutineState { Paused, Running }
        public struct Coroutine
        {
            public string Name;
            public CoroutineState State;
            public IEnumerator<bool> Routine;

            public Coroutine(IEnumerator<bool> routine, string name, bool start)
            {
                Name = name;
                if (start)
                    State = CoroutineState.Running;
                else
                    State = CoroutineState.Paused;
                Routine = routine;
            }
        }

        static Dictionary<string, Coroutine> coroutines = new Dictionary<string, Coroutine>();
        public static void Clear()
        {
            coroutines.Clear();
        }

        public static void AddCoroutine(IEnumerator<bool> coroutine, string name, bool start)
        {
            coroutines[name] = new Coroutine(coroutine, name, start);
        }
        public static bool IsRunning(string coroutine)
        {
            return coroutines.ContainsKey(coroutine) && coroutines[coroutine].State == CoroutineState.Running;
        }

        public static void Pause(string coroutine)
        {
            if (coroutines.ContainsKey(coroutine))
            {
                Coroutine c = coroutines[coroutine];
                c.State = CoroutineState.Paused;
                coroutines[coroutine] = c;
            }
        }

        public static void Start(string coroutine)
        {
            if (coroutines.ContainsKey(coroutine))
            {
                Coroutine c = coroutines[coroutine];

                c.State = CoroutineState.Running;

                coroutines[coroutine] = c;
            }
        }

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

        public static void Update(float gt)
        {
            List<string> k = new List<string>(coroutines.Keys);
            List<string> toRemove = new List<string>();

            for (int i = 0; i < coroutines.Count; ++i)
            {
                Coroutine c = coroutines[k[i]];
                if (c.State == CoroutineState.Running)
                {
                    c.Routine.MoveNext();
                    if (c.Routine.Current)
                    {
                        toRemove.Add(c.Name);
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
