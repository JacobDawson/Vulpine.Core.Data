/**
 *  This file is an integral part of the Vulpine Core Library: 
 *  Copyright (c) 2016-2017 Benjamin Jacob Dawson. 
 *
 *      https://www.jacobs-den.org/projects/core-library/
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

using Vulpine.Core.Data.Lists;

//SC

namespace Vulpine.Core.Data.Tables
{
    /// <summary>
    /// This implementation of a Table uses an array of linked lists to store its
    /// items. Because each bucket may hold more than one item, it can afford to
    /// use closed-address hashing. If two items hash to the same index, they are
    /// stored in the same bucket. The target bucket must be searched to retrieve
    /// the item, however. Thus the performance of the table depends heavily on the
    /// ratio of buckets to items being stored. The table will automatically update 
    /// the number of buckets it has in order to better compensate an increase in 
    /// size. This allows items in the table to be fetched in O(1) amortized time.
    /// </summary>
    /// <typeparam name="K">Key type of the table</typeparam>
    /// <typeparam name="E">Element type of the table</typeparam>
    /// <remarks>Last Update: 2016-06-18</remarks>
    public sealed class TableClosed<K, E> : Table<K, E>
    {
        #region Class Definitions...

        //the maximum load factor for optimal performance
        private const double MAXL = 4.0;

        //the default base size of the table
        private const int DBS = 7;

        //stores the size of the hash table
        private int size;

        //stores the buckets of the table
        private VList<KeyedItem<K, E>>[] table;

        /// <summary>
        /// Constructs a new closed-address table, with the given default number
        /// of buckets. This value should be significantly smaller than the total 
        /// number of items, in order to reduce space complexity. 
        /// </summary>
        /// <param name="cap">The default number of buckets</param>
        public TableClosed(int cap = DBS)
        {
            Initialise(cap);
        }

        /// <summary>
        /// Constructs a new table, containing multiple entries. The number of
        /// buckets is automatically determined based on the number of entries.
        /// </summary>
        /// <param name="pairs">The entries into the table</param>
        public TableClosed(IEnumerable<KeyedItem<K, E>> pairs)
        {
            //obtain the base capacity from the number of items
            int cap = (int)((pairs.Count() * 1.25) / MAXL) + 1;

            Initialise(cap);

            //adds the key-value pairs one at a time
            foreach (var pair in pairs) Add(pair.Key, pair.Item);
        }

        /// <summary>
        /// Constructs a new table, containing multiple entries. The keys
        /// for the entries are derived from a separate key selector function.
        /// The number of buckets is automatically determined based on the
        /// number of entries.
        /// </summary>
        /// <param name="items">The items to be stored in the table</param>
        /// <param name="selector">A function to derive the keys for each
        /// item that is stored in the table</param>
        public TableClosed(IEnumerable<E> items, Func<E, K> selector)
        {
            //obtain the base capacity from the number of items
            int cap = (int)((items.Count() * 1.25) / MAXL) + 1;

            Initialise(cap);

            //adds the key-value pairs one at a time
            foreach (var item in items)
            {
                var key = selector.Invoke(item);
                Add(key, item);
            }
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// Represents the number of items in the table.
        /// </summary>
        public override int Count
        {
            get { return size; }
        }

        /// <summary>
        /// Represents the internal size of the table, or how many buckets
        /// it has available to sort and store items.
        /// </summary>
        public override int Buckets
        {
            get { return table.Length; }
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Table Implementation...

        /// <summary>
        /// Determines if a particular key is already in use in this table.
        /// It returns true if the key exists, and false otherwise.
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>True if the key exists, false if otherwise</returns>
        public override bool HasKey(K key)
        {
            //uses a fake key to prob the table
            var fake = new KeyedItem<K, E>(key);

            //finds the bucket for the key
            int hash = fake.GetHashCode();
            int spot = HashIndex(hash);

            //asks the bucket if it has a matching key
            return table[spot].Contains(fake);
        }

        /// <summary>
        /// Retrieves a value from the table that matches the given key. If no
        /// match for the key can be found, it returns null.
        /// </summary>
        /// <param name="key">Key of the desired item</param>
        /// <returns>The matching item, or null if not found</returns>
        public override E GetValue(K key)
        {
            //used in probing the table for the keyed value
            var fake = new KeyedItem<K, E>(key);

            //finds the correct bucket and index
            int hash = fake.GetHashCode();
            int spot = HashIndex(hash);
            int index = table[spot].IndexOf(fake) ?? -1;

            //makes sure we have found a match
            if (index < 0) return default(E);

            //returns the item
            return table[spot].GetItem(index).Item;
        }

        /// <summary>
        /// Inserts a value with a given key into the table. 
        /// </summary>
        /// <param name="key">Key of the item to be inserted</param>
        /// <param name="item">Item to be inserted</param>
        /// <exception cref="ArgumentNullException">If either the key or
        /// the item are null</exception>
        /// <exception cref=" InvalidOperationException">If the table already
        /// contains an item with the same key</exception>
        public override void Add(K key, E item)
        {
            //checks that neither the pair nor the item are null
            if (key == null) throw new ArgumentNullException("key");
            if (item == null) throw new ArgumentNullException("item");

            //creates a new keyed item to contain the pair
            var pair = new KeyedItem<K, E>(key, item);

            //finds the correct bucket and index
            int hash = pair.GetHashCode();
            int spot = HashIndex(hash);

            //checks that the key is unique
            if (table[spot].Contains(pair))
                throw new InvalidOperationException();

            //inserts the new key-value pair
            InsertAt(pair, spot);
        }

        /// <summary>
        /// Overwrites any existing value that might have been associated
        /// with the given key, with the new value. If the key did not
        /// previously have a value, a new key-value pair is created.
        /// </summary>
        /// <param name="key">Key to overwrite</param>
        /// <param name="item">New value for the key</param>
        /// <exception cref="ArgumentNullException">If either the key or
        /// the item are null</exception>
        public override void Overwrite(K key, E item)
        {
            //checks that neither the pair nor the item are null
            if (key == null) throw new ArgumentNullException("key");
            if (item == null) throw new ArgumentNullException("item");

            //creates a new keyed item to contain the pair
            var pair = new KeyedItem<K, E>(key, item);

            //finds the correct bucket and index
            int hash = pair.GetHashCode();
            int spot = HashIndex(hash);
            int index = table[spot].IndexOf(pair) ?? -1;

            //removes the key form the table if it already exists
            if (index >= 0)
            {
                table[spot].RemoveAt(index);
                size = size - 1;
            }

            //inserts the new key-value pair
            InsertAt(pair, spot);
        }

        /// <summary>
        /// Removes an item from the table matching the given key. It 
        /// returns the item that was removed. If no match for the key 
        /// can be found, it returns null.
        /// </summary>
        /// <param name="key">Key of the item to remove</param>
        /// <returns>The item that was removed, or null if not found</returns>
        public override E Remove(K key)
        {
            //used in probing the tree for the keyed value
            var fake = new KeyedItem<K, E>(key);

            //finds the correct bucket and index
            int hash = fake.GetHashCode();
            int spot = HashIndex(hash);
            int index = table[spot].IndexOf(fake) ?? -1;

            //makes sure we have found a match
            if (index < 0) return default(E);

            //removes the pair from the table
            var pair = table[spot].RemoveAt(index);
            size = size - 1;

            return pair.Item;
        }

        /// <summary>
        /// Creates an enumeration over all the keys and values in 
        /// the table, together as keyed items.
        /// </summary>
        /// <returns>An enumeration of keyed items</returns>
        public override IEnumerator<KeyedItem<K, E>> GetEnumerator()
        {
            for (int i = 0; i < table.Length; i++)
            {
                //enumerates each bucket in double-iteration
                foreach (var pair in table[i]) yield return pair;
            }
        }

        /// <summary>
        /// Removes all items from the table. Preventing any
        /// potential memory leaks.
        /// </summary>
        public override void Clear()
        {
            //clears the buckets of all data
            if (table != null)
            {
                for (int i = 0; i < table.Length; i++)
                table[i].Clear();
            }

            //resets the table
            table = CreateArray(DBS);
            size = 0;
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method used for inserting a key-value pair into a particular
        /// bucket and resizing the table if necessary. It is called from both
        /// Insert and Overwrite to handle the case of insertion.
        /// </summary>
        /// <param name="pair">The pair to be inserted</param>
        /// <param name="spot">The index of the bucket</param>
        private void InsertAt(KeyedItem<K, E> pair, int spot)
        {
            //inserts the key-value pair
            table[spot].Add(pair);
            size = size + 1;

            //resizes the table if we exceed the maximum load
            int min_size = (int)(size / MAXL) + 1;
            if (table.Length < min_size) ChangeSize(min_size * 2);
        }

        /// <summary>
        /// Helper method for adjusting the size of the internal array.
        /// It always selects the next prime size after the desired size.
        /// </summary>
        /// <param name="new_size">The desired size</param>
        private void ChangeSize(int new_size)
        {
            //creates the larger temporary array
            int hash_size = Prime.NextPrime(new_size);
            var temp = CreateArray(hash_size);

            for (int x = 0; x < table.Length; x++)
            {
                //places each value into it's new bucket
                foreach (var pair in table[x])
                {
                    int hash = pair.GetHashCode();
                    int spot = HashIndex(hash, temp.Length);
                    temp[spot].Add(pair);
                }

                //erases the old bucket
                table[x].Clear();
            }

            //replaces the buckets
            table = temp;
        }

        /// <summary>
        /// Helper method for initializing the internal array, with the desired 
        /// starting capacity. The capacity must be greater than the step size 
        /// for double hashing to work.
        /// </summary>
        /// <param name="cap">The starting capacity</param>
        private void Initialise(int cap)
        {
            //makes certain that capacity is prime
            if (cap < DBS) cap = DBS;
            cap = Prime.NextPrime(cap - 1);

            //sets the initial conditions
            table = CreateArray(cap);
            size = 0;
        }

        /// <summary>
        /// Helper method that creates an array of buckets for the table. 
        /// It instantiates each of the buckets so they are ready to use.
        /// </summary>
        /// <param name="length">Number of buckets</param>
        /// <returns>An array of buckets</returns>
        private VList<KeyedItem<K, E>>[] CreateArray(int length)
        {
            //creates each of the buckets in the new array
            var array = new VList<KeyedItem<K, E>>[length];
            for (int i = 0; i < length; i++)
            array[i] = new VListLinked<KeyedItem<K, E>>();

            return array;
        }

        #endregion /////////////////////////////////////////////////////////////
    }
}
