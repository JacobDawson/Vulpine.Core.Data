﻿/**
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

namespace Vulpine.Core.Data.Tables
{
    /// <summary>
    /// A Table, also known as an associative array, is an advanced data structure
    /// that references its contents by associating each item with a particular key.
    /// To look up a given item in the table only it's key needs to be known. Usually
    /// this is a very efficient operation. A table can also be thought of as a function
    /// which maps keys to values. Each key inside the table maps to a single value,
    /// however the same value may have multiple keys. For this reason, it is not
    /// permissible to insert duplicate keys into a table. Otherwise you would have
    /// a single key pointing to more than one value, which is not allowed.
    /// </summary>
    /// <typeparam name="K">Key type of the table</typeparam>
    /// <typeparam name="E">Element type of the table</typeparam>
    /// <remarks>Last Update: 2016-06-16</remarks>
    public abstract class Table<K, E> : VDictionary<K, E>
    {
        #region Class Definintions...

        /// <summary>
        /// Generates a string representation of the table, displaying the
        /// size of the table, as well as the number of buckets in use.
        /// </summary>
        /// <returns>The table as a string</returns>
        public override string ToString()
        {
            if (this.Empty) return "Empty Table";
            return String.Format("Table[{0}, {1}]", Count, Buckets);
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// Tables, by definition, require all keys to point to a unique
        /// value, so this property always returns true.
        /// </summary>
        public override bool UniqueKeys
        {
            get { return true; }
        }

        /// <summary>
        /// Represents the internal size of the table, or how many buckets
        /// it has available to sort and store items.
        /// </summary>
        public abstract int Buckets { get; }

        /// <summary>
        /// Accesses the items in the table by key. If a key is set to null,
        /// that key is instead removed from the table. See the respective
        /// retrieve, overwrite, and remove methods for more details.
        /// </summary>
        /// <param name="key">The key pointing to the item</param>
        /// <returns>The matching item</returns>
        public E this[K key]
        {
            get 
            { 
                return GetValue(key); 
            }
            set 
            {
                if (value == null) Remove(key);
                else Overwrite(key, value); 
            }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Abstract Methods...

        /// <summary>
        /// Retrieves a value from the table that matches the given key. If no
        /// match for the key can be found, it returns null.
        /// </summary>
        /// <param name="key">Key of the desired item</param>
        /// <returns>The matching item, or null if not found</returns>
        public abstract E GetValue(K key);

        /// <summary>
        /// Overwrites any existing value that might have been associated
        /// with the given key, with the new value. If the key did not
        /// previously have a value, a new key-value pair is created.
        /// </summary>
        /// <param name="key">Key to overwrite</param>
        /// <param name="item">New value for the key</param>
        /// <exception cref="ArgumentNullException">If either the key or
        /// the item are null</exception>
        public abstract void Overwrite(K key, E item);

        /// <summary>
        /// Removes an item from the table matching the given key. It 
        /// returns the item that was removed. If no match for the key 
        /// can be found, it returns null.
        /// </summary>
        /// <param name="key">Key of the item to remove</param>
        /// <returns>The item that was removed, or null if not found</returns>
        public abstract E Remove(K key);

        #endregion /////////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method for generating a hashed index.
        /// </summary>
        /// <param name="hash">Hash code used to derive the index</param>
        /// <returns>An index into the table</returns>
        protected int HashIndex(int hash)
        {
            hash = hash & Int32.MaxValue;
            return hash % this.Buckets;
        }

        /// <summary>
        /// Helper method for generating a hashed index.
        /// </summary>
        /// <param name="hash">Hash code used to derive the index</param>
        /// <param name="size">The size of the table</param>
        /// <returns>An index into the table</returns>
        protected int HashIndex(int hash, int size)
        {
            hash = hash & Int32.MaxValue;
            return hash % size;
        }

        #endregion /////////////////////////////////////////////////////////////
    }
}
