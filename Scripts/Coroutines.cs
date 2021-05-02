using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public static class Coroutines
    {
        public static IEnumerator<bool> FadeInSceneTransision()
        {
            while (RenderingManager.GlobalFade > 0)
            {
                RenderingManager.GlobalFade -= 128 * TimeManager.DeltaTime;

                if (RenderingManager.GlobalFade < 0)
                {
                    RenderingManager.GlobalFade = 0;
                    SceneManager.SceneState = SceneManager.State.Running;
                }
                yield return false;
            }
            yield return true;
        }
        public static IEnumerator<bool> FadeOutSceneTransision()
        {
            while (RenderingManager.GlobalFade < 255)
            {
                RenderingManager.GlobalFade += 128 * TimeManager.DeltaTime;
                if (RenderingManager.GlobalFade > 255)
                {
                    RenderingManager.GlobalFade = 255;
                    SceneManager.CurrentScene = null;
                }
                yield return false;
            }

            yield return true;
        }
    }
}
