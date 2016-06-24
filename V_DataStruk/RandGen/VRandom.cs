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
        #region Abstract Methods...

        /// <summary>
        /// Represents the 32-bit seed value used to initialise this
        /// particular instance of Random.
        /// </summary>
        public abstract int Seed { get; }

        /// <summary>
        /// Generates a psudo-random floating-point value that is in
        /// between 0.0 inclusive, and 1.0 exclusive.
        /// </summary>
        /// <returns>A psudo-random double</returns>
        public abstract double NextDouble();

        /// <summary>
        /// Generates a psudo-random value that is between zero
        /// and the maximum value for 32-bit integers.
        /// </summary>
        /// <returns>A psudo-random interger</returns>
        public abstract int NextInt();

        #endregion ////////////////////////////////////////////////////////

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
        /// <exception cref="ArgRangeExcp">If the maximum value is less 
        /// than one</exception>
        public int RandInt(int max)
        {
            //checks that the maximum value is positive
            ArgRangeExcp.Atleast("max", max, 1);

            //uses the high order bits to generate the spread
            int x = (Int32.MaxValue / max) + 1;
            return NextInt() / x;
        }

        /// <summary>
        /// Generates a psudo-random interger value betwen two given bounds,
        /// including the lower bound, but excluding the upper bound.
        /// </summary>
        /// <param name="low">Lower bound, inclued in range</param>
        /// <param name="high">Upper bound, excluded from range</param>
        /// <returns>A psudo-random value within the given bounds</returns>
        /// <exception cref="ArgRangeExcp">If the upper bound is lower
        /// than the lower bound</exception>
        public int RandInt(int low, int high)
        {
            //checks that our high paramater is larger than our low paramater
            ArgRangeExcp.Atleast("high", high, low + 1);

            //uses the high order bits to generate the spread
            int x = (Int32.MaxValue / (high - low)) + 1;
            return (NextInt() / x) + low;
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
        /// Simulates rolling a set of dice, each with a given number of sides,
        /// and summing the values of all the dice.
        /// </summary>
        /// <param name="dice">Number of dice to roll</param>
        /// <param name="sides">Number of sides per die</param>
        /// <returns>The total value of the dice roll</returns>
        /// <exception cref="ArgRangeExcp">If the number of dice or sides is
        /// less than one or greator than 255</exception>
        public int RollSum(int dice, int sides)
        {
            ArgRangeExcp.Check("dice", dice, 1, 255);
            ArgRangeExcp.Check("sides", sides, 1, 255);

            int sum = 0;

            for (int i = 0; i < dice; i++)
            sum += RandInt(sides) + 1;

            return sum;
        }

        /// <summary>
        /// Simulates rolling a set of dice, each with a given number of sides,
        /// and summing the values of all the dice.
        /// </summary>
        /// <param name="dice">Number of dice to roll</param>
        /// <param name="sides">Number of sides per die</param>
        /// <returns>An array containing the results of each die, plus
        /// the total sum of all the dice</returns>
        /// <exception cref="ArgRangeExcp">If the number of dice or sides is
        /// less than one or greator than 255</exception>
        public int[] RollDice(int dice, int sides)
        {
            //requires that the dice and sides are between 1 and 256
            ArgRangeExcp.Check("dice", dice, 1, 255);
            ArgRangeExcp.Check("sides", sides, 1, 255);

            //creates an array to store the results plus sum
            int[] res = new int[dice + 1];
            int sum = 0;

            //rolls the dice
            for (int i = 0; i < dice; i++)
            {
                res[i] = RandInt(sides) + 1;
                sum = sum + res[i];
            }

            //includes the sum with the results
            res[dice] = sum;
            return res;
        }

        /// <summary>
        /// Generates an array of random byte values.
        /// </summary>
        /// <param name="count">Number of bytes to generate</param>
        /// <returns>A psudo-random array of bytes</returns>
        public byte[] GetBytes(int count)
        {
            //creates an array for the new bytes
            byte[] data = new byte[count];

            //we use the top 8 bits, and ignore the lower 23
            for (int i = 0; i < count; i++)
            data[i] = (byte)(NextInt() >> 23);

            return data;
        }

    }
}
