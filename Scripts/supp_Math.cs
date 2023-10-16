using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Core.Scripts
{
    public static class supp_Math
    {
        /// <summary>
        /// icnredibly hacky implementation of NextFloat() borrowed from stack https://stackoverflow.com/a/3365388/7416252
        /// <br/>
        /// <b>this is experimental and is in no way guaranteed to be safe</b> <i>(use at own risk, -Blake)</i>
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float NextFloat(Random random, int min, int max)
        {
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            // choose -149 instead of -126 to also generate subnormal floats (*)
            double exponent = Math.Pow(2.0, random.Next(min, max));
            return (float)(mantissa * exponent);
        }
    }
}