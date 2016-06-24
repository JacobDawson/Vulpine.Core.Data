﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vulpine.Core.Data.Queues;

namespace Vulpine.Core.Data.Heeps
{
    /// <summary>
    /// This implementation of a Heep uses a complete binary tree to store its items.
    /// Because the tree is complete, it can be stored in an array. Simple arithmatic
    /// can be used to compute index of the children and the parent of a given node.
    /// Each node in the tree is required to have higher priority than either of its
    /// children. This ensures that the highest priority item is always at the root of
    /// the tree, making look-up time O(1). Insertion and Removal both take O(log n)
    /// time, as they have to shift items up and down the tree. It is possible to 
    /// specify the default capacity upon construction, in order to try and avoid 
    /// unnecessary array resizing.
    /// </summary>
    /// <typeparam name="K">Key type of the heep</typeparam>
    /// <typeparam name="E">Element type of the heep</typeparam>
    /// <remarks>Last Update: 2016-06-21</remarks>
    public class HeepArray<K, E> : Heep<K, E>
        where K : IComparable<K>
    {
        //the default base capacity
        private const int DBC = 16;

        //stores the heep in an array
        private KeyedItem<K, E>[] heep;

        //keeps track of the size seperatly
        private int size;

        /// <summary>
        /// Constructs an empty heep with the given initial capacity. It can 
        /// construct either a min-heep (default) or a max-heep.
        /// </summary>
        /// <param name="cap">The inintial capacity</param>
        /// <param name="max">Set to true to create a max-heep, set to
        /// false to create a min-heep</param>
        public HeepArray(int cap = DBC, bool max = false)
        {
            //sets up the base size to user specification
            cap = (cap < DBC) ? DBC : cap;

            //sets the initial conditions
            heep = new KeyedItem<K, E>[cap];
            maxheep = max;
            size = 0;
        }

        /// <summary>
        /// Constructs a new heep containing multiple entries. The initial capacity
        /// is derived from the size of the heep. It can construct either a min-heep 
        /// (default) or a max-heep.
        /// </summary>
        /// <param name="pairs">The entries into the table</param>
        /// <param name="max">Set to true to create a max-heep, set to
        /// false to create a min-heep</param>
        public HeepArray(IEnumerable<KeyedItem<K, E>> pairs, bool max = false)
        {
            //generates base size from number of items
            int cap = (int)(pairs.Count() * 1.25) + 1;
            cap = (cap < DBC) ? DBC : cap;

            //sets the initial conditions
            heep = new KeyedItem<K, E>[cap];
            maxheep = max;
            size = 0;

            //adds the key-value pairs one at a time
            foreach (var pair in pairs) Add(pair.Key, pair.Item);
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
        public HeepArray(IEnumerable<E> items, Func<E, K> selector, bool max = false)
        {
            //generates base size from number of items
            int cap = (int)(items.Count() * 1.25) + 1;
            cap = (cap < DBC) ? DBC : cap;

            //sets the initial conditions
            heep = new KeyedItem<K, E>[cap];
            maxheep = max;
            size = 0;

            //adds the key-value pairs one at a time
            foreach (var item in items)
            {
                var key = selector.Invoke(item);
                Add(key, item);
            }
        }

        #region Class Properties...

        /// <summary>
        /// Represents the number of items in the heep.
        /// </summary>
        public override int Count
        {
            get { return size; }
        }

        /// <summary>
        /// Represents the highest priority item in the heep. What constitutes the
        /// highest priority is dependent on wheather it is a min-heep or a max-heep.
        /// </summary>
        public override E TopItem
        {
            get 
            {
                if (size < 1) return default(E);
                else return heep[0].Item;
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
                if (size < 1) return default(K);
                else return heep[0].Key;
            }
        }

        /// <summary>
        /// Represents the heep's current capacity; the number of items the 
        /// list is able to store without resizing, as opposed to the number
        /// it actually contains.
        /// </summary>
        public int Capacity
        {
            get
            {
                return heep.Length;
            }
            set
            {
                //makes sure the capacity is of valid size
                int min = Math.Max(DBC, size);
                int cap = Math.Max(min, value);

                Resize(cap);
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
            //we can't contain the item if we're empty
            if (size == 0) return false;

            //uses a fake keyed item to search the heep
            var fake = new KeyedItem<K, E>(key);

            //uses a queue to aid in the search
            var search = new DequeArray<Int32>(heep.Length);
            search.PushBack(0);

            using (search)
            {
                while (!search.Empty)
                {
                    int index = search.PopFront();
                    int comp = Compare(heep[index], fake);

                    //determins if we keep searching this branch
                    if (comp == 0) return true;
                    if (comp > 0) continue;

                    //finds the children of the node
                    int left = (2 * index) + 1;
                    int right = left + 1;

                    //pushes the children we still need to search
                    if (left < size) search.PushBack(left);
                    if (right < size) search.PushBack(right);
                }
            }

            //we've failed to find the item
            return false;
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

            //resizes the array if we've run out of room
            if (size >= heep.Length) Resize(size * 2);
            int index = size;

            //creates a new key-value pair
            var pair = new KeyedItem<K, E>(key, item);

            //inserts the item at the end of the array
            heep[size] = pair;
            size = size + 1;

            while (index > 0)
            {
                //finds the parent index
                int parent = (index - 1) / 2;
                var temp = heep[parent];

                //we've found our resting place
                if (Compare(pair, temp) >= 0) break;

                //swaps the curent node with it's parent
                heep[index] = temp;
                heep[parent] = pair;

                //advances the index
                index = parent;
            }
        }

        /// <summary>
        /// Removes the top-most item from the heep and returns its value. What 
        /// constitutes the top-most item is dependent on wheather it is a min-heep 
        /// or a max-heep. It reutrns null if the heep is empty.
        /// </summary>
        /// <returns>The highest priority item, or null if empty</returns>
        public override E Dequeue()
        {
            //calls the more general method
            K temp; return Dequeue(out temp);
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
            //checks to make shure the heep isn't empty
            if (size == 0)
            {
                key = default(K);
                return default(E);
            }

            //replaces the root with the last element
            var temp = heep[0];
            heep[0] = heep[size - 1];
            heep[size - 1] = null;
            size = size - 1;

            //used in restoring the heap
            var root = heep[0];
            int index = 0;

            while (index < size)
            {
                //finds the indicies of the children
                int left = (2 * index) + 1;
                int right = left + 1;
                int min = index;

                //determins which child (if any) is the smaller
                if (left < size && CompIndex(left, min) < 0) min = left;
                if (right < size && CompIndex(right, min) < 0) min = right;

                //if the curent node is the smallest, we're done
                if (min == index) break;

                //swaps with the child and updates
                heep[index] = heep[min];
                heep[min] = root;
                index = min;
            }

            //returns the deleted root
            key = temp.Key;
            return temp.Item;
        }

        /// <summary>
        /// Creates an enumeration over all the keys and values in 
        /// the heep, together as keyed items.
        /// </summary>
        /// <returns>An enumeration of keyed items</returns>
        public override IEnumerator<KeyedItem<K, E>> GetEnumerator()
        {
            //simply itterates over all items in level order
            for (int i = 0; i < size; i++) yield return heep[i];
        }

        /// <summary>
        /// Removes all items from the heep. Preventing any
        /// potential memory leaks.
        /// </summary>
        public override void Clear()
        {
            //resets the initial conditions
            heep = new KeyedItem<K, E>[DBC];
            size = 0;
        }

        #endregion ///////////////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method, compares the items stored at two diffrent indices,
        /// using the inherited comparison method to maintain heep order.
        /// </summary>
        /// <param name="index1">Index of the first item</param>
        /// <param name="index2">Index of the second item</param>
        /// <returns>A negitive number if item1 comes first, a positive
        /// number if it comes second, and zero if they are equil</returns>
        private int CompIndex(int index1, int index2)
        {
            var pair1 = heep[index1];
            var pair2 = heep[index2];
            return Compare(pair1, pair2);
        }

        /// <summary>
        /// Helper method for resizing the internal array.
        /// </summary>
        /// <param name="new_size">The new size of the array</param>
        private void Resize(int new_size)
        {
            var temp = new KeyedItem<K, E>[new_size];
            for (int index = 0; index < size; index++)
                temp[index] = heep[index];
            heep = temp;
        }

        #endregion ///////////////////////////////////////////////////////////////////


    }
}
