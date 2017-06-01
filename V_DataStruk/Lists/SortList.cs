/**
 *  This file is an integral part of the Vulpine Core Library: 
 *  Copyright (c) 2016-2017 Benjamin Jacob Dawson. 
 *
 *      http://www.jakesden.com/corelibrary.html
 *
 *  This file is licensed under the Apache License, Version 2.0 (the "License"); 
 *  you may not use this file except in compliance with the License. You may 
 *  obtain a copy of the License at:
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.    
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//SC

namespace Vulpine.Core.Data.Lists
{
    /// <summary>
    /// A Sorted List is a list that maintains a sorted order at all times. 
    /// Because of this, it is not possible to insert or set items at 
    /// arbitrary indices, as this would disrupt the sorted order. Because 
    /// the list is sorted, search times for items in the list are greatly
    /// reduced. Duplicate items may be inserted into a sorted list, however 
    /// their exact handling is left unspecified. In addition, it is possible 
    /// to specify an alternate comparison method, used to determine the 
    /// sorted order of the list.
    /// </summary>
    /// <typeparam name="E">The element type of the list</typeparam>
    /// <remarks>Last Update: 2016-05-29</remarks>
    public abstract class SortList<E> : VCollection<E>, Indexed<E> 
        where E : IComparable<E>
    {
        #region Class Definitions...

        //stores a reference to the size of the list
        protected int size;

        //stores a reference to the method of comparison
        protected Comparison<E> comparer;

        /// <summary>
        /// Generates a string representation of the list, displaying it's
        /// contents up to a certain size. If the list is too long, it will
        /// use ellipsis notation instead.
        /// </summary>
        /// <returns>The list as a string</returns>
        public override string ToString()
        {
            if (size == 0) return "Empty List";
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("List[{0}] (", size);
            sb.Append(GetItem(0));

            if (size < 5)
            {
                for (int i = 1; i < size; i++)
                {
                    sb.Append(", ");
                    sb.Append(GetItem(i));
                }
            }
            else
            {
                for (int i = 1; i < 3; i++)
                {
                    sb.Append(", ");
                    sb.Append(GetItem(i));
                }

                sb.Append(" ... ");
                sb.Append(GetItem(-1));
            }

            sb.Append(")");
            return sb.ToString();
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// Represents the number of items in the list.
        /// </summary>
        public override int Count
        {
            get { return size; }
        }

        /// <summary>
        /// Accesses the items in the sorted list by index. See the GetItem() 
        /// method for more details.
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <returns>The desired item</returns>
        public E this[Index index]
        {
            get { return GetItem(index); }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Colection Implmentation...

        /// <summary>
        /// Determines if the sorted list contains a particular item. The 
        /// method of search is determined by the particular implementation.
        /// </summary>
        /// <param name="item">The item to test for containment</param>
        /// <returns>True if the item is contained in the list, false if 
        /// otherwise</returns>
        public override bool Contains(E item)
        {
            //if we find an index, then we contain the item
            return IndexOf(item).HasValue;
        }

        /// <summary>
        /// Inserts an item into the sorted list. The items in the list are 
        /// automatically sorted upon insertion.
        /// </summary>
        /// <param name="item">The item to insert</param>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public override void Add(E item)
        {
            //calls upon the more detailed method
            this.Insert(item);
        }

        /// <summary>
        /// Removes an item from the end of the list. If the list is empty it 
        /// returns null instead.
        /// </summary>
        /// <returns>The item that was removed, or null if empty</returns>
        public override E Dequeue()
        {
            //checks for an empty list
            if (size == 0) return default(E);

            //calls the abstract method
            return RemoveAt(size - 1);
        }

        /// <summary>
        /// Removes the first instance of a given item from the list. It 
        /// returns false if the item was unable to be removed.
        /// </summary>
        /// <param name="item">Item to be removed</param>
        /// <returns>True if the item was successfully removed</returns>
        public override bool Remove(E item)
        {
            //If we don't find the item, we can't remove it
            int? index = IndexOf(item);
            if (!index.HasValue) return false;

            //Removes the item
            return RemoveAt(index.Value) != null;
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Abstract Methods...

        /// <summary>
        /// Finds the index of the requested item within the list. If the list 
        /// contains duplicate items, it returns the index of the first item 
        /// found.
        /// </summary>
        /// <param name="item">The item to be found</param>
        /// <returns>The index of the target item, or null if the item is not 
        /// contained within the list</returns>
        public abstract int? IndexOf(E item);

        /// <summary>
        /// Retrieves the item from the list at the given index. Negative 
        /// indices indicate positions from the end of the list. If the index 
        /// is invalid, then null is returned.
        /// </summary>
        /// <param name="index">Index of the item to be retrieved</param>
        /// <returns>The requested item, or null if the index was invalid
        /// </returns>
        public abstract E GetItem(Index index);

        /// <summary>
        /// Inserts an item into the list, keeping sorted order. It also 
        /// reports the index of the item's insertion.
        /// </summary>
        /// <param name="item">The item to be inserted</param>
        /// <returns>Index where the item was inserted</returns>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public abstract int Insert(E item);

        /// <summary>
        /// Removes an item from the list. Negative indices indicate positions 
        /// from the end of the list. If the index is invalid, no items are 
        /// removed and null is returned.
        /// </summary>
        /// <param name="index">Index of the item to be removed</param>
        /// <returns>The removed item, or null if the index was invalid
        /// </returns>
        public abstract E RemoveAt(Index index);

        #endregion /////////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method, compares one item to another, using the specialized 
        /// comparison method if provided, otherwise it uses the default method 
        /// defined on the content type.
        /// </summary>
        /// <param name="item1">First item to be compared</param>
        /// <param name="item2">Second item to be compared</param>
        /// <returns>A negative number if item1 comes first, a positive number
        /// if it comes second, and zero if they are equal</returns>
        protected int Compare(E item1, E item2)
        {
            //uses the default comparison function if necessary
            if (comparer == null) return item1.CompareTo(item2);

            //uses the custom comparison function, then the default
            int comp = comparer.Invoke(item1, item2);
            if (comp == 0) comp = item1.CompareTo(item2);

            return comp;
        }

        #endregion /////////////////////////////////////////////////////////////
    }
}