using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data
{
    /// <summary>
    /// Contains a collection of static methods involving prime numbers. Examples
    /// include the generation of prime numbers and tests for primality.
    /// </summary>
    /// <remarks>Last Update: 2016-08-30</remarks>
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
        /// Determines if a given number is prime or composite. It returns
        /// true if the number is prime and false if it is composite.
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

        /// <summary>
        /// A simple method for computing the unique prime factorisation for any natural
        /// number greator that one. It is not garenteed to terminate quickly for the
        /// product of large primes, which is very hard to compute.
        /// </summary>
        /// <param name="num">Number to factor</param>
        /// <returns>The unique prime factors</returns>
        public static IEnumerable<Int32> Factor(int num)
        {
            //checks for valid input
            if (num < 2) yield break;
            int test = 2;

            while (num > 1 && !IsPrime(num))
            {
                //counts the number of divisions
                while (num % test == 0)
                {
                    num = num / test;
                    yield return test;
                }

                //advances to the next prime
                test = NextPrime(test);
            }

            //can't factor any further, remainder is prime
            if (num > 1) yield return num;
        }

        /// <summary>
        /// Uses a modified version of the Sieve of Eratosthenes to compute all the primes
        /// up to a given number. This is more effecient than repeited calls to NextPrime(),
        /// and it can generate all primes up to Int32.MaxValue utilising aproximatly
        /// 90 KB of memory.
        /// </summary>
        /// <param name="max">Maximum prime number</param>
        /// <returns>A List of primes</returns>
        public static IEnumerable<Int32> ListPrimes(int max)
        {
            //checks for valid input
            if (max < 2) yield break;

            //intitialy sets all indicies as prime
            int s = (int)Math.Sqrt(max) + 2;
            bool[] primes = new bool[s];
            bool[] temp = new bool[s];
            for (int n = 0; n < s; n++) primes[n] = true;

            //returns the first primes as they are found
            for (int n = 2; n < s; n++)
            {
                if (!primes[n]) continue;

                yield return n;
                int test = n * n;

                while (test < s)
                {
                    primes[test] = false;
                    test = test + n;
                }
            }

            for (int x = s; x < max; x += s)
            {
                //cleares out the temp array for the next run
                for (int n = 0; n < s; n++) temp[n] = true;

                //fills out the next s primes
                for (int n = 2; n < s; n++)
                {
                    if (!primes[n]) continue;
                    int test = (n * ((x / n) + 1)) - x;

                    while (test < s)
                    {
                        temp[test] = false;
                        test = test + n;
                    }
                }

                //returns the next s primes found
                for (int n = 1; n < s; n++)
                {
                    if (!temp[n]) continue;
                    yield return x + n;
                }
            }
        }
    }
}
