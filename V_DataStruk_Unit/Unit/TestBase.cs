using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Vulpine_Core_Data_Tests.Unit
{
    public abstract class TestBase
    {
        /// <summary>
        /// Test data used to initialise collections prior to testing them.
        /// </summary>
        protected static readonly string[] TestData = {
            "Mario", "Pikachu", "Bowser", "Peach", "Yoshi", "Donkey Kong", 
            "Captain Falcon", "Fox", "Ness", "Ice Climbers", "Kirby",
            "Samus", "Zelda", "Link", "Jigglypuff", "Mewtwo", "Luigi",
            "Marth", "Mr. Game & Watch", "Dr. Mario", "Gannondorf",
            "Young Link", "Falco", "Pichu", "Roy"
        };

        /// <summary>
        /// Contains the same items as the test data, but in sorted order.
        /// </summary>
        protected static readonly string[] SortedData = {
            "Bowser", "Captain Falcon", "Donkey Kong", "Dr. Mario", "Falco",
            "Fox", "Gannondorf", "Ice Climbers", "Jigglypuff", "Kirby",
            "Link", "Luigi", "Mario", "Marth", "Mewtwo", "Mr. Game & Watch",
            "Ness", "Peach", "Pichu", "Pikachu", "Roy", "Samus", "Yoshi",
            "Young Link", "Zelda"
        };

        /// <summary>
        /// An empty array, usefull for creating empty test objects. 
        /// </summary>
        protected static readonly string[] EmptyData = { };

        /// <summary>
        /// An array containing a single item, required for some tests.
        /// </summary>
        protected static readonly string[] Singleton = { "Renamon" };

        /// <summary>
        /// Provides access to the object we are testing. The object being
        /// tested should be reset before each test. To set the test object
        /// see the options below...
        /// </summary>
        protected abstract dynamic TestObject { get; }

        /// <summary>
        /// This method is responcable for setting the text object up with the
        /// given test data. A new test object should be created for each
        /// invocation. Tests should not call this method directly, but rather
        /// the SetTestObject method.
        /// </summary>
        /// <param name="data">Data with which to initialse the test object</param>
        protected abstract void SetTestObjectInternal(IEnumerable<String> data);

        /// <summary>
        /// This method should be called by tests to setup the internal state
        /// of the test object prior to testing. This mehtod must be called once
        /// for each test, otherwise the test object could wind up in an
        /// inconsistent state.
        /// </summary>
        /// <param name="data">Data with which to initialse the test object</param>
        protected void SetTestObject(IEnumerable<String> data)
        {
            try
            {
                //calls the internal method
                SetTestObjectInternal(data);
            }
            catch (Exception e)
            {
                Console.WriteLine("UNABLE TO INITALIZE TEST DATA!!! ");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine(e.StackTrace);

                Assert.Inconclusive("UNABLE TO INITALIZE TEST DATA!!! ");
            }
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                //makes shure we dispose of the test object
                TestObject.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to dispose of TestObject.");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
