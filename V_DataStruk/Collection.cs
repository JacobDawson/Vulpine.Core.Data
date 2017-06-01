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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//SC

namespace Vulpine.Core.Data
{
    /// <summary>
    /// This abstract class provides the basic functionality of all collection-type
    /// data structures in the core library. A collection is any group of items that
    /// share some common structure. Furthermore, it is not permissible to store NULL
    /// items in a collection, as NULL values have the special connotation of "This
    /// item is missing from the collection". It is similar to the ICollection interface 
    /// but different in certain key aspects. First of all, it is an abstract class, 
    /// not an interface. It includes a default dequeue method, providing queue-like 
    /// operations for all collections. Not all collections implement the default 
    /// remove method, so it should be used with caution. Although it dose implement
    /// the ICollection interface, this is done primary to support LINQ operations,
    /// which run more efficiently when the sequence being processed implements ICollection.
    /// </summary>
    /// <typeparam name="E">The element type of the collection</typeparam>
    /// <remarks>Last Update: 2016-06-14</remarks>
    public abstract class VCollection<E> : ICollection<E>, IDisposable
    {
        #region Default Properties...

        /// <summary>
        /// Represents the number of items in the collection.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Determines if the collection is empty or contains items. It is
        /// set to true if empty and false if otherwise.
        /// </summary>
        public virtual bool Empty
        {
            get { return Count <= 0; }
        }

        /// <summary>
        /// Determines if the collection only allows read access. By default 
        /// data structures allow both read and wright access.
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return false; }
        }
        
        #endregion //////////////////////////////////////////////////////////////

        #region Default Operations...

        /// <summary>
        /// Determines if the collection contains a particular item.
        /// </summary>
        /// <param name="item">The item to test for containment</param>
        /// <returns>True if the item is contained in the collection,
        /// false if otherwise</returns>
        public abstract bool Contains(E item);

        /// <summary>
        /// Inserts an item into the collection.
        /// </summary>
        /// <param name="item">The item to be inserted</param>
        /// <remarks>It overloads the (+) operator</remarks>
        /// <exception cref="ArgumentNullException">If null is inserted</exception>
        public abstract void Add(E item);

        /// <summary>
        /// Removes a single item at random from the collection. The
        /// exact item removed is determined by the data structure 
        /// being implemented. If the collection is empty it returns null.
        /// </summary>
        /// <returns>The item that was removed, or null if empty</returns>
        public abstract E Dequeue();

        /// <summary>
        /// Removes a given item from the collection. If the collection contains
        /// duplicate values, the first instance found is removed. It returns
        /// true if the item was successfully removed, and false if otherwise.
        /// </summary>
        /// <param name="item">Item to be removed</param>
        /// <returns>True if the item was successfully removed</returns>
        /// <exception cref="NotSuportedException">If the underlying data
        /// structure dose not support explicit removal of items</exception>
        public abstract bool Remove(E item);

        /// <summary>
        /// Removes all items from the collection. Causing the collection
        /// to revert to it's original, initialized state.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Creates an enumeration over all the items in the collection. 
        /// </summary>
        /// <returns>An enumerator over the collection</returns>
        public abstract IEnumerator<E> GetEnumerator();

        #endregion //////////////////////////////////////////////////////////////

        #region Additional Opperations...

        /// <summary>
        /// Inserts multiple items into the collection.
        /// </summary>
        /// <param name="items">Items to insert</param>
        /// <exception cref="ArgumentNullException">If a null value is 
        /// inserted into the collection</exception>
        public virtual void Add(params E[] items)
        {
            //simply calls the add method repeatedly
            foreach (E item in items) Add(item);
        }

        /// <summary>
        /// Inserts multiple items into the collection.
        /// </summary>
        /// <param name="items">Items to insert</param>
        /// <remarks>It overloads the (+) operator</remarks>
        /// <exception cref="ArgumentNullException">If a null value is 
        /// inserted into the collection</exception>
        public virtual void Add(IEnumerable<E> items)
        {
            //simply calls the add method repeatedly
            foreach (E item in items) Add(item);
        }

        /// <summary>
        /// Copies the elements of the collection into an array with a given
        /// offset. This is implemented primary in support of the ICollection
        /// interface, for which it is required.
        /// </summary>
        /// <param name="array">The array in which to place the items</param>
        /// <param name="offset">The offset index into the array</param>
        /// <exception cref="ArgumentNullException">If the array is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the offset is less
        /// than zero, or dose not leave enough room for the collection to fit
        /// into the rest of the array</exception>
        public virtual void CopyTo(E[] array, int offset)
        {
            //checks that the collection will fit into the array
            if (array == null) throw new ArgumentNullException("array");
            if (offset < 0 || offset > array.Length - Count)
                throw new ArgumentOutOfRangeException("offset");

            //copies the items into the array, one by one
            foreach (E item in this)
            {
                array[offset] = item;
                offset = offset + 1;
            }
        }

        /// <summary>
        /// Provides a dispose method to comply with the IDisposable syntax,
        /// as many collections require cleanup after use. By default, it
        /// simply clears the collection of all data.
        /// </summary>
        public virtual void Dispose()
        {
            Clear();
        }

        #endregion //////////////////////////////////////////////////////////////

        #region Operator Overlodes...

        /// <summary>
        /// Calls the Add() method and returns a reference to the same collection.
        /// This is so that multiple insertions can be chained together.
        /// </summary>
        public static VCollection<E> operator+(VCollection<E> container, E item)
        { 
            container.Add(item); 
            return container; 
        }

        /// <summary>
        /// Calls the Add() method and returns a reference to the same collection.
        /// This is so that multiple insertions can be chained together.
        /// </summary>
        public static VCollection<E> operator+(VCollection<E> container, IEnumerable<E> items)
        { 
            container.Add(items); 
            return container; 
        }

        #endregion //////////////////////////////////////////////////////////////

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
