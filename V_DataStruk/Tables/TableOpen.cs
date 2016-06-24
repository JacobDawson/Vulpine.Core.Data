using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//spell-checked

namespace Vulpine.Core.Data.Tables
{
    /// <summary>
    /// This implementation of a Table uses a single array to store its items. Because
    /// each bucket can only hold one item it must use open-address hashing to resolve 
    /// collisions. If two items hash to the same index, it probes the array looking for 
    /// the second item, using a step size computed from its hash code. This technique is 
    /// sometimes refereed to as double hashing. The array itself must be significantly 
    /// larger that the size of the table in order to minimize collisions. However, the 
    /// actual space complexity is comparable to binary search trees. Items in the table 
    /// can be fetched in O(1) amortized time.
    /// </summary>
    /// <typeparam name="K">Key type of the table</typeparam>
    /// <typeparam name="E">Element type of the table</typeparam>
    /// <remarks>Last Update: 2016-06-18</remarks>
    public sealed class TableOpen<K, E> : Table<K, E>
    {
        #region Class Definitions...

        //the maximum load factor for optimal performance
        private const double MAXL = 0.75;

        //the step size must be less than the default base size
        //both values must be prime for maximum efficiency
        private const int DBS = 23;
        private const int DSS = 13;

        //contains the object to be used as holder places
        private static readonly object HOLD = new Object();

        //defines the attributes of the hash table
        private int size;
        private int counter;

        //stores the table as generic objects
        private Object[] table;

        /// <summary>
        /// Constructs a new open-address table, with the given default number
        /// of buckets. This value should be significantly larger than the total 
        /// number of items, in order to reduce hash collisions. 
        /// </summary>
        /// <param name="cap">The default number of buckets</param>
        public TableOpen(int cap = DBS)
        {
            Initialise(cap);
        }

        /// <summary>
        /// Constructs a new table, containing multiple entries. The number of
        /// buckets is automatically determined based on the number of entries.
        /// </summary>
        /// <param name="pairs">The entries into the table</param>
        public TableOpen(IEnumerable<KeyedItem<K, E>> pairs)
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
        public TableOpen(IEnumerable<E> items, Func<E, K> selector)
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

        #endregion /////////////////////////////////////////////////////////////

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

        #endregion /////////////////////////////////////////////////////////////

        #region Table Implementation...

        /// <summary>
        /// Determines if a particular key is already in use in this table.
        /// It returns true if the key exists, and false otherwise.
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>True if the key exists, false if otherwise</returns>
        public override bool HasKey(K key)
        {
            //if we find an index, then we contain the item
            return FindIndex(key) >= 0;
        }

        /// <summary>
        /// Retrieves a value from the table that matches the given key. If no
        /// match for the key can be found, it returns null.
        /// </summary>
        /// <param name="key">Key of the desired item</param>
        /// <returns>The matching item, or null if not found</returns>
        public override E GetValue(K key)
        {
            //searches for the index matching the key
            int index = FindIndex(key);

            //makes sure that we have found an index
            if (index < 0) return default(E);

            //returns the value from legitimate pairs
            var temp = table[index] as KeyedItem<K, E>;
            return (temp != null) ? temp.Item : default(E);
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

            //takes care of adding room to the table
            if (size > table.Length * MAXL) 
                ChangeSize(2 * table.Length);

            //clears up holder places when necessary
            if (size + counter > table.Length * MAXL) 
                ChangeSize(table.Length - 2);

            //obtains the index and step size
            int hash = pair.GetHashCode();
            int spot = HashIndex(hash);
            int step = HashStep(hash);
            int posible = -1;

            while (table[spot] != null)
            {
                //checks for duplicate keys
                var temp = table[spot] as KeyedItem<K, E>;
                if (pair.Equals(temp)) throw new InvalidOperationException();

                //checks for potential holder spots
                if (table[spot] == HOLD && posible < 0)
                {
                    posible = spot;
                    break;
                }

                //updates the checking spot
                spot = HashIndex(spot + step);
            }

            //determines if location is empty
            if (posible < 0) posible = spot;
            else counter = counter - 1;

            //adds the keyed item pair
            table[posible] = pair;
            size = size + 1;
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

            //searches for the index matching the key
            int index = FindIndex(key);

            //overwrites the old key-value pair if it exists
            if (index < 0) Add(key, item);
            else table[index] = new KeyedItem<K, E>(key, item);
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
            //searches for the index matching the key
            int index = FindIndex(key);

            //checks for failed matches
            if (index < 0) return default(E);

            //deletes the pair and remembers it for later
            var temp = table[index] as KeyedItem<K, E>;
            table[index] = HOLD;

            //updates the counters
            size = size - 1;
            counter = counter + 1;

            //returns the item that was removed
            return (temp != null) ? temp.Item : default(E);
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
                //only returns valid pairs from the table
                var pair = table[i] as KeyedItem<K, E>;
                if (pair != null) yield return pair;
            }
        }

        /// <summary>
        /// Removes all items from the table. Preventing any
        /// potential memory leaks.
        /// </summary>
        public override void Clear()
        {
            //must be greater than 7 for double hashing
            table = new Object[DBS];
            size = counter = 0;
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method used to generate the step size used in searching.
        /// </summary>
        /// <param name="hash">Hash code used to derive the step size</param>
        /// <returns>Step size to use while searching</returns>
        private int HashStep(int hash)
        {
            //step size cannot equal table size
            hash = hash & Int32.MaxValue;
            return DSS - (hash % DSS);
        }

        /// <summary>
        /// Helper Method, finds the unique address into the table that matches 
        /// the given key. It returns negative if no sutch address is found.
        /// </summary>
        /// <param name="key">The key to find</param>
        /// <returns>Unique address of the key, or negative one</returns>
        private int FindIndex(K key)
        {
            //obtains the index and step size
            int hash = key.GetHashCode();
            int spot = HashIndex(hash);
            int step = HashStep(hash);

            while (table[spot] != null)
            {
                //checks to see if the item is found
                var temp = table[spot] as KeyedItem<K, E>;
                if (temp != null && key.Equals(temp.Key)) return spot;

                //advances the index
                spot = HashIndex(spot + step);
            }

            //we failed to find the key
            return -1;
        }

        /// <summary>
        /// Helper method for adjusting the size of the internal array.
        /// It always selects the next prime size after the desired size.
        /// </summary>
        /// <param name="new_size">The desired size</param>
        private void ChangeSize(int new_size)
        {
            //used in the inner loop
            int hash, spot, step;

            //creates the temporary array
            int hash_size = Prime.NextPrime(new_size);
            Object[] temp = new Object[hash_size];

            for (int x = 0; x < table.Length; x++)
            {
                //don't need to copy any holder values
                if (table[x] == null) continue;
                if (table[x] == HOLD) continue;

                //computes the initial index
                hash = table[x].GetHashCode();
                spot = HashIndex(hash, temp.Length);
                step = HashStep(hash);

                //probes further if necessary
                while (temp[spot] != null)
                spot = HashIndex(spot + step);

                temp[spot] = table[x];
            }

            counter = 0;
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
            table = new Object[cap];
            size = counter = 0;
        }

        #endregion /////////////////////////////////////////////////////////////
    }
}
