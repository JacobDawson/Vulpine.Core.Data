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
    public class SortListArrayTests : SortListTests
    {
        #region Test API...

        private SortListArray<String> my_list;

        protected override dynamic TestObject
        {
            get { return my_list; }
        }

        protected override void SetTestObjectInternal(IEnumerable<string> data)
        {
            my_list = new SortListArray<String>(data);
        }

        #endregion

        #region Tests...

        [TestCase("Renamon")]
        [TestCase("Gatomon")]
        [TestCase("Agumon")]
        public void Insert_ListAtCapacity_CapacityIncreses(string item)
        {
            SetTestObject(TestData);
            int before, after;

            before = TestObject.Capacity = TestData.Length;
            TestObject.Insert(item);
            after = TestObject.Capacity;

            Assert.That(after, Is.GreaterThan(before),
                "Capacity did not increase upon insert. ");
        }

        #endregion
    }
}
