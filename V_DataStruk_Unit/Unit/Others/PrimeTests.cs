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
        
    }
}
