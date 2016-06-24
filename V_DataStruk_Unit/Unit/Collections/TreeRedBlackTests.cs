using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data.Trees;

namespace Vulpine_Core_Data_Tests.Unit.Collections
{
    [TestFixture]
    public class TreeRedBlackTests : TreeTests
    {
        #region Test API...

        private TreeRedBlack<String> my_tree;

        protected override dynamic TestObject
        {
            get { return my_tree; }
        }

        protected override void SetTestObjectInternal(IEnumerable<string> data)
        {
            my_tree = new TreeRedBlack<String>();
            foreach (var item in data) my_tree.Add(item);
        }

        #endregion

        #region Tests...

        [Test]
        public void Depth_FilledTree_LessThan2LogN()
        {
            SetTestObject(TestData);

            int depth = TestObject.Depth;
            int max = (int)(2.0 * Math.Log(TestData.Length + 1, 2.0));

            Assert.That(depth, Is.LessThanOrEqualTo(max),
                "The depth of the tree was greator than the upper bouund.");
        }

        #endregion
    }
}
