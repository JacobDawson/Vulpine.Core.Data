using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data;

namespace Vulpine_Core_Data_Tests.Unit.Dictionaries
{
    public abstract class TableTests : DictionaryTests
    {
        [Test]
        public void ToString_FilledTable_ContainsCount()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string count = TestObject.Count.ToString();

            Assert.That(output, Contains.Substring(count));
        }

        [Test]
        public void ToString_FilledTable_ContainsBuckets()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string buckets = TestObject.Buckets.ToString();

            Assert.That(output, Contains.Substring(buckets));
        }

        [TestCase("Renamon")]
        [TestCase("Gatomon")]
        [TestCase("Agumon")]
        public void Add_FilledTable_KeyMatchesItem(string key)
        {
            SetTestObject(TestData);

            TestObject.Add(key, key.Length);
            int? value = TestObject.GetValue(key);

            Assert.That(value, Is.EqualTo(key.Length));
        }

        [Test]
        public void Add_DuplicateKey_ExceptionThrown()
        {
            SetTestObject(TestData);
            var test = new TestDelegate(
                Add_DuplicateKey_ExceptionThrown_Internal);
            var type = typeof(InvalidOperationException);
            Assert.That(test, Throws.Exception.TypeOf(type));
        }

        private void Add_DuplicateKey_ExceptionThrown_Internal()
        {
            TestObject.Add("Mario", -1);
        }

        [Test]
        public void GetValue_EmptyTable_ReturnsNull()
        {
            SetTestObject(EmptyData);

            int? value = TestObject.GetValue("Mario");

            Assert.That(value, Is.Null);
        }

        [TestCase("Mario")]
        [TestCase("Fox")]
        [TestCase("Bowser")]
        public void GetValue_KeyInTable_ReturnsValue(string key)
        {
            SetTestObject(TestData);

            int? value = TestObject.GetValue(key);
            int? expected = key.Length;

            Assert.That(value, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_KeyNotInTable_ReturnsNull()
        {
            SetTestObject(TestData);

            int? value = TestObject.GetValue("Renamon");

            Assert.That(value, Is.Null);
        }

        [TestCase("Mario")]
        [TestCase("Fox")]
        [TestCase("Bowser")]
        public void Overwrite_KeyInTable_KeyMatchesItem(string key)
        {
            SetTestObject(TestData);

            TestObject.Overwrite(key, -1);
            int? value = TestObject.GetValue(key);

            Assert.That(value, Is.EqualTo(-1));
        }

        [TestCase("Renamon")]
        [TestCase("Gatomon")]
        [TestCase("Agumon")]
        public void Overwrite_KeyNotInTable_KeyMatchesItem(string key)
        {
            SetTestObject(TestData);

            TestObject.Overwrite(key, key.Length);
            int? value = TestObject.GetValue(key);

            Assert.That(value, Is.EqualTo(key.Length));
        }

        [Test]
        public void Overwrite_NullValue_ExceptionThrown()
        {
            SetTestObject(EmptyData);
            var test = new TestDelegate(
                Overwrite_NullValue_ExceptionThrown_Internal);
            var type = typeof(ArgumentNullException);
            Assert.That(test, Throws.Exception.TypeOf(type));
        }

        private void Overwrite_NullValue_ExceptionThrown_Internal()
        {
            TestObject.Overwrite("Mario", (int?)null);
        }

        [TestCase("Mario")]
        [TestCase("Fox")]
        [TestCase("Bowser")]
        public void Remove_KeyInTable_KeyRemoved(string key)
        {
            SetTestObject(TestData);

            TestObject.Remove(key);
            var fake = new KeyedItem<string, int?>(key);

            Assert.That((object)TestObject, Has.None.EqualTo(fake));
        }

        [TestCase("Mario")]
        [TestCase("Fox")]
        [TestCase("Bowser")]
        public void Remove_KeyInTable_ValueReturned(string key)
        {
            SetTestObject(TestData);

            var ret = TestObject.Remove(key);
            int? expected = key.Length;

            Assert.That((object)ret, Is.EqualTo(expected));
        }

        [Test]
        public void Remove_KeyNotInTable_ReturnsNull()
        {
            SetTestObject(TestData);

            var ret = TestObject.Remove("Renamon");

            Assert.That((object)ret, Is.Null);
        }

    }
}
