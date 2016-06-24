using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.Heeps
{
    /// <summary>
    /// A Heep, also known as a priority queue, is a queue-like data structor that
    /// contains items with a given priority. The items can then be dequeued from
    /// the Heep in order from highest to lowest priority. What constitutes the highest
    /// priority is dependent on weather the heep is a min-heep or a max-heep. In a
    /// min-heep the lowest valued key has the highest priority. Think of it as the
    /// number one item having higher priority than the number two item. In a max-heep
    /// this is reversed, the highest valued key has the highest priority. Although
    /// it is possable to have diffrent items with the same priority in the heep, no
    /// garentee is made about their relitive order. Only one of the highest priority
    /// items is garenteed to be removed at any given time.
    /// </summary>
    /// <typeparam name="K">Key type of the heep</typeparam>
    /// <typeparam name="E">Element type of the heep</typeparam>
    /// <remarks>Last Update: 2016-06-21</remarks>
    public abstract class Heep<K, E> : VDictionary<K, E>
        where K : IComparable<K>
    {
        #region Class Definitions...

        //determins if the heep is a max-heep or a min-heep
        protected bool maxheep;

        /// <summary>
        /// Generates a string representation of the heep, desplaying the size 
        /// of the heep, along with the top-most item and its priority.
        /// </summary>
        /// <returns>The heep as a string</returns>
        public override string ToString()
        {
            if (this.Empty) return "Empty Heep";

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Heep[{0}] ", Count);
            sb.Append(" rooted at ");
            sb.AppendFormat("<{0}>", TopItem);
            sb.Append(" with priority ");
            sb.AppendFormat("<{0}>", TopKey);

            return sb.ToString();
        }

        #endregion ///////////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// Determins if this is a min-heep or a max-heep. It returns true
        /// in the case of a max-heep, and false in case of a min-heep.
        /// </summary>
        public bool IsMax
        {
            get { return maxheep; }
        }

        /// <summary>
        /// Represents the highest priority item in the heep. What constitutes the
        /// highest priority is dependent on wheather it is a min-heep or a max-heep.
        /// </summary>
        public abstract E TopItem { get; }

        /// <summary>
        /// Represents the highest priority in the entire heep. This can be seen as
        /// the priority of the heep itself. What constitutes the highest priority 
        /// is dependent on wheather it is a min-heep or a max-heep.
        /// </summary>
        public abstract K TopKey { get; }

        #endregion ///////////////////////////////////////////////////////////////////

        #region Abstract Methods...

        /// <summary>
        /// Removes the top-most item from the heep and returns its value. What 
        /// constitutes the top-most item is dependent on wheather it is a min-heep 
        /// or a max-heep. It reutrns null if the heep is empty.
        /// </summary>
        /// <returns>The highest priority item, or null if empty</returns>
        public abstract E Dequeue();

        /// <summary>
        /// Removes the top-most item from the heep and returns both its value
        /// and its priority. What constitutes the top-most item is dependent on 
        /// wheather it is a min-heep or a max-heep.
        /// </summary>
        /// <param name="key">The priority of the item removed</param>
        /// <returns>The highest priority item, or null if empty</returns>
        public abstract E Dequeue(out K key);

        #endregion ///////////////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method used to compare one pair of items to another. Only
        /// the keys of the items are used, the values are ignored. It inverts
        /// the results of comparison in case of a max-heep.
        /// </summary>
        /// <param name="item1">First pair to be compared</param>
        /// <param name="item2">Second pair to be compared</param>
        /// <returns>A negative number if pair1 comes first, a positive number
        /// if it comes second, and zero if they are equal</returns>
        protected int Compare(KeyedItem<K, E> pair1, KeyedItem<K, E> pair2)
        {
            var key1 = pair1.Key;
            var key2 = pair2.Key;

            int comp = key1.CompareTo(key2);
            return (maxheep) ? -comp : comp;
        }

        #endregion ///////////////////////////////////////////////////////////////////
    }
}
