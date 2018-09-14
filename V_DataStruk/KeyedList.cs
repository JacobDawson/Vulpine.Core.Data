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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vulpine.Core.Data.Lists;
using Vulpine.Core.Data.Exceptions;

namespace Vulpine.Core.Data
{
    public class KeyedList<K, E> : IComparable<KeyedList<K, E>>,
        IEnumerable<E>, IDisposable where K : IComparable<K> where E : class
    {
        #region Class Definitions...

        //stores the key that refrences the list
        private K key;

        //stores the list of items itself
        private VList<E> items;

        /// <summary>
        /// Creates a new keyed list, witout any items.
        /// </summary>
        /// <param name="key">Key of the list</param>
        public KeyedList(K key)
        {
            //creates an empty list and sets the key
            items = new VListLinked<E>();
            this.key = key;
        }

        /// <summary>
        /// Creates a new keyed list, containing the given items.
        /// </summary>
        /// <param name="key">Key of the list</param>
        /// <param name="values">Items in the list</param>
        public KeyedList(K key, params E[] values)
        {
            //creates a list with the initial items
            items = new VListLinked<E>(values);
            this.key = key;
        }

        /// <summary>
        /// Generates a string representaton of the keyed list.
        /// </summary>
        /// <returns>The keyed list as a string</returns>
        public override string ToString()
        {
            if (key == null) return "Null Key";
            return String.Format("Keyed List: {0}", key);
        }

        /// <summary>
        /// Compares the current key to the key of another list. Null keys
        /// are listed as comming after all other keys.
        /// </summary>
        /// <param name="other">Keyed list to compare</param>
        /// <returns>Negative if the current comes first; Positive if
        /// the current comes after; Zero if the keys are the same
        /// </returns>
        public int CompareTo(KeyedList<K, E> other)
        {
            //null keys are listed after all others
            if (key == null || other.key == null)
            {
                if (other.key != null) return 1;
                else if (key != null) return -1;
                else return 0;
            }

            //simply compares the keys
            return key.CompareTo(other.key);
        }

        /// <summary>
        /// Determins if two object are considered equal.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if the objects are equal</returns>
        public override bool Equals(object obj)
        {
            //makes shure the other object is a keyed list
            if (!(obj is KeyedList<K, E>)) return false;
            
            //converts the object and compares
            var temp = (KeyedList<K, E>)obj;
            return (CompareTo(temp) == 0);
        }

        /// <summary>
        /// Generates a sudo-unique value pertaning to the key of
        /// the current item. Returns zero if the key is null.
        /// </summary>
        /// <returns>Hash value for the key</returns>
        public override int GetHashCode()
        {
            if (key == null) return 0;
            else return key.GetHashCode();
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// Obtains the key refrencing the current list of items.
        /// Read-Only
        /// </summary>
        public K Key
        {
            get { return key; }
        }

        /// <summary>
        /// Determins if the current key has any items associate with
        /// it; or if the list is empty. Read-Only
        /// </summary>
        public bool Empty
        {
            get { return items.Empty; }
        }

        public int Size
        {
            get { return items.Count; }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region List Access...

        /// <summary>
        /// Determins if the key points to a paticular item.
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <returns>True if the item is in the list</returns>
        public bool HasItem(E item)
        {
            //asks the list if it contains the item
            return items.Contains(item);
        }

        /// <summary>
        /// Adds an item to the list of items pointed to by 
        /// the current key.
        /// </summary>
        /// <param name="item">Item to insert</param>
        public void Insert(E item)
        {
            //inserts the item at the end of the list
            items.Insert(items.Count, item);
        }

        /// <summary>
        /// Removes an item from the list, sutch that the key 
        /// no longer points to it.
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>True if the item is sucessfull removed</returns>
        public bool Delete(E item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Enumerates all the items pointed at by this key.
        /// </summary>
        /// <returns>An enumeraton of all the items</returns>
        public IEnumerator<E> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary>
        /// Disposes of the keyed list, so as to free up any remaining
        /// memory resorces, and prevent memory leaks.
        /// </summary>
        public void Dispose()
        {
            items.Clear();
        }

        #endregion ////////////////////////////////////////////////////

        IEnumerator IEnumerable.GetEnumerator()
        { return items.GetEnumerator(); }
    }
}
