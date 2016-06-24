using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Vulpine.Core.Data.Tables;

namespace Vulpine_Core_Data_Tests.Unit.Dictionaries
{
    [TestFixture]
    public class TableClosedTests : TableTests
    {
        private TableClosed<String, Int32?> my_table;

        protected override dynamic TestObject
        {
            get { return my_table; }
        }

        protected override void SetTestObjectInternal(IEnumerable<string> data)
        {
            my_table = new TableClosed<string, int?>();
            foreach (var s in data) my_table.Add(s, s.Length);
        }
    }
}
