using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoGame_Core.Scripts
{
    public static class Coroutines
    {
        public static IEnumerator<bool> FadeInSceneTransision()
        {
            float speed = 510;
            while (Globals.RenderingManager.GlobalFade > 0)
            {
                Globals.RenderingManager.GlobalFade -= speed * TimeManager.DeltaTime;
                SoundManager.SetGlobalVolume(SoundManager.GlobalVolume + speed / 255 * TimeManager.DeltaTime); 
                if (Globals.RenderingManager.GlobalFade < 0)
                {
                    Globals.RenderingManager.GlobalFade = 0;
                    SceneManager.SceneState = SceneManager.State.Running;
                }
                yield return false;
            }
            yield return true;
        }
        public static IEnumerator<bool> FadeOutSceneTransision()
        {
            float speed = 510;
            while (Globals.RenderingManager.GlobalFade < 255)
            {
                Globals.RenderingManager.GlobalFade += speed * TimeManager.DeltaTime;
                SoundManager.SetGlobalVolume(SoundManager.GlobalVolume - speed / 255 * TimeManager.DeltaTime);
                if (Globals.RenderingManager.GlobalFade > 255)
                {
                    Globals.RenderingManager.GlobalFade = 255;
                    SceneManager.CurrentScene = null;
                }
                yield return false;
            }

            yield return true;
        }

        public static IEnumerator<bool> CreditScroll(float speed, Transform creditTransform, float height, RigidBody creditRigidbody)
        {
            creditRigidbody.MoveVelocity = new Vector2(0, speed);
            Vector2 startPos = creditTransform.Position;
            while (creditTransform.Position.Y < startPos.Y + height)
            {
                yield return false;
            }

            creditRigidbody.MoveVelocity = new Vector2(0, 0);
            yield return true;
        }
        public static IEnumerator<bool> Shake(float duration, int min, int max, SpriteRenderer s)
        {
            float timeElapsed = 0;
            Random r = new Random();
            int dir = -1;
            while (timeElapsed < duration)
            {
                if (SceneManager.SceneState == SceneManager.State.Running)
                {
                    timeElapsed += TimeManager.DeltaTime;
                    s.DrawOffset = (new Vector2(r.Next(min, max) * dir, 1));
                    dir *= -1;
                }
                yield return false;
            }
            s.DrawOffset = new Vector2();
            yield return true;
        }
        public static IEnumerator<bool> Reload(ItemCombatData cd)
        {
            float timeElapsed = 0;
            cd.Reloading = true;
            while (timeElapsed < 1)
            {
                if (SceneManager.SceneState == SceneManager.State.Running)
                {
                    timeElapsed += TimeManager.DeltaTime * cd.ReloadSpeed;
                }
                yield return false;
            }
            cd.Reloading = false;
            yield return true;
        }

        public static IEnumerator<bool> ModifyShipTransition(WorldObject player)
        {
            ShipData sd = (ShipData)player.GetComponent("ShipData");
            float timeElapsed = 0;
            sd.ReturnRads = player.Transform.Radians;
            sd.ReturnPos = player.Transform.Position;
            sd.ReturnScale = player.Transform.Scale;
            float duration = .25f;

            sd.MyState = ShipData.ShipState.Animating;
            while (timeElapsed < duration)
            {
                if (SceneManager.SceneState == SceneManager.State.Running)
                {
                    timeElapsed += TimeManager.DeltaTime;
                    player.Transform.Radians = sd.ReturnRads - sd.ReturnRads * timeElapsed / duration;
                    player.Transform.SetPosition(sd.ReturnPos - sd.ReturnPos * timeElapsed / duration);
                    player.Transform.Scale = sd.ReturnScale + (new Vector2(1,1) - sd.ReturnScale) * timeElapsed / duration;
                }
                yield return false;
            }
            sd.MyState = ShipData.ShipState.Sorting;
            yield return true;
        }
        public static IEnumerator<bool> PlayGameTransition(WorldObject player)
        {
            ShipData sd = (ShipData)player.GetComponent("ShipData");
            float timeElapsed = 0;
            float duration = .25f;

            sd.MyState = ShipData.ShipState.Animating;

            while (timeElapsed < duration)
            {
                if (SceneManager.SceneState == SceneManager.State.Running)
                {
                    timeElapsed += TimeManager.DeltaTime;
                    player.Transform.Radians = sd.ReturnRads * timeElapsed / duration;
                    player.Transform.SetPosition(sd.ReturnPos * timeElapsed / duration);
                    player.Transform.Scale = new Vector2(1, 1) - (new Vector2(1, 1) - sd.ReturnScale) * timeElapsed / duration;
                }
                yield return false;
            }
            sd.MyState = ShipData.ShipState.Playing;
            yield return true;
        }

    }
}
