using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Vulpine_Core_Data_Tests.Unit.Collections
{
    public abstract class ListTests : CollectionTests
    {
        [Test]
        public void ToString_FilledList_HasFirstItem()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string first = TestData.First();

            Assert.That(output, Contains.Substring(first));
        }

        [Test]
        public void ToString_FilledList_HasLastItem()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string last = TestData.Last();

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

            string element = TestData[n];
            int? index = TestObject.IndexOf(element);

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

            string expected = TestData[n];
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

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void SetItem_NewItem_ContainsItemAtIndex(int n)
        {
            SetTestObject(TestData);

            TestObject.SetItem(n, "Renamon");
            string result = TestObject.GetItem(n);

            Assert.That(result, Is.EqualTo("Renamon"));
        }

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void SetItem_NewItem_OldValueOverwritten(int n)
        {
            SetTestObject(TestData);

            string original = TestObject.GetItem(n);
            TestObject.SetItem(n, "Renamon");

            Assert.That((object)TestObject, Has.None.EqualTo(original));
        }

        [Test]
        public void SetItem_InvalidIndex_ThrowsException()
        {
            SetTestObject(TestData);
            var test = new TestDelegate(
                SetItem_InvalidIndex_ExceptionThrown_Internal);
            var type = typeof(ArgumentOutOfRangeException);
            Assert.That(test, Throws.Exception.TypeOf(type));
            
        }

        private void SetItem_InvalidIndex_ExceptionThrown_Internal()
        {
            TestObject.SetItem(200, "Renamon");
        }

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void Insert_NewItem_ContainsItemAtIndex(int n)
        {
            SetTestObject(TestData);

            TestObject.Insert(n, "Renamon");
            string result = TestObject.GetItem(n);

            Assert.That(result, Is.EqualTo("Renamon"));
        }

        [TestCase(4)]
        [TestCase(7)]
        [TestCase(22)]
        public void Insert_NewItem_OldValueRetained(int n)
        {
            SetTestObject(TestData);

            string original = TestObject.GetItem(n);
            TestObject.Insert(n, "Renamon");

            Assert.That((object)TestObject, Contains.Item(original));
        }

        [Test]
        public void Insert_InvalidIndex_ItemAtLastIndex()
        {
            SetTestObject(TestData);

            TestObject.Insert(100, "Renamon");
            string result = TestObject.GetItem(-1);

            Assert.That(result, Is.EqualTo("Renamon"));
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
