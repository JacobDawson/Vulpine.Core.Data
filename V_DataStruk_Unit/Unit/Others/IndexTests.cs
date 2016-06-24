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
    public class IndexTests
    {
        private dynamic MakeIndex(int index)
        {
            return new Index(index);
        }

        [Test]
        public void GetIndex_IndexGreatorThanSize_ReutrnsNull()
        {
            dynamic index = MakeIndex(101);
            int? output = index.GetIndex(100);

            Assert.That(output, Is.Null);
        }

        [Test]
        public void GetIndex_IndexBetweenZeroAndSize_ReutrnsIndex()
        {
            dynamic index = MakeIndex(50);
            int? output = index.GetIndex(100);

            Assert.That(output, Is.EqualTo(50));
        }

        [Test]
        public void GetIndex_IndexBetweenNegSizeAndZero_ReturnsModIndex()
        {
            dynamic index = MakeIndex(-25);
            int? output = index.GetIndex(100);

            Assert.That(output, Is.EqualTo(75));      
        }

        [Test]
        public void GetIndex_IndexLessThanNegSize_ReturnsNull()
        {
            dynamic index = MakeIndex(-101);
            int? output = index.GetIndex(100);

            Assert.That(output, Is.Null);
        }
    }
}
