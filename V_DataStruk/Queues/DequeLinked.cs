using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.Queues
{
    /// <summary>
    /// This implementation of a Deque uses a doubly-linked list of nodes to 
    /// store it's data. However, it only maintains a reference to the front 
    /// and back of the deque, allowing the list to grow without bound. When 
    /// an item is inserted it creates a new node and links it to the head, 
    /// making it the new head. Likewise, when an item is removed it simply 
    /// moves the head to the next node, disposing of the original. This makes
    /// it very efficient, and it never has to deal with array resizing.
    /// </summary>
    /// <typeparam name="E">The element type of the deque</typeparam>
    /// <remarks>Last Update: 2016-05-31</remarks>
    public sealed class DequeLinked<E> : Deque<E>
    {
        #region Class Definitions...

        //maintains the size of the deque
        private int size;

        //reference to front and back of deque
        private NodeLiniar<E> head;   
        private NodeLiniar<E> tail;

        /// <summary>
        /// Constructs a new empty deque, without any items.
        /// </summary>
        public DequeLinked()
        {
            Clear();
        }

        /// <summary>
        /// Constructs a new deque initialized with the given items.
        /// </summary>
        /// <param name="items">Initial contents of the deque</param>
        public DequeLinked(params E[] items)
        {
            Clear();

            //pushes each of the items onto the deque
            foreach (E item in items) PushBack(item);
        }

        /// <summary>
        /// Constructs a new deque initialized with the given items.
        /// </summary>
        /// <param name="items">Initial contents of the deque</param>
        public DequeLinked(IEnumerable<E> items)
        {
            Clear();

            //pushes each of the items onto the deque
            foreach (E item in items) PushBack(item);
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// Determines if the deque is empty or contains items. It is set to 
        /// true if empty and false if otherwise.
        /// </summary>
        public override bool Empty
        {
            get { return (head == null); }
        }

        /// <summary>
        /// Represents the number of items in the deque.
        /// </summary>
        public override int Count
        {
            get { return size; }
        }

        /// <summary>
        /// The item currently at the front of the deque.
        /// </summary>
        public override E Front
        {
            get 
            {
                if (head == null) return default(E);
                else return head.Data;             
            }
        }

        /// <summary>
        /// The item currently at the back of the deque.
        /// </summary>
        public override E Back
        {
            get 
            {
                if (tail == null) return default(E);
                else return tail.Data;             
            }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Deque Implementation...

        /// <summary>
        /// Attempts to determine if the given item is contained within the 
        /// deque by basically iterating over the entire collection. It is not
        /// recommended for frequent use.
        /// </summary>
        /// <param name="item">Item to find in the deque</param>
        /// <returns>True if the item is in the deque, and false if otherwise
        /// </returns>
        public override bool Contains(E item)
        {
            //we can't contain the item if we're empty
            if (head == null) return false;
            NodeLiniar<E> temp = head;

            //checks each item from head to tail
            while (true)
            {
                if (temp.Data.Equals(item)) return true;
                if (temp == tail) return false;
                temp = temp.Next;
            }
        }

        /// <summary>
        /// Inserts an item onto the back of the Deque. Taken together with 
        /// PopFront() it can be used to implement a Queue, with First-In
        /// First-Out order.
        /// </summary>
        /// <param name="item">Item to insert</param>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public override void PushBack(E item)
        {
            //we do not allow the insertion of null elements
            if (item == null) throw new ArgumentNullException("item");

            //creates a new node to insert into the deque
            NodeLiniar<E> temp = new NodeLiniar<E>(item);

            if (tail == null)
            {
                //creates the deque with a single node
                head = tail = temp;
                temp.Next = null;
                temp.Prev = null;
            }
            else
            {
                //adds the node onto the back of the deque
                tail.Next = temp;
                tail = temp;
            }

            size++;
        }

        /// <summary>
        /// Inserts an item onto the front of the Deque. Taken together with 
        /// PopFront() it can be used to implement a Stack, with Last-In
        /// First-Out order.
        /// </summary>
        /// <param name="item">Item to insert</param>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public override void PushFront(E item)
        {
            //we do not allow the insertion of null elements
            if (item == null) throw new ArgumentNullException("item");

            //creates a new node to insert into the deque
            NodeLiniar<E> temp = new NodeLiniar<E>(item);

            if (head == null)
            {
                //creates the deque with a single node
                head = tail = temp;
                temp.Next = null;
                temp.Prev = null;
            }
            else
            {
                //adds the node onto the front of the deque
                head.Prev = temp;
                head = temp;
            }

            size++;
        }

        /// <summary>
        /// Removes an item from the back of the Deque. This method is not 
        /// commonly used, but is provided for sake of completeness. It 
        /// returns null if the Deque is currently empty.
        /// </summary>
        /// <returns>The removed item, or null if empty</returns>
        public override E PopBack()
        {
            //makes sure the deque is not empty
            if (head == null) return default(E);
       
            //store the item to return at the end
            E item = tail.Data;
            
            if (size == 1)
            {
                //clears the last item from the deque
                tail.Dispose();
                head = tail = null;
            }
            else
            {
                //removes the item from the tail and moves on
                var temp = tail;
                tail = tail.Prev;
                temp.Dispose();
            }

            size--;
            return item;
        }

        /// <summary>
        /// Removes an item from the front of the Deque. This method is used 
        /// in the implementation of both Stacks and Queues. It returns null
        /// if the Deque is currently empty.
        /// </summary>
        /// <returns>The removed item, or null if empty</returns>
        public override E PopFront()
        {
            //makes sure the deque is not empty
            if (head == null) return default(E);
       
            //store the item to return at the end
            E item = head.Data;
            
            if (size == 1)
            {
                //clears the last item from the deque
                head.Dispose();
                head = tail = null;
            }
            else
            {
                //removes the item from the head and moves on
                var temp = head;
                head = head.Next;
                temp.Dispose();
            }

            size--;
            return item;
        }

        /// <summary>
        /// Removes all items from the deque. Causing the collection to revert 
        /// to it's original initialized state.
        /// </summary>
        public override void Clear()
        {
            //used in disposing of each node
            NodeLiniar<E> curr = head;
            NodeLiniar<E> temp = curr;

            //disposes of each node, from head to tail
            while (curr != null)
            {
                temp = curr.Next;
                curr.Dispose();
                curr = temp;
            }

            //sets the initial conditions
            head = tail = null;
            size = 0;
        }

        /// <summary>
        /// Generates an enumeration of all the items in the deque, ranging
        /// from the front to the back.
        /// </summary>
        /// <returns>An enumeration of the deque</returns>
        public override IEnumerator<E> GetEnumerator()
        {
            //nothing to iterate if the deque is empty
            if (head == null) yield break;
            NodeLiniar<E> temp = head;

            //follows each item from head to tail
            while (true)
            {
                yield return temp.Data;
                if (temp == tail) yield break;
                temp = temp.Next;
            }
        }

        #endregion /////////////////////////////////////////////////////////////
    }
}