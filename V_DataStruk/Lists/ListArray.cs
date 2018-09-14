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
    /// This implementation of List uses an array to store its items. The 
    /// items in the array are referenced directly by there index, allowing 
    /// for immediate retrieval. When items are added or removed, however, 
    /// the elements of the array must be shifted to maintain proper order. 
    /// The size of the internal array is referred to as the list's Capacity, 
    /// which is always larger than it's Count. The initial Capacity of the 
    /// list can be set upon construction. It provides fast lookup time, at 
    /// the expense of slow insertion and removal time.
    /// </summary>
    /// <typeparam name="E">The element type of the list</typeparam>
    /// <remarks>Last Update: 2016-05-19</remarks>
    public sealed class VListArray<E> : VList<E>
    {
        #region Class Definitions...

        //the default base capacity
        private const int DBC = 16;

        //stores the items in the list
        private E[] items;

        /// <summary>
        /// Constructs an empty list with a given default capacity. Although 
        /// the list may grow beyond this capacity as items are added, it will 
        /// not shrink bellow it.
        /// </summary>
        /// <param name="cap">The initial default capacity</param>
        public VListArray(int cap = DBC)
        {
            cap = (cap < DBC) ? DBC : cap;
            Reset(cap);
        }

        /// <summary>
        /// Constructs a new list, containing multiple items. The default 
        /// capacity is set slightly larger than the initial size, allowing
        /// for the insertion of additional items.
        /// </summary>
        /// <param name="items">The items of the list</param>
        public VListArray(params E[] items)
        {
            //generates base capacity from number of items
            int cap = (int)(items.Length * 1.25) + 1;
            cap = (cap < DBC) ? DBC : cap;

            Reset(cap);

            //inserts the items in-order to avoid shifting
            for (int i = 0; i < items.Length; i++)
            Insert(i, items[i]);
        }

        /// <summary>
        /// Constructs a new list, containing the same items as another 
        /// collection. The default capacity is set slightly larger than the 
        /// initial size, allowing for the insertion of additional items.
        /// </summary>
        /// <param name="items">The items of the list</param>
        public VListArray(IEnumerable<E> items)
        {
            //generates base capacity from number of items
            int cap = (int)(items.Count() * 1.25) + 1;
            cap = (cap < DBC) ? DBC : cap;

            Reset(cap);

            //inserts the items in-order to avoid shifting
            foreach (E item in items) Add(item);
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// Represents the list's current capacity; the number of items the 
        /// list is able to store without resizing, as opposed to the number
        /// it actually contains.
        /// </summary>
        public int Capacity
        {
            get 
            { 
                return items.Length; 
            }
            set
            {
                //makes sure the capacity is of valid size
                int min = Math.Max(DBC, size);
                int cap = Math.Max(min, value);

                Resize(cap);
            }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region List Implementation...

        /// <summary>
        /// Finds the index of the requested item within the list. If the list 
        /// contains duplicate items, it returns the index of the first item 
        /// found.
        /// </summary>
        /// <param name="item">The item to be found</param>
        /// <returns>The index of the target item, or null if the item is not
        /// contained within the list</returns>
        public override int? IndexOf(E item)
        {
            //search for a matching item in the list
            for (int i = 0; i < size; i++)
            if (items[i].Equals(item)) return i;

            //return null if we haven't found one
            return null;
        }

        /// <summary>
        /// Retrieves the item from the list at the given index. Negative 
        /// indices indicate positions from the end of the list. If the index 
        /// is invalid, then null is returned.
        /// </summary>
        /// <param name="index">Index of the item to be retrieved</param>
        /// <returns>The requested item, or null if the index was invalid
        /// </returns>
        public override E GetItem(Index index)
        {
            int? x = index.GetIndex(this.size);
            return x.HasValue ? items[x.Value] : default(E);
        }

        /// <summary>
        /// Sets the value at a given index in the list. Negative indices 
        /// indicate positions from the end of the list. If the index is 
        /// invalid, then an exception is thrown.
        /// </summary>
        /// <param name="index">The index to be changed</param>
        /// <param name="item">The value to be set</param>
        /// <exception cref="ArgumentNullException">
        /// If null is inserted</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the given 
        /// index is invalid for the current list</exception>
        public override void SetItem(Index index, E item)
        {
            //we do not allow the insertion of null elements
            if (item == null) throw new ArgumentNullException("item");

            //obtains a valid index
            int? x = index.GetIndex(this.size);
            if (!x.HasValue) throw new ArgumentOutOfRangeException("index");

            //simply sets the item
            items[x.Value] = item;
        }

        /// <summary>
        /// Inserts an item into the list before the given index. Negative 
        /// indices indicate positions from the end of the list. If the index
        /// is invalid, then the item is inserted at the end of the list.
        /// </summary>
        /// <param name="index">The index of insertion</param>
        /// <param name="item">The item to be inserted</param>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public override void Insert(Index index, E item)
        {
            //we do not allow the insertion of null elements
            if (item == null) throw new ArgumentNullException("item");

            int pos = this.size;
            int stop = index.GetIndex(pos) ?? pos;

            //resizes array if necessary
            if (pos >= items.Length) Resize(size * 2);

            //shifts the items below
            while (pos > stop)
            {
                items[pos] = items[pos - 1];
                pos--;
            }

            // insert new item
            items[stop] = item;
            this.size++;
        }

        /// <summary>
        /// Removes an item from the list. Negative indices indicate positions 
        /// from the end of the list. If the index is invalid, no items are 
        /// removed and null is returned.
        /// </summary>
        /// <param name="index">Index of the item to be removed</param>
        /// <returns>The removed item, or null if the index was invalid
        /// </returns>
        public override E RemoveAt(Index index)
        {
            //if the index is invalid, there is nothing to do
            int? test = index.GetIndex(size);
            if (!test.HasValue) return default(E);

            int pos = test.Value;
            E item = items[pos];

            //delete by shifting          
            while (pos < size - 1)
            {
                items[pos] = items[pos + 1];
                pos++;
            }

            items[size - 1] = default(E);
            this.size--;

            return item;
        }
    
        /// <summary>
        /// Removes all items from the list, preventing memory leaks.
        /// </summary>
        public override void Clear()
        {
            items = new E[DBC];
            this.size = 0;
        }

        /// <summary>
        /// Creates an enumeration over all the items in the list.
        /// </summary>
        /// <returns>An enumerator over the list</returns>
        public override IEnumerator<E> GetEnumerator()
        {
            //simply iterates over all the items up to size
            for (int i = 0; i < size; i++)
            yield return items[i];
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method for resizing the internal array.
        /// </summary>
        /// <param name="new_size">The new size of the array</param>
        private void Resize(int new_size)
        {
            E[] temp = new E[new_size];
            for (int index = 0; index < size; index++)
            {
                temp[index] = items[index];
            }
            items = temp;
        }

        /// <summary>
        /// Helper method for reseting the internal array, with the desired 
        /// starting capacity.
        /// </summary>
        /// <param name="cap">The starting capacity</param>
        private void Reset(int cap)
        {
            items = new E[cap];
            this.size = 0;
        }

        #endregion /////////////////////////////////////////////////////////////
    }
}