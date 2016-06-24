using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data.Lists;

namespace Vulpine_Core_Data_Tests.Unit.Collections
{
    public abstract class SortListTests : CollectionTests
    {
        [Test]
        public void ToString_FilledList_HasFirstItem()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string first = SortedData.First();

            Assert.That(output, Contains.Substring(first));
        }

        [Test]
        public void ToString_FilledList_HasLastItem()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string last = SortedData.Last();

            Assert.That(output, Contains.Substring(last));
        }

        [Test]
        public void ToString_SingleItem_HasItem()
        {
            SetTestObject(Singleton);

            string output = TestObject.ToString();
            string single = Singleton[0];

            Assert.That(output, Contains.Substring(single));
        }

        [Test]
        public void IndexOf_EmptyList_ReturnsNull()
        {
            SetTestObject(EmptyData);

            int? index = TestObject.IndexOf("Renamon");

            Assert.That(index, Is.Null);
        }

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void IndexOf_HasItem_ReturnsIndex(int n)
        {
            SetTestObject(TestData);

            string item = SortedData[n];
            int? index = TestObject.IndexOf(item);

            Assert.That(index, Is.EqualTo(n));
        }

        [Test]
        public void IndexOf_DoseNotHaveItem_ReturnsNull()
        {
            SetTestObject(TestData);

            int? index = TestObject.IndexOf("Renamon");

            Assert.That(index, Is.Null);
        }

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void GetItem_ValidIndex_ReturnsItem(int n)
        {
            SetTestObject(TestData);

            string expected = SortedData[n];
            string actual = TestObject.GetItem(n);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetItem_InvalidIndex_ReturnsNull()
        {
            SetTestObject(TestData);

            int index = TestData.Length + 1;
            string output = TestObject.GetItem(index);

            Assert.That(output, Is.Null);
        }

        [TestCase("Renamon")]
        [TestCase("Gatomon")]
        [TestCase("Agumon")]
        public void Insert_NewItem_ItemAtIndex(string item)
        {
            SetTestObject(TestData);

            int? index = TestObject.Insert(item);
            string result = TestObject.GetItem(index.Value);

            Assert.That(result, Is.EqualTo(item));
        }

        [TestCase("Renamon")]
        [TestCase("Gatomon")]
        [TestCase("Agumon")]
        public void Insert_NewItem_ListSorted(string item)
        {
            SetTestObject(TestData);

            TestObject.Insert(item);

            Assert.That((object)TestObject, Is.Ordered);
        }

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void RemoveAt_VaildIndex_ItemRemoved(int n)
        {
            SetTestObject(TestData);

            string output = TestObject.RemoveAt(n);

            Assert.That((object)TestObject, Has.None.EqualTo(output));
        }

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void RemoveAt_ValidIndex_ReturnsOriginal(int n)
        {
            SetTestObject(TestData);

            string original = TestObject.GetItem(n);
            string output = TestObject.RemoveAt(n);

            Assert.That(output, Is.EqualTo(original));
        }

        [Test]
        public void RemoveAt_InvalidIndex_ReturnsNull()
        {
            SetTestObject(TestData);

            string output = TestObject.GetItem(200);

            Assert.That(output, Is.Null);
        }

        [TestCase("Pichu")]
        [TestCase("Roy")]
        [TestCase("Ice Climbers")]
        public void Remove_HasItem_ItemRemoved(string item)
        {
            SetTestObject(TestData);

            TestObject.Remove(item);

            Assert.That((object)TestObject, Has.None.EqualTo(item));
        }

        [TestCase("Pichu")]
        [TestCase("Roy")]
        [TestCase("Ice Climbers")]
        public void Remove_HasItem_ReturnsTrue(string item)
        {
            SetTestObject(TestData);

            bool result = TestObject.Remove(item);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Remove_DoseNotHaveItem_ReturnsFalse()
        {
            SetTestObject(TestData);

            bool result = TestObject.Remove("Renamon");

            Assert.That(result, Is.False);
        }

    }
}
