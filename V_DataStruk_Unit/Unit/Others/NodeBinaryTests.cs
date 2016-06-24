using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data;

namespace Vulpine_Core_Data_Tests.Unit.Others
{
    [TestFixture]
    public class NodeBinaryTests
    {
        private dynamic MakeNode(string value)
        {
            return new NodeBinary<String>(value);
        }

        [Test]
        public void ToStirng_LeafNode_ContainsA()
        {
            dynamic nodeA;
            string output;

            nodeA = MakeNode("Andy");
            output = nodeA.ToString();

            Assert.That(output, Contains.Substring("Andy"));
        }

        [Test]
        public void ToString_LeftChildOnly_ContainsB()
        {
            dynamic nodeA, nodeB;
            string output;

            nodeA = MakeNode("Andy");
            nodeB = MakeNode("Bob");
            nodeA.Left = nodeB;
            output = nodeA.ToString();

            Assert.That(output, Contains.Substring("Bob"));
        }

        [Test]
        public void ToString_RightChildOnly_ContainsC()
        {
            dynamic nodeA, nodeC;
            string output;

            nodeA = MakeNode("Andy");
            nodeC = MakeNode("Cat");
            nodeA.Right = nodeC;
            output = nodeA.ToString();

            Assert.That(output, Contains.Substring("Cat"));
        }

        [Test]
        public void ToString_FullNode_ContainsBandC()
        {
            dynamic nodeA, nodeB, nodeC;
            string output;

            nodeA = MakeNode("Andy");
            nodeB = MakeNode("Bob");
            nodeC = MakeNode("Cat");

            nodeA.Left = nodeB;
            nodeA.Right = nodeC;
            output = nodeA.ToString();

            Assert.That(output, Contains.Substring("Bob").And.ContainsSubstring("Cat"));
        }

        [Test]
        public void Left_SetNode_ChildUpdated()
        {
            dynamic nodeA, nodeB, nodeC;

            nodeA = MakeNode("A");
            nodeB = MakeNode("B");

            nodeA.Left = nodeB;
            nodeC = nodeB.Parent;

            Assert.That((object)nodeC, Is.EqualTo(nodeA),
                "The parrent refrence for NodeB was not set!");
        }

        [Test]
        public void Right_SetNode_ChildUpdated()
        {
            dynamic nodeA, nodeB, nodeC;

            nodeA = MakeNode("A");
            nodeB = MakeNode("B");

            nodeA.Right = nodeB;
            nodeC = nodeB.Parent;

            Assert.That((object)nodeC, Is.EqualTo(nodeA),
                "The parrent refrence for NodeB was not set!");
        }

        [Test]
        public void GetSibling_CalledOnLeft_ReturnsRight()
        {
            dynamic nodeA, nodeB, nodeC, result;

            nodeA = MakeNode("A");
            nodeB = MakeNode("B");
            nodeC = MakeNode("C");

            nodeA.Left = nodeB;
            nodeA.Right = nodeC;
            result = nodeB.GetSibling();

            Assert.That((object)result, Is.EqualTo(nodeC));
        }

        [Test]
        public void GetSibling_CalledOnRight_ReturnsLeft()
        {
            dynamic nodeA, nodeB, nodeC, result;

            nodeA = MakeNode("A");
            nodeB = MakeNode("B");
            nodeC = MakeNode("C");

            nodeA.Left = nodeB;
            nodeA.Right = nodeC;
            result = nodeC.GetSibling();

            Assert.That((object)result, Is.EqualTo(nodeB));
        }

        [Test]
        public void GetSibling_CalledOnRoot_ReturnsNull()
        {
            dynamic nodeA, result;

            nodeA = MakeNode("A");
            result = nodeA.GetSibling();

            Assert.That((object)result, Is.Null);
        }

        [Test]
        public void ListChildren_LeafNode_Empty()
        {
            dynamic nodeA;

            nodeA = MakeNode("A");
            var list = nodeA.ListChildren();

            Assert.That(list, Is.Empty);
        }

        [Test]
        public void ListChildren_FullNode_ContainsBandC()
        {
            dynamic nodeA, nodeB, nodeC;

            nodeA = MakeNode("A");
            nodeB = MakeNode("B");
            nodeC = MakeNode("C");

            nodeA.Left = nodeB;
            nodeA.Right = nodeC;
            var list = nodeA.ListChildren();

            Assert.That(list, Contains.Item(nodeB).And.Contains(nodeC));
        }


    }
}
