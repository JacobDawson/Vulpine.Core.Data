using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vulpine.Core.Data.Trees;

namespace Vulpine.Core.Data.Heeps
{
    /// <summary>
    /// This implementation of a Heep uses a single self-balancing tree to store
    /// its items, and is little more than a wrapper for the heep interface.
    /// Because the tree is balanced, both insertion and removal are O(log n)
    /// operations. It dose however come with some aditional space complexity,
    /// as it also has to store both the structor of the tree, as well as the 
    /// balancing information.
    /// </summary>
    /// <typeparam name="K">Key type of the heep</typeparam>
    /// <typeparam name="E">Element type of the heep</typeparam>
    /// <remarks>Last Update: 2016-06-21</remarks>
    public sealed class HeepTree<K, E> : Heep<K, E>
        where K : IComparable<K>
    {
        #region Class Definitions...

        //stores the items in a single tree
        private Tree<KeyedItem<K, E>> inner;

        //keeps a refrence to the top-most pair
        private KeyedItem<K, E> top;

        /// <summary>
        /// Constructs an empty, tree-based heep. It can construct either
        /// a min-heep (default) or a max-heep.
        /// </summary>
        /// <param name="max">Set to true to create a max-heep, set to
        /// false to create a min-heep</param>
        public HeepTree(bool max = false)
        {
            maxheep = max;
            top = null;

            //creates the singluar red-black tree
            inner = new TreeRedBlack<KeyedItem<K, E>>();
        }

        /// <summary>
        /// Constructs a new heep containing multiple entries. It can construct 
        /// either a min-heep (default) or a max-heep.
        /// </summary>
        /// <param name="pairs">The entries into the table</param>
        /// <param name="max">Set to true to create a max-heep, set to
        /// false to create a min-heep</param>
        public HeepTree(IEnumerable<KeyedItem<K, E>> pairs, bool max = false)
        {
            maxheep = max;
            top = null;

            //creates the singluar red-black tree
            inner = new TreeRedBlack<KeyedItem<K, E>>();

            //adds the key-value pairs one at a time
            foreach (var pair in pairs) inner.Add(pair);
        }

        /// <summary>
        /// Constructs a new heep, containing multiple entries. The keys
        /// for the entries are derived from a seperate key selector funciton.
        /// It can construct either a min-heep (default) or a max-heep.
        /// </summary>
        /// <param name="items">The items to be stored in the heep</param>
        /// <param name="selector">A funciton to derive the keys for each
        /// item that is stored in the heep</param>
        /// <param name="max">Set to true to create a max-heep, set to
        /// false to create a min-heep</param>
        public HeepTree(IEnumerable<E> items, Func<E, K> selector, bool max = false)
        {
            maxheep = max;
            top = null;

            //creates the singluar red-black tree
            inner = new TreeRedBlack<KeyedItem<K, E>>();

            //adds the key-value pairs one at a time
            foreach (var item in items)
            {
                var key = selector.Invoke(item);
                inner.Add(new KeyedItem<K, E>(key, item));
            }
        }

        #endregion ///////////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// Determins if the heep is empty or contains items. It is
        /// set to true if empty and false if otherwise.
        /// </summary>
        public override bool Empty
        {
            get { return inner.Empty; }
        }

        /// <summary>
        /// Represents the number of items in the heep.
        /// </summary>
        public override int Count
        {
            get { return inner.Count; }
        }

        /// <summary>
        /// Represents the highest priority item in the heep. What constitutes the
        /// highest priority is dependent on wheather it is a min-heep or a max-heep.
        /// </summary>
        public override E TopItem
        {
            get 
            {
                if (inner.Empty) return default(E);
                if (top == null) top = inner.GetMinMax(maxheep);

                return top.Item;
            }
        }

        /// <summary>
        /// Represents the highest priority in the entire heep. This can be seen as
        /// the priority of the heep itself. What constitutes the highest priority 
        /// is dependent on wheather it is a min-heep or a max-heep.
        /// </summary>
        public override K TopKey
        {
            get 
            {
                if (inner.Empty) return default(K);
                if (top == null) top = inner.GetMinMax(maxheep);

                return top.Key;
            }
        }

        #endregion ///////////////////////////////////////////////////////////////////

        #region Heep Implementation...

        /// <summary>
        /// Determins if any item in the heep has the matching priority.
        /// It returns true if sutch an item is found and false if otherwise.
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>True if the key exists, false if otherwise</returns>
        public override bool HasKey(K key)
        {
            //if the key is null we don't contain it
            if (key == null) return false;

            //uses a fake key inorder to probe the tree
            var fake_key = new KeyedItem<K, E>(key);
            return inner.Contains(fake_key);
        }

        /// <summary>
        /// Inserts an item with given priroity into the heep. 
        /// </summary>
        /// <param name="key">The key, or priority, of the item</param>
        /// <param name="item">The item to be inserted</param>
        /// <exception cref="ArgumentNullException">If either the key or
        /// the item are null</exception>
        public override void Add(K key, E item)
        {
            //checks that neither the pair nor the item are null
            if (key == null) throw new ArgumentNullException("key");
            if (item == null) throw new ArgumentNullException("item");

            top = null;

            //adds the new keyed item pair to the tree
            inner.Add(new KeyedItem<K, E>(key, item));
        }

        /// <summary>
        /// Removes the top-most item from the heep and returns its value. What 
        /// constitutes the top-most item is dependent on wheather it is a min-heep 
        /// or a max-heep. It reutrns null if the heep is empty.
        /// </summary>
        /// <returns>The highest priority item, or null if empty</returns>
        public override E Dequeue()
        {
            top = null;

            //calls the apropriate min-max method
            var pair = inner.RemoveMinMax(maxheep);

            //returns the item of the pair returned by the tree
            return (pair != null) ? pair.Item : default(E);
        }

        /// <summary>
        /// Removes the top-most item from the heep and returns both its value
        /// and its priority. What constitutes the top-most item is dependent on 
        /// wheather it is a min-heep or a max-heep.
        /// </summary>
        /// <param name="key">The priority of the item removed</param>
        /// <returns>The highest priority item, or null if empty</returns>
        public override E Dequeue(out K key)
        {
            top = null;

            //calls the apropriate min-max method
            var pair = inner.RemoveMinMax(maxheep);

            if (pair != null)
            {
                //returns the key and value seperatly
                key = pair.Key;
                return pair.Item;
            }
            else
            {
                //uses default values if inner returned null
                key = default(K);
                return default(E);
            }
        }

        /// <summary>
        /// Creates an enumeration over all the keys and values in 
        /// the heep, together as keyed items.
        /// </summary>
        /// <returns>An enumeration of keyed items</returns>
        public override IEnumerator<KeyedItem<K, E>> GetEnumerator()
        {
            //iterates throught the tree in level-order
            return inner.ListInLevelOrder().GetEnumerator();
        }

        /// <summary>
        /// Removes all items from the heep. Preventing any
        /// potential memory leaks.
        /// </summary>
        public override void Clear()
        {
            inner.Clear();
            top = null;
        }

        #endregion ///////////////////////////////////////////////////////////////////
    }
}
