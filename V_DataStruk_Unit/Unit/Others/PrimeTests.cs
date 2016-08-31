using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data;

namespace Vulpine_Core_Data_Tests.Unit.Others
{
    [TestFixture]
    public class PrimeTests
    {
        private static readonly int[][] factors =
        {
            new int[] {2, 2, 2, 3, 3, 5},
            new int[] {2, 2, 3, 7, 7, 7, 29},
            new int[] {97, 263, 509},
            new int[] {1697, 2389},
        };

        [TestCase(7, 11)]
        [TestCase(100, 101)]
        [TestCase(5000, 5003)]
        [TestCase(524275, 524287)]
        [TestCase(47326695, 47326913)] 
        public void NextPrime_ValidInput_ExpectedOutput(int n, int expected)
        {
            int x = Prime.NextPrime(n);
            Assert.That(x, Is.EqualTo(expected));
        }

        [TestCase(7)]
        [TestCase(4999)]
        [TestCase(536287)]
        public void IsPrime_PrimeNumber_ReturnsTrue(int n)
        {
            bool test = Prime.IsPrime(n);
            Assert.That(test, Is.True);
        }

        [TestCase(12)]
        [TestCase(512461)]
        [TestCase(30376669)]
        public void IsPrime_ComposetNumber_ReturnsFalse(int n)
        {
            bool test = Prime.IsPrime(n);
            Assert.That(test, Is.False);
        }

        [TestCase(360, 0)]
        [TestCase(119364, 1)]
        [TestCase(12985099, 2)]
        [TestCase(4054133, 3)]
        public void Factor_ValidInput_ExpectedResults(int n, int fac)
        {
            var ex = factors[fac];
            var act = Prime.Factor(n).ToArray();

            Assert.That(act, Is.EquivalentTo(ex));
        }





        [Test]
        public void ListPrimes_First1000_ExpectedReults()
        {
            var ex = DataSets.Primes;
            var act = Prime.ListPrimes(7920);

            Assert.That(act, Is.EquivalentTo(ex));
        }
        
    }
}
