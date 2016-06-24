using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data;

namespace Vulpine_Core_Data_Tests.Unit.Dictionaries
{
    public abstract class DictionaryTests : TestBase
    {
        [Test]
        public void Count_EmptyDictionary_ReturnsZero()
        {
            SetTestObject(EmptyData);

            int count = TestObject.Count;

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]
        public void Count_FilledDictionary_ReturnsDataSize()
        {
            SetTestObject(TestData);

            int count = TestObject.Count;
            int expected = TestData.Length;

            Assert.That(count, Is.EqualTo(expected));
        }

        [Test]
        public void HasKey_EmptyDictionary_ReturnsFalse()
        {
            SetTestObject(EmptyData);

            bool ret = TestObject.HasKey("Mario");

            Assert.That(ret, Is.False,
                "Did not expect to find the key \"Mario\" in the table.");
        }

        [TestCase("Mario")]
        [TestCase("Fox")]
        [TestCase("Bowser")]
        public void HasKey_KeyInDictionary_ReturnsTrue(string key)
        {
            SetTestObject(TestData);

            bool ret = TestObject.HasKey(key);

            Assert.That(ret, Is.True,
                "Expected to find the key \"{0}\" in the table.", key);
        }

        [Test]
        public void HasKey_KeyNotInDictionary_ReturnsFalse()
        {
            SetTestObject(TestData);

            bool ret = TestObject.HasKey("Renamon");

            Assert.That(ret, Is.False,
                "Did not expect to find the key \"Renamon\" in the table.");
        }

        [Test]
        public void Add_EmptyDictionary_ContainsKey()
        {
            SetTestObject(EmptyData);

            TestObject.Add("Renamon", 7);
            var fake = new KeyedItem<string, int?>("Renamon");

            Assert.That((object)TestObject, Contains.Item(fake));
        }

        [Test]
        public void Add_EmptyDictionary_CountIncreases()
        {
            SetTestObject(EmptyData);
            int before, after;

            before = TestObject.Count;
            TestObject.Add("Renamon", 7);
            after = TestObject.Count;

            Assert.That(after, Is.EqualTo(before + 1),
                "Count did not increase after insertion.");
        }

        [TestCase("Renamon")]
        [TestCase("Gatomon")]
        [TestCase("Agumon")]
        public void Add_FilledDictionary_ContainsKey(string item)
        {
            SetTestObject(TestData);

            TestObject.Add(item, item.Length);
            var fake = new KeyedItem<string, int?>(item);

            Assert.That((object)TestObject, Contains.Item(fake));
        }

        [TestCase("Renamon")]
        [TestCase("Gatomon")]
        [TestCase("Agumon")]
        public void Add_FilledDictionary_CountIncreases(string item)
        {
            SetTestObject(TestData);
            int before, after;

            before = TestObject.Count;
            TestObject.Add(item, item.Length);
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
            TestObject.Add("Xyzzy", (int?)null);
        }

        [Test]
        public void Clear_EmptyDictionary_IsEmpty()
        {
            SetTestObject(EmptyData);

            TestObject.Clear();

            Assert.That((object)TestObject, Is.Empty);
        }

        [Test]
        public void Clear_FilledDictionary_IsEmpty()
        {
            SetTestObject(TestData);

            TestObject.Clear();

            Assert.That((object)TestObject, Is.Empty);
        }


    }
}
