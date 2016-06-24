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
    public class ListLinkedTests : ListTests
    {
        #region Test API...

        private VListLinked<String> my_list;

        protected override dynamic TestObject
        {
            get { return my_list; }
        }

        protected override void SetTestObjectInternal(IEnumerable<string> data)
        {
            my_list = new VListLinked<String>(data);
        }

        #endregion

        #region Tests...

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void GetItem_ValidIndex_LastIndexUpdated(int index)
        {
            SetTestObject(TestData);

            TestObject.GetItem(index);
            int current = TestObject.LastIndex;

            Assert.That(current, Is.EqualTo(index));
        }

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void Insert_NewItem_LastIndexUpdated(int index)
        {
            SetTestObject(TestData);

            TestObject.Insert(index, "Renamon");
            int current = TestObject.LastIndex;

            Assert.That(current, Is.EqualTo(index));
        }

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void RemoveAt_ValidIndex_LastIndexUpdated(int index)
        {
            SetTestObject(TestData);

            TestObject.RemoveAt(index);
            int current = TestObject.LastIndex;

            Assert.That(current, Is.EqualTo(index));
        }

        #endregion
    }
}
