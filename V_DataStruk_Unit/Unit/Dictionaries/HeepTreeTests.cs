using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data.Heeps;

namespace Vulpine_Core_Data_Tests.Unit.Dictionaries
{
    [TestFixture]
    public class HeepTreeTests : HeepTests
    {
        private HeepTree<string, int?> my_heep;

        protected override dynamic TestObject
        {
            get { return my_heep; }
        }

        protected override void SetTestObjectInternal(IEnumerable<string> data)
        {
            my_heep = new HeepTree<string, int?>();
            foreach (var s in data) my_heep.Add(s, s.Length);
        }
    }
}
