using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data.Heeps;

namespace Vulpine_Core_Data_Tests.Unit.Dictionaries
{
    [TestFixture]
    public class HeepArrayTests : HeepTests
    {
        #region Test API...

        private HeepArray<string, int?> my_heep;

        protected override dynamic TestObject
        {
            get { return my_heep; }
        }

        protected override void SetTestObjectInternal(IEnumerable<string> data)
        {
            my_heep = new HeepArray<string, int?>();
            foreach (var s in data) my_heep.Add(s, s.Length);
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
            TestObject.Add(item, item.Length);
            after = TestObject.Capacity;

            Assert.That(after, Is.GreaterThan(before),
                "Capacity did not increase upon insert. ");
        }

        #endregion
    }
}
