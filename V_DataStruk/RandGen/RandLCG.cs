using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.RandGen
{
    /// <summary>
    /// The Linear Congruential Generator is one of the simplest kind of PRNG availabe.
    /// Dispite it's simplicity it often dose well on tests, and is provided as the
    /// default generator for many programing packages. Thus, it has become somewhat
    /// of a standard by witch other PRNGs are judged. This particular generator uses
    /// the paramaters spesified by the Minimal Standard Generator.
    /// </summary>
    /// <remarks>Last Update: 2016-02-03</remarks>
    public class RandLCG : VRandom
    {
        #region Class Definitions...

        //the paramaters that define the PRGN
        private const int M = 2147483647;
        private const int A = 48271;
        private const int Q = M / A;
        private const int R = M % A;
        
        //stores the state of the PRGN
        private int state;
        private int seed;

        /// <summary>
        /// Constructs a new PRGN using the current system time
        /// as the intinal seed value.
        /// </summary>
        public RandLCG()
        {
            int time = (int)(DateTime.Now.Ticks % Int32.MaxValue);
            this.seed = time;
            this.state = time;
        }

        /// <summary>
        /// Constructs a new PRGN useing the desired seed.
        /// </summary>
        /// <param name="seed">The initial seed</param>
        public RandLCG(int seed)
        {
            this.seed = seed;
            this.state = seed & Int32.MaxValue;
        }

        /// <summary>
        /// Represents the 32-bit seed value used to initialise this
        /// particular instance of Random.
        /// </summary>
        public override int Seed
        {
            get { return seed; }
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Random Implementation...

        /// <summary>
        /// Generates a psudo-random value that is between zero
        /// and the maximum value for 32-bit integers.
        /// </summary>
        /// <returns>A psudo-random interger</returns>
        public override int NextInt()
        {
            unchecked
            {
                state = A * (state % Q) - R * (state / Q);
                if (state <= 0) state += M;
            }

            return state;
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

        #endregion ///////////////////////////////////////////////////////////////
    }
}
