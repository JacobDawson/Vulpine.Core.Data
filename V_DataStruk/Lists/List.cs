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

namespace Vulpine.Core.Data.Lists
{
    /// <summary>
    /// A List is an indexed container of objects similar to an array. One key 
    /// difference is that it allows for the insertion and removal of objects. 
    /// When an item is inserted at a given index, all proceeding items increase
    /// in index to make room. Likewise, when an item is removed, all proceeding 
    /// items decrease in index to fill the gap. Like an array, all indices are 
    /// zero based. In addition, negative indices can be used to refer to items 
    /// from the end of the list. Because a list dose not impose any kind of 
    /// sorted order, a List can be of any object type, at the expense of making
    /// all search operations O(n).
    /// </summary>
    /// <typeparam name="E">The element type of the list</typeparam>
    /// <remarks>Last Update: 2016-05-20</remarks>
    public abstract class VList<E> : VCollection<E>, Indexed<E>
    {
        #region Class Definitions...

        //stores a reference to the size of the list
        protected int size;

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
        /// Accesses the items in the list. See the SetItem() and GetItem() 
        /// methods for more details.
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <returns>The requested item</returns>
        public E this[Index index]
        {
            get { return GetItem(index); }
            set { SetItem(index, value); }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Collection Implementation...

        /// <summary>
        /// Determines whether or not the list contains a similar item by 
        /// preforming a linear search through the list.
        /// </summary>
        /// <param name="item">Item to search for</param>
        /// <returns>True if a similar item is found in the list, and
        /// false if otherwise</returns>
        public override bool Contains(E item)
        {
            //if we find an index, then we contain the item
            return IndexOf(item).HasValue;
        }

        /// <summary>
        /// Inserts an item at the end of the list.
        /// </summary>
        /// <param name="item">The item to be inserted</param>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public override void Add(E item)
        {
            //calls the abstract method
            Insert(size, item);
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
        /// Removes the first instance of a given item from the list. It returns
        /// false if the item was unable to be removed.
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
        /// Sets the value at a given index in the list. Negative indices 
        /// indicate positions from the end of the list. If the index is 
        /// invalid, then an exception is thrown.
        /// </summary>
        /// <param name="index">The index to be changed</param>
        /// <param name="item">The value to be set</param>
        /// <exception cref="ArgumentNullException">
        /// If null is inserted</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the given index
        /// is invalid for the current list</exception>
        public abstract void SetItem(Index index, E item);

        /// <summary>
        /// Inserts an item into the list before the given index. Negative 
        /// indices indicate positions from the end of the list. If the index
        /// is invalid, then the item is inserted at the end of the list.
        /// </summary>
        /// <param name="index">The index of insertion</param>
        /// <param name="item">The item to be inserted</param>
        /// <exception cref="ArgumentNullException">
        /// If null is inserted</exception>
        public abstract void Insert(Index index, E item);

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
    }
}