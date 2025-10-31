using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace borktorial
{
    /// <summary>
    /// Quick 'n Dirty Floating Point Noise Engine
    /// </summary>
    public static class noiser
    {
        /// <summary>
        /// The actual noise generator. 
        /// Probably sucks. Can't bother fixing it
        /// </summary>
        /// <param name="scl">The scale</param>
        /// <param name="randness">The effect the randomness has on it</param>
        /// <param name="rand">The random thing</param>
        /// <param name="c">Magic</param>
        /// <returns>A number determined by complete dark magic and your parameters</returns>
        public static float generate(float scl, float randness, Random rand, float c)
        {
            float rBase = rand.NextSingle() * scl;
            float exol = (rBase / randness)*c;
            double combo = rBase * exol;
            double rCombo = rBase / exol;
            double cCombo = combo * rCombo;
            double magic = Math.Sin((cCombo/c) * 1000);
            return (float)magic;
        }
    }
}
