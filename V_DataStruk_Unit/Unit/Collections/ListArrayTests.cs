using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data.Lists;

namespace Vulpine_Core_Data_Tests.Unit.Collections
{
    [TestFixture]
    public class ListArrayTests : ListTests
    {
        #region Test API...

        private VListArray<String> my_list; 

        protected override dynamic TestObject
        {
            get { return my_list; }
        }

        protected override void SetTestObjectInternal(IEnumerable<string> data)
        {
            my_list = new VListArray<String>(data);
        }

        #endregion

        #region Tests...

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void Insert_ListAtCapacity_CapacityIncreses(int index)
        {
            SetTestObject(TestData);
            int before, after;

            before = TestObject.Capacity = TestData.Length;
            TestObject.Insert(index, "Renamon");
            after = TestObject.Capacity;

            Assert.That(after, Is.GreaterThan(before),
                "Capacity did not increase upon insert. ");
        }

        #endregion
    }
}
