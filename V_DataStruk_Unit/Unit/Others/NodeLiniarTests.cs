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
    public class NodeLiniarTests
    {
        private dynamic MakeNode(string value)
        {
            return new NodeLiniar<String>(value);
        }

        [Test]
        public void ToString_SingleNode_ContainsA()
        {
            dynamic nodeA;
            string output;

            nodeA = MakeNode("Andy");
            output = nodeA.ToString();

            Assert.That(output, Contains.Substring("Andy"));
        }

        [Test]
        public void ToString_LinkedNode_ContainsB()
        {
            dynamic nodeA, nodeB;
            string output;

            nodeA = MakeNode("Andy");
            nodeB = MakeNode("Bob");
            nodeA.Next = nodeB;
            output = nodeA.ToString();

            Assert.That(output, Contains.Substring("Bob"));
        }

        [Test]
        public void Next_SetNode_NodeUpdated()
        {
            dynamic nodeA, nodeB, nodeC;

            nodeA = MakeNode("A");
            nodeB = MakeNode("B");

            nodeA.Next = nodeB;
            nodeC = nodeB.Prev;

            Assert.That((object)nodeC, Is.EqualTo(nodeA),
                "The recripial link for NodeB was not set!");
        }

        [Test]
        public void Prev_SetNode_NodeUpdated()
        {
            dynamic nodeA, nodeB, nodeC;

            nodeA = MakeNode("A");
            nodeB = MakeNode("B");

            nodeA.Prev = nodeB;
            nodeC = nodeB.Next;

            Assert.That((object)nodeC, Is.EqualTo(nodeA),
                "The recripial link for NodeB was not set!");
        }

        [Test]
        public void ListChildren_SingleNode_Empty()
        {
            dynamic nodeA;

            nodeA = MakeNode("A");
            var list = nodeA.ListChildren();

            Assert.That(list, Is.Empty);
        }

        [Test]
        public void ListChildren_LinkedNode_ContainsLink()
        {
            dynamic nodeA, nodeB;

            nodeA = MakeNode("A");
            nodeB = MakeNode("B");

            nodeA.Next = nodeB;
            var list = nodeA.ListChildren();

            //Assert.That(list, Has.Some.EqualTo(nodeB));
            Assert.That(list, Contains.Item(nodeB));
        }

    }
}
