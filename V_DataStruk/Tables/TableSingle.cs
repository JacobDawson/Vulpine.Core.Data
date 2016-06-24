using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//spell-checked

using Vulpine.Core.Data.Trees;

namespace Vulpine.Core.Data.Tables
{
    /// <summary>
    /// This implementation of a Table uses a single self-balancing tree to store
    /// its items, and is little more than a wrapper for the table interface. Because
    /// it only has a single bucket to store its items, look-up times are O(log n) in
    /// the general case. However, because it dose not use hashing to determine where
    /// to store its items, it is not susceptible to hash collisions. And it can prove
    /// to be quite efficient when the number of items in the table is small.
    /// </summary>
    /// <typeparam name="K">Key type of the table</typeparam>
    /// <typeparam name="E">Element type of the table</typeparam>
    /// <remarks>Last Update: 2016-06-16</remarks>
    public sealed class TableSingle<K, E> : Table<K, E>
    {
        #region Class Definitions...

        //stores the items in a single tree
        private Tree<KeyedItem<K, E>> inner;

        /// <summary>
        /// Constructs a empty single-tree table.
        /// </summary>
        public TableSingle()
        {
            //creates the singular red-black tree
            inner = new TreeRedBlack<KeyedItem<K, E>>();
        }

        /// <summary>
        /// Constructs a new table, containing multiple entries.  
        /// </summary>
        /// <param name="pairs">The entries into the table</param>
        public TableSingle(IEnumerable<KeyedItem<K, E>> pairs)
        {
            //creates the singular red-black tree
            inner = new TreeRedBlack<KeyedItem<K, E>>();

            //adds the key-value pairs one at a time
            foreach (var pair in pairs) inner.Add(pair);
        }

        /// <summary>
        /// Constructs a new table, containing multiple entries. The keys
        /// for the entries are derived from a separate key selector function.
        /// </summary>
        /// <param name="items">The items to be stored in the table</param>
        /// <param name="selector">A function to derive the keys for each
        /// item that is stored in the table</param>
        public TableSingle(IEnumerable<E> items, Func<E, K> selector)
        {
            //creates the singular red-black tree
            inner = new TreeRedBlack<KeyedItem<K, E>>();

            //adds the key-value pairs one at a time
            foreach (var item in items)
            {
                var key = selector.Invoke(item);
                inner.Add(new KeyedItem<K, E>(key, item));
            }
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// Determines if the table is empty or contains items. It is
        /// set to true if empty and false if otherwise.
        /// </summary>
        public override bool Empty
        {
            get { return inner.Empty; }
        }

        /// <summary>
        /// Represents the number of items in the table.
        /// </summary>
        public override int Count
        {
            get { return inner.Count; }
        }

        /// <summary>
        /// There is only one tree in a single tree table, so this
        /// value always returns one.
        /// </summary>
        public override int Buckets
        {
            get { return 1; }
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
            //if the key is null we don't contain it
            if (key == null) return false;

            //uses a fake key in order to probe the tree
            var fake_key = new KeyedItem<K, E>(key);
            return inner.Contains(fake_key);
        }

        /// <summary>
        /// Retrieves a value from the table that matches the given key. If no
        /// match for the key can be found, it returns null.
        /// </summary>
        /// <param name="key">Key of the desired item</param>
        /// <returns>The matching item, or null if not found</returns>
        public override E GetValue(K key)
        {
            //if the key is null we don't contain it
            if (key == null) return default(E);

            //uses a fake key to probe the tree
            var fake = new KeyedItem<K, E>(key);
            var pair = inner.Retreve(fake);

            //returns the item of the pair returned by the tree
            return (pair != null) ? pair.Item : default(E);
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

            //checks that the tree dose not contain the key
            if (inner.Contains(pair)) throw new InvalidOperationException();

            //adds the pair to the tree
            inner.Add(pair);
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

            //deletes the old pair before inserting the new
            inner.Remove(pair);
            inner.Add(pair);
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
            //if the key is null we don't contain it
            if (key == null) return default(E);     

            //uses a fake key in order to probe the tree
            var fake_key = new KeyedItem<K, E>(key);

            //removes the item and stores the result
            var pair = inner.Retreve(fake_key);
            inner.Remove(fake_key);

            //returns the item that was removed
            return (pair != null) ? pair.Item : default(E);
        }

        /// <summary>
        /// Creates an enumeration over all the keys and values in 
        /// the table, together as keyed items.
        /// </summary>
        /// <returns>An enumeration of keyed items</returns>
        public override IEnumerator<KeyedItem<K, E>> GetEnumerator()
        {
            //simply uses the tree's own enumerator
            return inner.GetEnumerator();
        }

        /// <summary>
        /// Removes all items from the table. Preventing any
        /// potential memory leaks.
        /// </summary>
        public override void Clear()
        {
            inner.Clear();
        }

        #endregion ///////////////////////////////////////////////////////////////
    }
}
