using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Core.Scripts
{
    public class AnimationHandler
    {
        private GameObject gameObject;
        private SpriteRenderer spriteRenderer;
        public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }
        public GameObject GameObject { get { return gameObject; } }

        public AnimationHandler(GameObject go)
        {
            gameObject = go;
        }
    }
}
