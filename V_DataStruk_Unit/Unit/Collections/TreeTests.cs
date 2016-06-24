using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Vulpine_Core_Data_Tests.Unit.Collections
{
    public abstract class TreeTests : CollectionTests
    {
        [Test]
        public void ToString_FilledTree_ContainsCount()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string count = TestObject.Count.ToString();

            Assert.That(output, Contains.Substring(count));
        }

        [Test]
        public void ToString_FilledTree_ContainsDepth()
        {
            SetTestObject(TestData);

            string output = TestObject.ToString();
            string depth = TestObject.Depth.ToString();

            Assert.That(output, Contains.Substring(depth));
        }

        [Test]
        public void Depth_EmptyTree_ReturnsZero()
        {
            SetTestObject(EmptyData);

            int depth = TestObject.Depth;

            Assert.That(depth, Is.EqualTo(0),
                "The depth of the empty tree was not zero.");
        }

        [Test]
        public void Depth_FilledTree_LessThanCount()
        {
            SetTestObject(TestData);

            int depth = TestObject.Depth;
            int max = TestData.Length;

            Assert.That(depth, Is.LessThanOrEqualTo(max),
                "The depth of the tree was greator than the upper bouund.");
        }

        [Test]
        public void Depth_FilledTree_GreatorThanLogN()
        {
            SetTestObject(TestData);

            int depth = TestObject.Depth;
            int min = (int)Math.Log(TestData.Length + 1, 2.0);

            Assert.That(depth, Is.GreaterThanOrEqualTo(min),
                "The depth of the tree was less than the lower bouund.");
        }

        [TestCase("Renamon")]
        [TestCase("Gatomon")]
        [TestCase("Agumon")]
        public void Add_NewItem_TreeSorted(string item)
        {
            SetTestObject(TestData);

            TestObject.Add(item);

            Assert.That((object)TestObject, Is.Ordered);
        }

        [Test]
        public void Retreve_EmptyTree_ReturnsNull()
        {
            SetTestObject(EmptyData);

            string output = TestObject.Retreve("Renamon");

            Assert.That(output, Is.Null);
        }

        [TestCase("Mario")]
        [TestCase("Luigi")]
        [TestCase("Zelda")]
        public void Retreve_HasItem_ReturnsItem(string item)
        {
            SetTestObject(TestData);

            string output = TestObject.Retreve(item);

            Assert.That(item, Is.EqualTo(item));
        }

        [Test]
        public void Retreve_DoseNotHaveItem_ReturnsNull()
        {
            SetTestObject(TestData);

            string output = TestObject.Retreve("Renamon");

            Assert.That(output, Is.Null);
        }

        [Test]
        public void GetMinMax_Maximum_ReturnsMax()
        {
            SetTestObject(TestData);

            string result = TestObject.GetMinMax(true);
            string expected = TestData.Max();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetMinMax_Minimum_ReturnsMin()
        {
            SetTestObject(TestData);

            string result = TestObject.GetMinMax(false);
            string expected = TestData.Min();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void RemoveMinMax_Maximum_MaxRemoved()
        {
            SetTestObject(TestData);

            TestObject.RemoveMinMax(true);
            string target = TestData.Max();

            Assert.That((object)TestObject, Has.None.EqualTo(target));
        }

        [Test]
        public void RemoveMinMax_Minimum_MinRemoved()
        {
            SetTestObject(TestData);

            TestObject.RemoveMinMax(false);
            string target = TestData.Min();

            Assert.That((object)TestObject, Has.None.EqualTo(target));
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
