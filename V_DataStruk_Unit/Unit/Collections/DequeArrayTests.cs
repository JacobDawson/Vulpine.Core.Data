using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data.Queues;

namespace Vulpine_Core_Data_Tests.Unit.Collections
{
    [TestFixture]
    public class DequeArrayTests : DequeTests
    {
        #region Test API...

        private DequeArray<String> my_list;

        protected override dynamic TestObject
        {
            get { return my_list; }
        }

        protected override void SetTestObjectInternal(IEnumerable<string> data)
        {
            my_list = new DequeArray<String>(data);
        }

        #endregion

        #region Tests...

        [Test]
        public void PushBack_DequeAtCapacity_CapacityIncreses()
        {
            SetTestObject(TestData);
            int before, after;

            before = TestObject.Capacity = TestData.Length;
            TestObject.PushBack("Renamon");
            after = TestObject.Capacity;

            Assert.That(after, Is.GreaterThan(before),
                "Capacity did not increase upon insert. ");
        }

        [Test]
        public void PushFront_DequeAtCapacity_CapacityIncreses()
        {
            SetTestObject(TestData);
            int before, after;

            before = TestObject.Capacity = TestData.Length;
            TestObject.PushFront("Renamon");
            after = TestObject.Capacity;

            Assert.That(after, Is.GreaterThan(before),
                "Capacity did not increase upon insert. ");
        }

        #endregion
    }
}
