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
    /// This implementation of List uses a circular chain of nodes to store its 
    /// items. As such, it must search through the items in-order to find a
    /// particular index. However, it always retains the last position searched, 
    /// exploiting locality of reference to increase speed. Insertion and 
    /// deletion happen very fast, once the desired index has been located. 
    /// Linked lists run very fast when most operations are close in index to 
    /// the previous operation. In the worst case however, all operations are 
    /// O(n).
    /// </summary>
    /// <typeparam name="E">The element type of the list</typeparam>
    /// <remarks>Last Update: 2016-05-19</remarks>
    public sealed class VListLinked<E> : VList<E>
    {
        #region Class Deffinitions...

        //references the location the user is at currently
        private NodeLiniar<E> loc;
        private int loc_pos;

        /// <summary>
        /// Constructs a new empty list, without any items.
        /// </summary>
        public VListLinked()
        {
            Clear();
        }

        /// <summary>
        /// Constructs a new list, containing multiple items.
        /// </summary>
        /// <param name="items">The items of the list</param>
        public VListLinked(params E[] items)
        {
            Clear();

            //inserts the items in order to avoid searching
            for (int i = 0; i < items.Length; i++)
                Insert(i, items[i]);
        }

        /// <summary>
        /// Constructs a new list, containing multiple items.
        /// </summary>
        /// <param name="items">The items of the list</param>
        public VListLinked(IEnumerable<E> items)
        {
            Clear();

            //inserts the items in order to avoid searching
            foreach (E item in items) Add(item);
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// The last index that was used to access the list. This determines 
        /// how quickly other parts of the list can be reached.
        /// </summary>
        public int LastIndex
        {
            get { return loc_pos; }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region List Implementation...

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
            //we can't contain null, so return null
            if (item == null) return null;

            //if we are empty, we can't contain the item
            if (loc == null) return null;

            //used in searching for the item
            NodeLiniar<E> first = loc;

            while (true)
            {
                //checks if we have found our item
                if (loc.Data.Equals(item)) return loc_pos;

                //advances the current location
                loc_pos = (loc_pos + 1) % size;
                loc = loc.Next;

                //if we loop back around, then we don't have it
                if (loc == first) return null;
            }
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
            //if the index is invalid there is nothing to do
            int? temp = index.GetIndex(this.size);
            if (!temp.HasValue) return default(E);

            //get reference to node, then data in node
            NodeLiniar<E> where = Find(temp.Value);
            E data = where.Data;

            //stay at location of most recent activity
            loc = where;
            loc_pos = temp.Value;
            return data;
        }

        /// <summary>
        /// Sets the value at a given index in the list. Negative indices 
        /// indicate positions from the end of the list. If the index is 
        /// invalid, then an exception is thrown.
        /// </summary>
        /// <param name="index">The index to be changed</param>
        /// <param name="item">The value to be set</param>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">If the given index 
        /// is invalid for the current list</exception>
        public override void SetItem(Index index, E item)
        {
            //we do not allow the insertion of null elements
            if (item == null) throw new ArgumentNullException("item");

            //obtains a valid index
            int? x = index.GetIndex(this.size);
            if (!x.HasValue) throw new ArgumentOutOfRangeException("index");

            //finds the item and updates our last position
            loc = Find(x.Value);
            loc.Data = item;
            loc_pos = x.Value;
        }

        /// <summary>
        /// Inserts an item into the list before the given index. Negative 
        /// indices indicate positions from the end of the list. If the index 
        /// is invalid, then the item is inserted at the end of the list.
        /// </summary>
        /// <param name="index">The index of insertion.</param>
        /// <param name="item">The item to be inserted</param>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public override void Insert(Index index, E item)
        {
            //we do not allow the insertion of null elements
            if (item == null) throw new ArgumentNullException("item");

            //creates a new node for the item
            NodeLiniar<E> node = new NodeLiniar<E>(item);

            if (this.size == 0)
            {
                //takes care of the initial node
                node.Next = node;
                node.Prev = node;
                loc = node;
                loc_pos = 0;
            }
            else
            {
                int x = index.GetIndex(size) ?? size;

                //obtains the nodes to insert between
                NodeLiniar<E> curr = Find(x);
                NodeLiniar<E> prev = curr.Prev;

                //sets the new node between prev and curr
                prev.Next = node;
                curr.Prev = node;
                loc = node;
                loc_pos = x;
            }

            this.size++;
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

            //retrieves the data being removed
            int x = test.Value;
            E data = Find(x).Data;

            if (this.size == 1)
            {
                //takes care of deleting last item
                loc.Dispose();
                loc = null;
                loc_pos = -1;
            }
            else
            {
                //finds the neighboring nodes
                NodeLiniar<E> cur = Find(x);
                NodeLiniar<E> prev = cur.Prev;
                NodeLiniar<E> after = cur.Next;

                //links the neighbors to each other
                prev.Next = after;
                after.Prev = prev;

                cur.Dispose();
                loc = after;
                loc_pos = x % (size - 1);
            }

            this.size--;
            return data;
        }

        /// <summary>
        /// Removes all items from the list, preventing memory leaks.
        /// </summary>
        public override void Clear()
        {
            //must set this in order to break the cycle
            if (loc != null) loc.Prev.Next = null;
            NodeLiniar<E> curr = loc;

            //disposes of each node in order
            while (curr != null)
            {
                NodeLiniar<E> temp = curr.Next;
                curr.Dispose();
                curr = temp;
            }

            //sets initial conditions
            loc = null;
            loc_pos = -1;
            this.size = 0;
        }

        /// <summary>
        /// Creates an enumeration over all the items in the list.
        /// </summary>
        /// <returns>An enumerator over the list</returns>
        public override IEnumerator<E> GetEnumerator()
        {
            //there is nothing to iterate in an empty list
            if (loc == null) yield break;

            //makes the iteration start at index zero
            NodeLiniar<E> head = Find(0);
            NodeLiniar<E> reff = head;

            while (true)
            {
                //returns the item and advances
                yield return reff.Data;
                reff = reff.Next;

                //break off once we've cycled back around
                if (reff == head) yield break;
            }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method that locates a specified node in the circle. It dose 
        /// not update the local index pointer.
        /// </summary>
        /// <param name="index">Index of the desired node</param>
        /// <returns>The desired node</returns>
        private NodeLiniar<E> Find(int index)
        {
            NodeLiniar<E> where = loc;
            int min_dist, dist1, dist2;

            //checks for an empty list
            if (this.size == 0) return null;

            //takes care of the simple case
            if (index == loc_pos) return loc;

            //computes both distances to the desired index
            dist1 = index - loc_pos;
            if (index < loc_pos) dist2 = dist1 + this.size;
            else dist2 = dist1 - this.size;

            //finds the minimum distance in absolute value
            if (Math.Abs(dist1) <= Math.Abs(dist2)) min_dist = dist1;
            else min_dist = dist2;

            if (min_dist < 0)
            {
                //negative distance means use prev links
                for (int x = 1; x <= -min_dist; x++)
                    where = where.Prev;
            }
            else
            {
                //positive distance means use next links
                for (int x = 1; x <= min_dist; x++)
                    where = where.Next;
            }

            return where;
        }

        #endregion /////////////////////////////////////////////////////////////
    }
}