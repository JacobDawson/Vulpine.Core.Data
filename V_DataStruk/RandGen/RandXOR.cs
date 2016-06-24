using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.RandGen
{

    /// <summary>
    /// The XOR-Shift Register is a rather small but fast PRNG, offering a long period
    /// of 2^128 - 1. It is reported to pass all of the DieHard tests, but fails some
    /// of the BigCrush tests. The shift values used in this particular Shift Register
    /// were listed in a paper by Geroge Marsaglia.
    /// </summary>
    /// <remarks>Last Update: 2016-02-06</remarks>
    public class RandXOR : VRandom
    {
        #region Class Definitions...

        //stores the internal state of the XOR-Shift register
        private uint x, y, z, w;

        //remembers the initial seed
        private int seed;

        /// <summary>
        /// Constructs a new PRGN using the current system time
        /// as the intinal seed value.
        /// </summary>
        public RandXOR()
        {
            int time = (int)(DateTime.Now.Ticks % Int32.MaxValue);
            this.seed = time;
            Init(time);
        }

        /// <summary>
        /// Constructs a new PRGN useing the desired seed.
        /// </summary>
        /// <param name="seed">The initial seed</param>
        public RandXOR(int seed)
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

        #region The XOR-Shift Register...

        /// <summary>
        /// Uses the 32-bit seed to mutate the starting 128-bit internal
        /// state of the XOR-Shift Register.
        /// </summary>
        /// <param name="seed">The initial seed</param>
        private void Init(int seed)
        {
            x = unchecked((uint)seed);

            y = x ^ 0x9FA7B2E7U;
            z = x ^ 0x51DF156EU;
            w = x ^ 0x10702939U;
        }

        /// <summary>
        /// Generates a random 32-bit interger.
        /// </summary>
        /// <returns>A psudo-random int</returns>
        private int Generate()
        {
            uint temp = x ^ (x << 11);
            temp = temp ^ (temp >> 8);

            x = y;
            y = z;
            z = w;

            w = w ^ (w >> 19);
            w = w ^ temp;

            return unchecked((int)w);
        }

        #endregion /////////////////////////////////////////////////////////////////

    }
}
