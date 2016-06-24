using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.Lists
{
    /// <summary>
    /// This implementation of a Sorted List uses a circular chain of nodes to
    /// store it's items. This allows for fast insertion and deletion at the
    /// expense of slower look-up times. It also avoids the problems of array
    /// resizing. It benefits from the same concept of spacial locality that 
    /// linked lists do, making access times short when the items are close 
    /// together. This means that it is ideal for data that is accessed in 
    /// near sorted order.
    /// </summary>
    /// <typeparam name="E">The element type of the list</typeparam>
    /// <remarks>Last Update: 2016-05-30</remarks>
    public sealed class SortListLinked<E> : SortList<E>
        where E : IComparable<E>
    {
        #region Class Deffinitions...

        //references the location the user is at currently
        private NodeLiniar<E> loc;
        private NodeLiniar<E> apex;
        private int loc_pos;

        /// <summary>
        /// Constructs a new empty list, without any items. A comparison 
        /// function may be provided, specifying how items should be compared
        /// when sorting the list.
        /// </summary>
        /// <param name="comp">Comparison operator to be used</param>
        public SortListLinked(Comparison<E> comp = null)
        {
            //initializes the linked list
            comparer = comp;
            Clear();
        }

        /// <summary>
        /// Constructs a new list, containing multiple items.
        /// </summary>
        /// <param name="items">The items in the list</param>
        public SortListLinked(params E[] items)
        {
            //initializes the linked list
            comparer = null;
            Clear();

            //inserts the items one by one
            foreach (E item in items) Add(item);
        }

        /// <summary>
        /// Constructs a new list, containing multiple items.
        /// </summary>
        /// <param name="items">The items in the list</param>
        public SortListLinked(IEnumerable<E> items)
        {
            //initializes the linked list
            comparer = null;
            Clear();

            //inserts the items one by one
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

        #region Sorting Implementation...

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
            //calls upon the more specialized method
            int index = LocateIndex(item);
            return (index >= 0) ? index : default(int?);
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
            NodeLiniar<E> where = LocateNode(temp.Value);
            return where.Data;
        }

        /// <summary>
        /// Inserts an item into the list, keeping sorted order. It also 
        /// reports the index of the item's insertion.
        /// </summary>
        /// <param name="item">The item to be inserted</param>
        /// <returns>Index where the item was inserted</returns>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public override int Insert(E item)
        {
            //we do not allow the insertion of null elements
            if (item == null) throw new ArgumentNullException("item");

            //generates a new node with the item to insert
            NodeLiniar<E> node = new NodeLiniar<E>(item);

            //takes care of initial node
            if (this.size == 0)
            {
                node.Next = node;
                node.Prev = node;
                loc = apex = node;
                loc_pos = 0;
                size = 1;
                return loc_pos;
            }

            //gets the parent node
            NodeLiniar<E> curr = LocateInsert(item);

            //inserts the node between curr and prev
            NodeLiniar<E> prev = curr.Prev;
            prev.Next = node;
            curr.Prev = node;
            loc = node;

            //updates size and the apex
            if (size % 2 == 0) apex = apex.Prev;
            this.size++; return loc_pos;
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
            NodeLiniar<E> where = LocateNode(test.Value);
            E data = where.Data;

            //takes care of deleting last item
            if (this.size == 1)
            {                
                Clear();
                return where.Data;
            }

            //finds the neighboring nodes
            NodeLiniar<E> prev = where.Prev;
            NodeLiniar<E> after = where.Next;
            where.Dispose();

            //removes the node from the loop
            prev.Next = after;
            after.Prev = prev;
            loc = after;
            loc_pos = test.Value;

            //shifts the apex if necessary
            if (size % 2 == 1) apex = apex.Next;

            this.size--;
            loc_pos = loc_pos % size;
            return where.Data;
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
            apex = loc = null;
            loc_pos = -1;
            this.size = 0;
        }

        /// <summary>
        /// Creates an enumeration over all the items in the list.
        /// </summary>
        /// <returns>An enumerator over the list</returns>
        public override IEnumerator<E> GetEnumerator()
        {
            //makes the iteration start at index zero
            NodeLiniar<E> head = LocateNode(0);
            NodeLiniar<E> reff = head;

            //if we are empty there is nothing to return
            if (head == null) yield break;

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
        /// Helper method which rotates the circular list till the current 
        /// location is pointing to the desired index. It always rotates the 
        /// short way, so it never traverse more than half of the list. It 
        /// updates the loc and apex pointers.
        /// </summary>
        /// <param name="index">Index of desired node</param>
        /// <returns>Node at the desired index</returns>
        private NodeLiniar<E> LocateNode(int index)
        {
            //checks for the simple cases
            if (this.size == 0) return null;
            if (index == loc_pos) return loc;

            //used in computing the minimum distance
            int min_dist, dist1, dist2;

            //computes both distances to the desired index
            dist1 = index - loc_pos;
            if (index < loc_pos) dist2 = dist1 + this.size;
            else dist2 = dist1 - this.size;

            //finds the minimum distance in absolute value
            if (Math.Abs(dist1) < Math.Abs(dist2)) min_dist = dist1;
            else min_dist = dist2;

            if (min_dist < 0)
            {
                //negative distance means use prev links
                for (int x = 1; x <= -min_dist; x++)
                {
                    loc = loc.Prev;
                    apex = apex.Prev;
                }
            }
            else
            {
                //positive distance means use next links
                for (int x = 1; x <= min_dist; x++)
                {
                    loc = loc.Next;
                    apex = apex.Next;
                }
            }

            loc_pos = index;
            return loc;
        }

        /// <summary>
        /// Helper method which searches through the circular list till the 
        /// current location matches the target item. It always rotates the 
        /// short way, so it never traverse more than half of the list. It 
        /// updates the loc and apex pointers.
        /// </summary>
        /// <param name="item">The desired item</param>
        /// <returns>Index of the desired item</returns>
        private int LocateIndex(E item)
        {
            if (this.size == 0) return -1;

            //used in searching the list
            NodeLiniar<E> check = apex;
            bool dir = SearchDir(item);

            while (true)
            {
                //determines if the target has been located
                int comp = Compare(item, loc.Data);
                if (comp == 0) return loc_pos;
                if (loc == check) return -1;

                if (dir)
                {
                    //searches to the right
                    loc = loc.Next;
                    apex = apex.Next;
                    loc_pos++;
                }
                else
                {
                    //searches to the left
                    loc = loc.Prev;
                    apex = apex.Prev;
                    loc_pos--;
                }

                //wraps around the index values
                if (loc_pos < 0) loc_pos = size - 1;
                if (loc_pos >= size) loc_pos = 0;
            }
        }

        /// <summary>
        /// Helper method used to locate the node to insert a new item behind. 
        /// It returns null if no such node exists. It never runs through more 
        /// than half the list. It updates the loc and apex pointers.
        /// </summary>
        /// <param name="item">Item to be inserted</param>
        /// <returns>Node to insert behind</returns>
        private NodeLiniar<E> LocateInsert(E item)
        {
            if (this.size == 0) return null;

            if (!SearchDir(item))
            {
                //swaps the location and the apex
                NodeLiniar<E> temp = loc;
                loc = apex;
                apex = temp;

                //keeps the apex left heavy
                loc_pos = (loc_pos + size / 2) % size;
                if (size % 2 == 1) apex = apex.Prev;
            }

            //rotate to zero if the target is below our search
            if (Compare(item, loc.Data) < 0) LocateNode(0);

            while (true)
            {
                //determines in the insertion index is found
                int comparison = Compare(item, loc.Data);
                if (comparison <= 0) return loc;

                //searches (only) to the right
                loc = loc.Next;
                apex = apex.Next;
                loc_pos++;

                //determines if the threshold is crossed
                if (loc_pos == size) return loc;
            }
        }

        /// <summary>
        /// Helper method that determines weather an algorithm should search 
        /// to the left or the right to find a particular node, so as to 
        /// minimize the distance necessary to travel.
        /// </summary>
        /// <param name="target">The target item</param>
        /// <returns>True to search to the right, and false to search to the 
        /// left</returns>
        private bool SearchDir(E target)
        {
            //preforms the three test cases
            bool testA = Compare(target, loc.Data) > 0;
            bool testB = Compare(target, apex.Data) > 0;
            bool testC = Compare(loc.Data, apex.Data) > 0;

            //determines whether to look right or left
            bool dir = (testA && testC);
            dir = dir || (!testB && testC);
            dir = dir || (testA && !testB);

            return dir;
        }

        #endregion /////////////////////////////////////////////////////////////

    }
}