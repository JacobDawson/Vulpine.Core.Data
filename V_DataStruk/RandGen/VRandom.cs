using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vulpine.Core.Data.Exceptions;

namespace Vulpine.Core.Data.RandGen
{
    /// <summary>
    /// This is the parent class for all psudo-random number generators (PRNGs). It is
    /// ment to be a replacement for System.Random which is not the most ideal PRNG
    /// for all situations. It also provides convience methods for generating numbers
    /// in a particular range, rolling dice, and other common uses. Implementing classes
    /// need only to overide the abstract methods.
    /// </summary>
    /// <remarks>Last Update: 2016-02-10</remarks>
    public abstract class VRandom
    {
        #region Class Definitions...

        //used in generating normal distriubtion values
        private double last_norm = 0.0;
        private bool has_norm = false;

        //rembers the seed for the PRNG
        protected int seed;

        /// <summary>
        /// Generates a string representation of the current random number generator,
        /// using the seed value to uniquly identify the generator.
        /// </summary>
        /// <returns>The random number generator as a string</returns>
        public override string ToString()
        {
            return "RNG:" + seed.ToString("X8");
        }

        /// <summary>
        /// Determins if this random number generator equals another object. Two RNGs
        /// are considered equal, if they are of the same type and have the same seed.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if the objects are equal</returns>
        public override bool Equals(object obj)
        {
            VRandom rng = obj as VRandom;
            if (rng == null) return false;

            Type t1 = this.GetType();
            Type t2 = rng.GetType();

            return t1.Equals(t2) && seed.Equals(rng.seed); 
        }

        /// <summary>
        /// Gets a sudo-unique hash value for the current random number generator.
        /// This is just the seed for the generator.
        /// </summary>
        /// <returns>A sudo-unique hash value</returns>
        public override int GetHashCode()
        {
            return seed;
        }

        #endregion ////////////////////////////////////////////////////////

        #region Class properties...

        /// <summary>
        /// Represents the 32-bit seed value used to initialise this
        /// particular instance of Random.
        /// </summary>
        public int Seed
        {
            get { return seed; }
        }

        #endregion ////////////////////////////////////////////////////////

        #region Abstract Methods...

        /// <summary>
        /// Generates a psudo-random 32-bit value, that is between the
        /// maximum and minimum values for a 32-bit interger.
        /// </summary>
        /// <returns>A psudo-random interger</returns>
        public abstract int NextInt();

        /// <summary>
        /// Generates a psudo-random floating-point value that is in
        /// between 0.0 inclusive, and 1.0 exclusive.
        /// </summary>
        /// <returns>A psudo-random double</returns>
        public abstract double NextDouble();

        /// <summary>
        /// Resets the random number generator to the state that it was
        /// in when it was first initialised with its seed.
        /// </summary>
        public abstract void Reset();

        #endregion ////////////////////////////////////////////////////////

        #region Random Number Generation...

        /// <summary>
        /// Generates a random boolen value, with a close-to one to
        /// one chance of being either true or false.
        /// </summary>
        /// <returns>A psudo-random boolean value</returns>
        public bool RandBool()
        {
            int next = RandInt(2);
            return (next == 0);
        }

        /// <summary>
        /// Generates a psudo-random ingerger value between zero and the given
        /// maximum, but not including the maximum value.
        /// </summary>
        /// <param name="max">Maximum vlaue to generate</param>
        /// <returns>A psudo-random value within the given bounds</returns>
        public int RandInt(int max)
        {
            //checks that the maximum value is positive
            if (max < 1) return 0;

            //grabs a random positive value
            int r = NextInt() & Int32.MaxValue;

            //uses the high order bits to generate the spread
            int x = (Int32.MaxValue / max) + 1;
            return r / x;
        }

        /// <summary>
        /// Generates a psudo-random interger value betwen two given bounds,
        /// including the lower bound, but excluding the upper bound. It swaps
        /// the bounds if given out of order.
        /// </summary>
        /// <param name="low">Lower bound, inclued in range</param>
        /// <param name="high">Upper bound, excluded from range</param>
        /// <returns>A psudo-random value within the given bounds</returns>
        public int RandInt(int low, int high)
        {
            //checks for default case
            if (low == high) return low;

            if (low > high)
            {
                //swaps the values and rolls again
                return RandInt(high, low);
            }
            else
            {
                //grabs a random positive value
                int r = NextInt() & Int32.MaxValue;

                //uses the high order bits to generate the spread
                int x = (Int32.MaxValue / (high - low)) + 1;
                return (r / x) + low;
            }
        }

        /// <summary>
        /// Generates a random, positive, floating-point value up to, but not
        /// including, the given maximum.
        /// </summary>
        /// <param name="rand">Generator for random numbers</param>
        /// <param name="max">Maximum vlaue to generate</param>
        /// <returns>A psudo-random value within the given bounds</returns>
        public double RandDouble(double max)
        {
            //simply multiply by our maximum
            return NextDouble() * max;
        }

        /// <summary>
        /// Generates a random floating-point vlaue between two given bounds,
        /// excluding the posiblility of returning the upper bound. The bracket
        /// is reversed if given out of order.
        /// </summary>
        /// <param name="low">Lower bound, inclued in range</param>
        /// <param name="high">Upper bound, excluded from range</param>
        /// <returns>A psudo-random value within the given bounds</returns>
        public double RandDouble(double low, double high)
        {
            if (low > high)
            {
                //swaps the values and rolls again
                return RandDouble(high, low);
            }
            else
            {
                //computes our random number
                double next = this.NextDouble();
                return ((next * (high - low)) + low);
            }
        }

        /// <summary>
        /// Generates random byte values, up to the count specified. If a negitive
        /// value is given, it will generate values indefinitaly.
        /// </summary>
        /// <param name="count">Number of bytes to generate</param>
        /// <returns>An enumeration of random bytes</returns>
        public IEnumerable<Byte> RandBytes(int count)
        {
            //must generate atleast one byte
            if (count == 0) yield break;

            //used in generating the bytes
            byte[] bits;
            int next;
            int n = 0;

            while (true)
            {
                //grabs the next four bytes
                next = NextInt();
                bits = BitConverter.GetBytes(next);

                for (int i = 0; i < 4; i++)
                {
                    yield return bits[i]; n++;
                    if (count > 0 && n >= count) yield break;
                }
            }
        }

        /// <summary>
        /// Generates a random value with Standard Normal Distribution, that is
        /// a normal distribution with a mean of 0 and a standard diviaiton of 1.
        /// </summary>
        /// <returns>A random value with normal distribution</returns>
        public double RandNormal()
        {
            if (has_norm)
            {
                has_norm = false;
                return last_norm;
            }
            else
            {
                //grabs two uniform random variables
                double u1 = NextDouble();
                double u2 = NextDouble();

                //computes the polar cordinates
                double r = Math.Sqrt(-2.0 * Math.Log(u1));
                double t = Math.PI * 2.0 * u2;

                has_norm = true;
                last_norm = r * Math.Cos(t);
                return r * Math.Sin(t);
            }
        }

        /// <summary>
        /// Generates a random value with normal distribution, given the mean value
        /// and standard diviation of the distribution.
        /// </summary>
        /// <param name="mean">Mean of the distribution</param>
        /// <param name="sd">Standard diviation of the distribution</param>
        /// <returns>A random value with normal distribution</returns>
        public double RandNormal(double mean, double sd)
        {
            double n = RandNormal();
            return n * sd + mean;
        }

        /// <summary>
        /// Simulates rolling a set of dice, each with a given number of sides,
        /// and summing the values of all the dice plus a given offset.
        /// </summary>
        /// <param name="dice">Number of dice to roll</param>
        /// <param name="sides">Number of sides per die</param>
        /// <param name="offset">Offset to the sum</param>
        /// <returns>The total value of the dice roll</returns>
        public int RollDice(int dice, int sides, int offset)
        {
            //clamps the number of sides and dice
            if (sides < 1) sides = 1;
            if (dice < 1) dice = 1;

            //used in calculating the sum
            int x = (255 / sides) + 1;
            int sum = offset;

            //uses one byte for each die rolled
            foreach (byte b in RandBytes(dice))
            {
                sum += ((int)b / x) + 1;
            }

            return sum;
        }

        #endregion ////////////////////////////////////////////////////////

    }
}
