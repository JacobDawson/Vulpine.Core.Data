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

using Vulpine.Core.Data.Trees;

//SC

namespace Vulpine.Core.Data.Heeps
{
    /// <summary>
    /// This implementation of a Heep uses a single self-balancing tree to store
    /// its items, and is little more than a wrapper for the heap interface.
    /// Because the tree is balanced, both insertion and removal are O(log n)
    /// operations. It dose however come with some additional space complexity,
    /// as it also has to store both the structure of the tree, as well as the 
    /// balancing information.
    /// </summary>
    /// <typeparam name="K">Key type of the heap</typeparam>
    /// <typeparam name="E">Element type of the heap</typeparam>
    /// <remarks>Last Update: 2016-06-21</remarks>
    public sealed class HeepTree<K, E> : Heep<K, E>
        where K : IComparable<K>
    {
        #region Class Definitions...

        //stores the items in a single tree
        private Tree<KeyedItem<K, E>> inner;

        //keeps a reference to the top-most pair
        private KeyedItem<K, E> top;

        /// <summary>
        /// Constructs an empty, tree-based heap. It can construct either
        /// a min-heap (default) or a max-heap.
        /// </summary>
        /// <param name="max">Set to true to create a max-heap, set to
        /// false to create a min-heap</param>
        public HeepTree(bool max = false)
        {
            maxheep = max;
            top = null;

            //creates the singular red-black tree
            inner = new TreeRedBlack<KeyedItem<K, E>>();
        }

        /// <summary>
        /// Constructs a new heap containing multiple entries. It can construct 
        /// either a min-heap (default) or a max-heap.
        /// </summary>
        /// <param name="pairs">The entries into the table</param>
        /// <param name="max">Set to true to create a max-heep, set to
        /// false to create a min-heap</param>
        public HeepTree(IEnumerable<KeyedItem<K, E>> pairs, bool max = false)
        {
            maxheep = max;
            top = null;

            //creates the singular red-black tree
            inner = new TreeRedBlack<KeyedItem<K, E>>();

            //adds the key-value pairs one at a time
            foreach (var pair in pairs) inner.Add(pair);
        }

        /// <summary>
        /// Constructs a new heap, containing multiple entries. The keys
        /// for the entries are derived from a separate key selector function.
        /// It can construct either a min-heap (default) or a max-heap.
        /// </summary>
        /// <param name="items">The items to be stored in the heap</param>
        /// <param name="selector">A function to derive the keys for each
        /// item that is stored in the heap</param>
        /// <param name="max">Set to true to create a max-heap, set to
        /// false to create a min-heap</param>
        public HeepTree(IEnumerable<E> items, Func<E, K> selector, bool max = false)
        {
            maxheep = max;
            top = null;

            //creates the singular red-black tree
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
        /// Determines if the heap is empty or contains items. It is
        /// set to true if empty and false if otherwise.
        /// </summary>
        public override bool Empty
        {
            get { return inner.Empty; }
        }

        /// <summary>
        /// Represents the number of items in the heap.
        /// </summary>
        public override int Count
        {
            get { return inner.Count; }
        }

        /// <summary>
        /// Represents the highest priority item in the heap. What constitutes the
        /// highest priority is dependent on whether it is a min-heap or a max-heap.
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
        /// Represents the highest priority in the entire heap. This can be seen as
        /// the priority of the heap itself. What constitutes the highest priority 
        /// is dependent on whether it is a min-heap or a max-heap.
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
        /// Determines if any item in the heap has the matching priority.
        /// It returns true if such an item is found and false if otherwise.
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
        /// Inserts an item with given priority into the heap. 
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
        /// Removes the top-most item from the heap and returns its value. What 
        /// constitutes the top-most item is dependent on whether it is a min-heap 
        /// or a max-heap. It returns null if the heap is empty.
        /// </summary>
        /// <returns>The highest priority item, or null if empty</returns>
        public override E Dequeue()
        {
            top = null;

            //calls the appropriate min-max method
            var pair = inner.RemoveMinMax(maxheep);

            //returns the item of the pair returned by the tree
            return (pair != null) ? pair.Item : default(E);
        }

        /// <summary>
        /// Removes the top-most item from the heap and returns both its value
        /// and its priority. What constitutes the top-most item is dependent on 
        /// whether it is a min-heap or a max-heap.
        /// </summary>
        /// <param name="key">The priority of the item removed</param>
        /// <returns>The highest priority item, or null if empty</returns>
        public override E Dequeue(out K key)
        {
            top = null;

            //calls the appropriate min-max method
            var pair = inner.RemoveMinMax(maxheep);

            if (pair != null)
            {
                //returns the key and value separately
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
        /// the heap, together as keyed items.
        /// </summary>
        /// <returns>An enumeration of keyed items</returns>
        public override IEnumerator<KeyedItem<K, E>> GetEnumerator()
        {
            //iterates through the tree in level-order
            return inner.ListInLevelOrder().GetEnumerator();
        }

        /// <summary>
        /// Removes all items from the heap. Preventing any
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
