using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data.Queues;

namespace Vulpine_Core_Data_Tests.Unit.Collections
{
    public abstract class DequeTests : CollectionTests
    {
        [Test]
        public void ToString_FilledDeque_HasFirstItem()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string first = TestData.First();

            Assert.That(output, Contains.Substring(first));
        }

        [Test]
        public void ToString_FilledDeque_HasLastItem()
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
        public void PushBack_FilledDeque_UpdatesBack()
        {
            SetTestObject(TestData);

            TestObject.PushBack("Renamon");
            string result = TestObject.Back;

            Assert.That(result, Is.EqualTo("Renamon"));
        }

        [Test]
        public void PushBack_EmptyDeque_UpdatesBack()
        {
            SetTestObject(EmptyData);

            TestObject.PushBack("Renamon");
            string result = TestObject.Back;

            Assert.That(result, Is.EqualTo("Renamon"));
        }

        [Test]
        public void PushFront_FilledDeque_UpdatesFront()
        {
            SetTestObject(TestData);

            TestObject.PushFront("Renamon");
            string result = TestObject.Front;

            Assert.That(result, Is.EqualTo("Renamon"));
        }

        [Test]
        public void PushFront_EmptyDeque_UpdatesFront()
        {
            SetTestObject(EmptyData);

            TestObject.PushFront("Renamon");
            string result = TestObject.Front;

            Assert.That(result, Is.EqualTo("Renamon"));
        }

        [Test]
        public void PopBack_FilledDeque_ItemRemoved()
        {
            SetTestObject(TestData);

            string item = TestObject.PopBack();

            Assert.That((object)TestObject, Has.None.EqualTo(item));
        }

        [Test]
        public void PopBack_Singleton_IsEmpty()
        {
            SetTestObject(Singleton);

            TestObject.PopBack();

            Assert.That((object)TestObject, Is.Empty);
        }

        [Test]
        public void PopFront_FilledDeque_ItemRemoved()
        {
            SetTestObject(TestData);

            string item = TestObject.PopFront();

            Assert.That((object)TestObject, Has.None.EqualTo(item));
        }

        [Test]
        public void PopFront_Singleton_IsEmpty()
        {
            SetTestObject(Singleton);

            TestObject.PopFront();

            Assert.That((object)TestObject, Is.Empty);
        }


    }
}
