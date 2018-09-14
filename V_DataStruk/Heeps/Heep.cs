/**
 *  This file is an integral part of the Vulpine Core Library
 *  Copyright (c) 2016-2018 Benjamin Jacob Dawson
 *
 *      http://www.jakesden.com/corelibrary.html
 *
 *  The Vulpine Core Library is free software; you can redistribute it 
 *  and/or modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License, or (at your option) any later version.
 *
 *  The Vulpine Core Library is distributed in the hope that it will 
 *  be useful, but WITHOUT ANY WARRANTY; without even the implied 
 *  warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 *  See the GNU Lesser General Public License for more details.
 *
 *      https://www.gnu.org/licenses/lgpl-2.1.html
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.Heeps
{
    /// <summary>
    /// A Heep, also known as a priority queue, is a queue-like data structure that
    /// contains items with a given priority. The items can then be dequeued from
    /// the Heep in order from highest to lowest priority. What constitutes the highest
    /// priority is dependent on weather the heap is a min-heap or a max-heap. In a
    /// min-heap the lowest valued key has the highest priority. Think of it as the
    /// number one item having higher priority than the number two item. In a max-heap
    /// this is reversed, the highest valued key has the highest priority. Although
    /// it is possible to have different items with the same priority in the heap, no
    /// guarantee is made about their relative order. Only one of the highest priority
    /// items is guaranteed to be removed at any given time.
    /// </summary>
    /// <typeparam name="K">Key type of the heap</typeparam>
    /// <typeparam name="E">Element type of the heap</typeparam>
    /// <remarks>Last Update: 2016-06-21</remarks>
    public abstract class Heep<K, E> : VDictionary<K, E>
        where K : IComparable<K>
    {
        #region Class Definitions...

        //determines if the heap is a max-heap or a min-heap
        protected bool maxheep;

        /// <summary>
        /// Generates a string representation of the heap, displaying the size 
        /// of the heap, along with the top-most item and its priority.
        /// </summary>
        /// <returns>The heap as a string</returns>
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
        /// Determines if this is a min-heap or a max-heap. It returns true
        /// in the case of a max-heap, and false in case of a min-heap.
        /// </summary>
        public bool IsMax
        {
            get { return maxheep; }
        }

        /// <summary>
        /// Represents the highest priority item in the heap. What constitutes the
        /// highest priority is dependent on whether it is a min-heap or a max-heap.
        /// </summary>
        public abstract E TopItem { get; }

        /// <summary>
        /// Represents the highest priority in the entire heap. This can be seen as
        /// the priority of the heap itself. What constitutes the highest priority 
        /// is dependent on whether it is a min-heap or a max-heap.
        /// </summary>
        public abstract K TopKey { get; }

        #endregion ///////////////////////////////////////////////////////////////////

        #region Abstract Methods...

        /// <summary>
        /// Removes the top-most item from the heap and returns its value. What 
        /// constitutes the top-most item is dependent on whether it is a min-heap 
        /// or a max-heap. It returns null if the heap is empty.
        /// </summary>
        /// <returns>The highest priority item, or null if empty</returns>
        public abstract E Dequeue();

        /// <summary>
        /// Removes the top-most item from the heap and returns both its value
        /// and its priority. What constitutes the top-most item is dependent on 
        /// whether it is a min-heap or a max-heap.
        /// </summary>
        /// <param name="key">The priority of the item removed</param>
        /// <returns>The highest priority item, or null if empty</returns>
        public abstract E Dequeue(out K key);

        #endregion ///////////////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method used to compare one pair of items to another. Only
        /// the keys of the items are used, the values are ignored. It inverts
        /// the results of comparison in case of a max-heap.
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
