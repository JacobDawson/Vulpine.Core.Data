using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data;

namespace Vulpine_Core_Data_Tests.Unit.Dictionaries
{
    public abstract class HeepTests : DictionaryTests
    {
        [Test]
        public void ToString_FilledHeep_ContainsCount()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string count = TestObject.Count.ToString();

            Assert.That(output, Contains.Substring(count));
        }

        [Test]
        public void ToString_FilledHeep_ContainsMinKey()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string key = TestObject.TopKey.ToString();

            Assert.That(output, Contains.Substring(key));
        }

        [Test]
        public void ToString_FilledHeep_ContainsMinValue()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string value = TestObject.TopItem.ToString();

            Assert.That(output, Contains.Substring(value));
        }

        [Test]
        public void TopKey_EmptyHeep_ReturnsNull()
        {
            SetTestObject(EmptyData);

            string output = TestObject.TopKey;

            Assert.That(output, Is.Null);
        }

        [Test]
        public void TopKey_FilledHeep_ReturnsMinKey()
        {
            SetTestObject(TestData);

            string output = TestObject.TopKey;
            string key = SortedData.First();

            Assert.That(output, Is.EqualTo(key));
        }

        [Test]
        public void TopItem_EmptyHeep_ReturnsNull()
        {
            SetTestObject(EmptyData);

            int? output = TestObject.TopItem;

            Assert.That(output, Is.Null);
        }

        [Test]
        public void TopItem_FilledHeep_RetrunsMinValue()
        {
            SetTestObject(TestData);

            int? output = TestObject.TopItem;
            int? value = SortedData.First().Length;

            Assert.That(output, Is.EqualTo(value));
        }

        [Test]
        public void Dequeue_EmptyHeep_ReturnsNull()
        {
            SetTestObject(EmptyData);

            int? output = TestObject.Dequeue();

            Assert.That(output, Is.Null);
        }

        [Test]
        public void Dequeue_FilledHeep_ReturnsMinValue()
        {
            SetTestObject(TestData);

            int? output = TestObject.Dequeue();
            int? expected = SortedData.First().Length;

            Assert.That(output, Is.EqualTo(expected));
        }

        [Test]
        public void Dequeue_MultInvoke_ItemsInOrder()
        {
            SetTestObject(TestData);

            var keys = new List<String>(TestData.Length);
            string temp = null;

            while (!TestObject.Empty)
            {
                TestObject.Dequeue(out temp);
                keys.Add(temp);
            }

            Assert.That(keys, Is.Ordered);
        }

    }
}
