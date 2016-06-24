using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.RandGen
{

    /// <summary>
    /// This is a homebrew PRNG based on the Rule 30 Celuluar Atomaton. As sutch, it
    /// has not be rigerously tested. The Rule 30 Atomaton is known for it's caotic
    /// behavior and it's sensitive dependence on initial conditions. It seems to
    /// preform well in most of the DieHard tests, dispite the obvious fractal
    /// pattern of it's bit sequence.
    /// </summary>
    /// <remarks>Last Update: 2016-02-07</remarks>
    public class RandR30 : VRandom
    {
        #region Class Definitions...

        //stores the world space for the Rule 30 atomata
        uint x1, y1, z1, w1;

        //remembers the initial seed
        private int seed;

        /// <summary>
        /// Constructs a new PRGN using the current system time
        /// as the intinal seed value.
        /// </summary>
        public RandR30()
        {
            int time = (int)(DateTime.Now.Ticks % Int32.MaxValue);
            this.seed = time;
            Init(time);
        }

        /// <summary>
        /// Constructs a new PRGN useing the desired seed.
        /// </summary>
        /// <param name="seed">The initial seed</param>
        public RandR30(int seed)
        {
            this.seed = seed;
            Init(seed);
        }

        /// <summary>
        /// Represents the 32-bit seed value used to initialise this
        /// particular instance of Random.
        /// </summary>
        public override int Seed
        {
            get { return seed; }
        }

        #endregion /////////////////////////////////////////////////////////////////

        #region Random Implementation...

        /// <summary>
        /// Generates a psudo-random value that is between zero
        /// and the maximum value for 32-bit integers.
        /// </summary>
        /// <returns>A psudo-random interger</returns>
        public override int NextInt()
        {
            //enshures our output is between 0 and MaxValue
            return Generate() & Int32.MaxValue;
        }

        /// <summary>
        /// Generates a psudo-random floating-point value that is in
        /// between 0.0 inclusive, and 1.0 exclusive.
        /// </summary>
        /// <returns>A psudo-random double</returns>
        public override double NextDouble()
        {
            //builds a double in the interval [1, 2) then shifts to [0, 1)
            long bits = ((long)NextInt() << 21) | (0x3FFL << 52);
            return BitConverter.Int64BitsToDouble(bits) - 1.0;
        }

        #endregion /////////////////////////////////////////////////////////////////

        #region The Rule 30 Celuluar Atomaton...

        /// <summary>
        /// Initailised the world state with the given seed.
        /// </summary>
        /// <param name="seed">The initial seed</param>
        private void Init(int seed)
        {
            x1 = unchecked((uint)seed);

            y1 = x1 ^ 0x1E4952DAU;
            z1 = x1 ^ 0x0B8CDF48U;
            w1 = x1 ^ 0xEF4BB1BDU;
        }

        /// <summary>
        /// Generates a hash of the world state after updating, this is
        /// used as the output of the generator.
        /// </summary>
        /// <returns>A psudo-random interger</returns>
        private int Generate()
        {
            Update();
            uint h = x1;

            unchecked
            {
                h ^= (h << 5) + (h >> 2) + y1;
                h ^= (h << 5) + (h >> 2) + z1;
                h ^= (h << 5) + (h >> 2) + w1;
            }

            return unchecked((int)h);
        }

        /// <summary>
        /// Updates the world state of the atomaton.
        /// </summary>
        private void Update()
        {         
            //rolls the 128-bit world stade one to the left and right
            //causing the world to wrap around itself like a ring
            uint x0 = (x1 << 1) | (w1 >> 31);
            uint x2 = (x1 >> 1) | (y1 << 31);

            uint y0 = (y1 << 1) | (x1 >> 31);
            uint y2 = (y1 >> 1) | (z1 << 31);

            uint z0 = (z1 << 1) | (y1 >> 31);
            uint z2 = (z1 >> 1) | (w1 << 31);

            uint w0 = (w1 << 1) | (z1 >> 31);
            uint w2 = (w1 >> 1) | (x1 << 31);

            //updates the world state to the next itteration
            x1 = (~x0 & x2) | (~x0 & x1) | (x0 & ~x1 & ~x2);
            y1 = (~y0 & y2) | (~y0 & y1) | (y0 & ~y1 & ~y2);
            z1 = (~z0 & z2) | (~z0 & z1) | (z0 & ~z1 & ~z2);
            w1 = (~w0 & w2) | (~w0 & w1) | (w0 & ~w1 & ~w2);
        }

        #endregion /////////////////////////////////////////////////////////////////
    }
}
