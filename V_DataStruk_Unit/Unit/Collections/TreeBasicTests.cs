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
    public class TreeBasicTests : TreeTests
    {
        #region Test API...

        private TreeBasic<String> my_tree;

        protected override dynamic TestObject
        {
            get { return my_tree; }
        }

        protected override void SetTestObjectInternal(IEnumerable<string> data)
        {
            my_tree = new TreeBasic<String>();
            foreach (var item in data) my_tree.Add(item);
        }

        #endregion
    }
}
