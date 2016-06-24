using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Vulpine_Core_Data_Tests.Unit.Collections
{
    public abstract class CollectionTests : TestBase
    {
        #region Tests...

        [Test]
        public void Count_EmptyCollection_ReturnsZero()
        {
            SetTestObject(EmptyData);

            int count = TestObject.Count;

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void Count_FilledCollection_ReturnsDataSize()
        {
            SetTestObject(TestData);

            int count = TestObject.Count;
            int expected = TestData.Length;

            Assert.That(count, Is.EqualTo(expected));
        }

        [Test]
        public void Contains_EmptyCollection_ReturnsFalse()
        {
            SetTestObject(EmptyData);
            bool contains = TestObject.Contains("Mario");

            Assert.That(contains, Is.False,
                "Did not expect to find the item \"Mario\" using the Contains() method.");
        }

        [TestCase("Mario")]
        [TestCase("Luigi")]
        [TestCase("Zelda")]
        public void Contains_HasItem_ReturnsTrue(string item)
        {
            SetTestObject(TestData);
            bool contains = TestObject.Contains(item);

            Assert.That(contains, Is.True,
                "Expected to find the item \"{0}\" using the Contains() method.",
                item);
        }

        [Test]
        public void Contains_DoseNotHaveItem_ReturnsFalse()
        {
            SetTestObject(TestData);
            bool contains = TestObject.Contains("Renamon");

            Assert.That(contains, Is.False,
                "Did not expect to find the item \"Renamon\" using the Contains() method.");
        }

        [Test]
        public void Add_EmptyCollection_ContainsItem()
        {
            SetTestObject(EmptyData);

            TestObject.Add("Renamon");

            Assert.That((object)TestObject, Contains.Item("Renamon"));
        }

        [Test]
        public void Add_EmptyCollection_CountIncreases()
        {
            SetTestObject(EmptyData);
            int before, after;

            before = TestObject.Count;
            TestObject.Add("Renamon");
            after = TestObject.Count;

            Assert.That(after, Is.EqualTo(before + 1),
                "Count did not increase after insertion.");
        }

        [TestCase("Renamon")]
        [TestCase("Gatomon")]
        [TestCase("Agumon")]
        public void Add_FilledCollection_ContainsItem(string item)
        {
            SetTestObject(TestData);

            TestObject.Add(item);

            Assert.That((object)TestObject, Contains.Item(item));
        }

        [TestCase("Renamon")]
        [TestCase("Gatomon")]
        [TestCase("Agumon")]
        public void Add_FilledCollection_CountIncreases(string item)
        {
            SetTestObject(TestData);
            int before, after;

            before = TestObject.Count;
            TestObject.Add(item);
            after = TestObject.Count;

            Assert.That(after, Is.EqualTo(before + 1),
                "Count did not increase after insertion.");
        }

        [Test]
        public void Add_NullValue_ExceptionThrown()
        {
            SetTestObject(EmptyData);
            var test = new TestDelegate(
                Add_NullValue_ExceptionThrown_Internal);
            var type = typeof(ArgumentNullException);
            Assert.That(test, Throws.Exception.TypeOf(type));
        }

        private void Add_NullValue_ExceptionThrown_Internal()
        {
            TestObject.Add((string)null);
        }

        [Test]
        public void Dequeue_EmptyCollection_ReturnsNull()
        {
            SetTestObject(EmptyData);

            string item = TestObject.Dequeue();

            Assert.That(item, Is.Null);
        }

        [Test]
        public void Dequeue_FilledCollection_ItemRemoved()
        {
            SetTestObject(TestData);

            string item = TestObject.Dequeue();

            Assert.That((object)TestObject, Has.None.EqualTo(item));
        }

        [Test]
        public void Dequeue_FilledCollection_CountDecreases()
        {
            SetTestObject(TestData);
            int before, after;

            before = TestObject.Count;
            TestObject.Dequeue();
            after = TestObject.Count;

            Assert.That(after, Is.EqualTo(before - 1),
                "Count did not decrease after dequeueing.");
        }

        [Test]
        public void Dequeue_Singleton_IsEmpty()
        {
            SetTestObject(Singleton);

            TestObject.Dequeue();

            Assert.That((object)TestObject, Is.Empty);
        }

        [Test]
        public void Clear_EmptyCollection_IsEmpty()
        {
            SetTestObject(EmptyData);

            TestObject.Clear();

            Assert.That((object)TestObject, Is.Empty);
        }

        [Test]
        public void Clear_FilledCollection_IsEmpty()
        {
            SetTestObject(TestData);

            TestObject.Clear();

            Assert.That((object)TestObject, Is.Empty);
        }

        #endregion

    }
}
