using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data.Queues;

namespace Vulpine_Core_Data_Tests.Unit.Collections
{
    [TestFixture]
    public class DequeLinkedTests : DequeTests
    {
        #region Test API...

        private DequeLinked<String> my_list;

        protected override dynamic TestObject
        {
            get { return my_list; }
        }

        protected override void SetTestObjectInternal(IEnumerable<string> data)
        {
            my_list = new DequeLinked<String>(data);
        }

        #endregion
    }
}
