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
    /// This implementation of a Sorted List uses an internal array to store 
    /// it's items. Because an array can be accessed at random, this allows 
    /// it to utilize the binary search algorithm for O(log(n)) search times. 
    /// The downside is that whenever an item is inserted or removed, all 
    /// proceeding items must be shifted in index in order to fill the array. 
    /// This makes insertion and removal O(n*log(n)). It is possible to 
    /// specify the default capacity upon construction, in order to try and 
    /// avoid unnecessary array resizing.
    /// </summary>
    /// <typeparam name="E">The element type of the list</typeparam>
    /// <remarks>Last Update: 2016-05-30</remarks>
    public sealed class SortListArray<E> : SortList<E>
        where E : IComparable<E>
    {
        #region Class Deffinitions...

        //the default base capacity
        private const int DBC = 16;

        //stores the items in the list
        private E[] items;

        /// <summary>
        /// Constructs an empty list with the given initial capacity. A 
        /// comparison function may also be provided, specifying how items
        /// should be compared when sorting the list.
        /// </summary>
        /// <param name="cap">The initial default capacity</param>
        /// <param name="comp">Comparison operator to be used</param>
        public SortListArray(int cap = DBC, Comparison<E> comp = null)
        {
            //sets up the base size to user specification
            cap = (cap < DBC) ? DBC : cap;

            //creates the sorted list
            comparer = comp;
            Reset(cap);
        }

        /// <summary>
        /// Constructs a new list, containing multiple items. The default 
        /// capacity is set slightly larger than the initial size, allowing
        /// for the insertion of additional items.
        /// </summary>
        /// <param name="items">The items of the list</param>
        public SortListArray(params E[] items)
        {
            //generates base size from number of items
            int cap = (int)(items.Length * 1.25) + 1;
            cap = (cap < DBC) ? DBC : cap;

            //creates the sorted list
            comparer = null;
            Reset(cap);

            //inserts all of the preliminary items into the list
            foreach (E item in items) Add(item);
        }

        /// <summary>
        /// Constructs a new list, containing the same items as another 
        /// collection. The default capacity is set slightly larger than the 
        /// initial size, allowing for the insertion of additional items.
        /// </summary>
        /// <param name="items">The items of the list</param>
        public SortListArray(IEnumerable<E> items)
        {
            //generates base size from number of items
            int cap = (int)(items.Count() * 1.25) + 1;
            cap = (cap < DBC) ? DBC : cap;

            //creates the sorted list
            comparer = null;
            Reset(cap);

            //inserts all of the preliminary items into the list
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

        #region Sorting Implementation...

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
            //calls upon the more specialized method
            int index = LocateIndex(item, false);
            return (index >= 0) ? index : default(int?);
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
        /// Inserts an item into the list, keeping sorted order. It also 
        /// reports the index of the item's insertion.
        /// </summary>
        /// <param name="item">The item to be inserted</param>
        /// <returns>Index where the item was inserted</returns>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public override int Insert(E item)
        {
            //we do not allow the insertion of null elements
            if (item == null) throw new ArgumentNullException("item");

            //obtains the index, and checks validity
            int index = LocateIndex(item, true);

            //resizes array if necessary
            if (size >= items.Length) Resize(size * 2);

            //shifts the items
            int pos = this.size;
            while (pos > index)
            {
                items[pos] = items[pos - 1];
                pos--;
            }

            //inserts new item
            items[index] = item;
            this.size++; return index;
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
        /// Creates an enumeration over all the items in the list.
        /// </summary>
        /// <returns>An enumerator over the list</returns>
        public override IEnumerator<E> GetEnumerator()
        {
            //simply iterates over all the items up to size
            for (int i = 0; i < size; i++)
            yield return items[i];
        }

        /// <summary>
        /// Removes all items from the list, preventing memory leaks.
        /// </summary>
        public override void Clear()
        {
            items = new E[DBC];
            this.size = 0;
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method for preforming binary search. It can look for either
        /// the index of insertion or retrieval, given an item to insert or 
        /// retrieve.
        /// </summary>
        /// <param name="item">The item to insert or retrieve</param>
        /// <param name="insert">Set true for insertion, set false for 
        /// retrieval</param>
        /// <returns>The index of insertion or retrieval</returns>
        private int LocateIndex(E item, bool insert)
        {
            //values to be used in loop
            bool found = false;
            int first = 0;
            int last = this.size - 1;
            int mid = last / 2;
            
            while(first <= last && !found)
            {           
                //compares the middle value to our target         
                int compare = Compare(item, items[mid]);
                
                //takes care of the three separate cases
                if (compare < 0) last = mid - 1;
                else if (compare > 0) first = mid + 1;
                else found = true;
                
                //computes middle value         
                mid = (first + last) / 2;                       
            }

            if (found) return mid;
            else if (insert) return first;
            else return -1;
        } 

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