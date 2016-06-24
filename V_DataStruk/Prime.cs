using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data
{
    /// <summary>
    /// Contains a collection of static methods involving prime numbers. Examples
    /// include the generation of prime numbers and tests for primamilty.
    /// </summary>
    /// <remarks>Last Update: 2016-06-20</remarks>
    public static class Prime
    {
        /// <summary>
        /// Generates the next prime number after a given number. That is,
        /// the smallest prime number strictly larger than the given number.
        /// </summary>
        /// <param name="n">Starting number</param>
        /// <returns>The next prime after the starting number</returns>
        public static int NextPrime(int n)
        {
            //checks for the base case
            if (n < 2) return 2;

            //used in loop
            int test = n;
            bool prime = false;

            //corrects for even starting positions
            if (test % 2 == 0) test = test - 1;

            while (!prime)
            {
                //obtains the next potential prime
                test = test + 2;
                prime = IsPrime(test);
            }

            return test;
        }

        /// <summary>
        /// Determins if a given number is prime or composet. It returns
        /// true if the number is prime and false if it is composet.
        /// </summary>
        /// <param name="n">Number to test</param>
        /// <returns>True if the number is prime</returns>
        public static bool IsPrime(int n)
        {
            //checks for base cases
            if (n < 2) return false;
            if (n == 2) return true;

            //used in the test loop
            int sqrt = (int)Math.Sqrt(n) + 1;
            bool prime = true;

            //checks each number less than the square root
            for (int x = 2; x <= sqrt; x++)
            {
                if (n % x == 0)
                { 
                    prime = false; 
                    break; 
                }
            }

            return prime;
        }
    }
}
