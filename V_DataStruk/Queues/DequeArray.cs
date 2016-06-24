using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.Queues
{
    /// <summary>
    /// This implementation of a Deque uses an internal array to store it's
    /// items. It treats the array as if it were a circular buffer. Instead
    /// of shifting items when pushing or popping, the way a list dose, it
    /// simply updates a pointer to the front or back of the deque. This
    /// makes insertion and removal constant time operations. Because it is
    /// array-based, it has an internal capacity that is greater than it's
    /// size. If the deque grows beyond it's given capacity, it must take 
    /// time to resize the array. The default capacity can be set at 
    /// initialization, if the maximum size of the deque is known ahead 
    /// of time.
    /// </summary>
    /// <typeparam name="E">The element type of the deque</typeparam>
    /// <remarks>Last Update: 2016-05-31</remarks>
    public sealed class DequeArray<E> : Deque<E>
    {
        #region Class Definitions...

        //the default base capacity
        private const int DBC = 16;

        //reference to front and back of deque
        private int head;
        private int tail;

        //stores the actual deque array
        private E[] buffer;

        /// <summary>
        /// constructs an empty deque with a given default capacity. Although
        /// the deque may grow beyond this capacity as items are added, it
        /// will not shrink below it.
        /// </summary>
        /// <param name="cap">The initial default capacity</param>
        public DequeArray(int cap = DBC)
        {
            //initializes the base size to a valid value
            cap = (cap < DBC) ? DBC : cap;

            Reset(cap);
        }

        /// <summary>
        /// Constructs a new deque, containing multiple items. The default 
        /// capacity is set slightly larger than the initial size, allowing
        /// for the insertion of additional items.
        /// </summary>
        /// <param name="items">Initial contents of the deque</param>
        public DequeArray(params E[] items)
        {
            //generates base capacity from number of items
            int cap = (int)(items.Length * 1.25) + 1;
            cap = (cap < DBC) ? DBC : cap;

            Reset(cap);

            //pushes each of the items onto the deque
            foreach (E item in items) PushBack(item);
        }

        /// <summary>
        /// Constructs a new deque, containing multiple items. The default
        /// capacity is set slightly larger than the initial size, allowing
        /// for the insertion of additional items.
        /// </summary>
        /// <param name="items">Initial contents of the deque</param>
        public DequeArray(IEnumerable<E> items)
        {
            //generates base capacity from number of items
            int cap = (int)(items.Count() * 1.25) + 1;
            cap = (cap < DBC) ? DBC : cap;

            Reset(cap);

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
            get { return (head < 0);  }
        }

        /// <summary>
        /// Represents the number of items in the deque.
        /// </summary>
        public override int Count
        {
            get 
            {
                //determines if the deque is empty
                if (head < 0) return 0;

                //calculates size from head and tail
                int count = (tail - head) + 1;
                if (count <= 0) count = buffer.Length + count;
                return count;
            }
        }

        /// <summary>
        /// The item currently at the front of the deque.
        /// </summary>
        public override E Front
        {
            get
            {
                if (head < 0) return default(E);
                else return buffer[head];
            }
        }

        /// <summary>
        /// The item currently at the back of the deque.
        /// </summary>
        public override E Back
        {
            get
            {
                if (tail < 0) return default(E);
                else return buffer[tail];
            }
        }

        /// <summary>
        /// Represents the deque's current capacity; the number of items the 
        /// deque is able to store without resizing, as opposed to the number
        /// it actually contains.
        /// </summary>
        public int Capacity
        {
            get
            {
                //simply returns the size of the buffer
                return buffer.Length;
            }
            set
            {
                //makes sure the capacity is of valid size
                int min = Math.Max(DBC, Count);
                int cap = Math.Max(min, value);

                Resize(cap);
            }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Queue Implementation...

        /// <summary>
        /// Attempts to determine if the given item is contained within the 
        /// deque by basically iterating over the entire collection. It is
        /// not recommended for frequent use.
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>True if the item is in the deque, and false if otherwise
        /// </returns>
        public override bool Contains(E item)
        {
            //we can't contain the item if were empty
            if (head < 0) return false;
            int index = head;

            //looks at each item between the head and tail
            while (true) 
            {
                if (buffer[index].Equals(item)) return true;
                if (index == tail) return false;
                index = (index + 1) % buffer.Length;
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

            //determines if the array needs resizing
            if (Count >= buffer.Length) Resize(Count * 2);

            if (head < 0)
            {
                //takes care of first item
                head = tail = 0;
                buffer[head] = item;
            }
            else
            {
                //adds the item and moves the tail forward
                tail = (tail + 1) % buffer.Length;
                buffer[tail] = item;
            }          
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

            //determines if the array needs resizing
            if (Count >= buffer.Length) Resize(Count * 2);

            if (head < 0)
            {
                //takes care of first item
                head = tail = 0;
                buffer[head] = item;
            }
            else
            {
                //adds the item and moves the head backward
                if (head > 0) head = head - 1;
                else head = buffer.Length - 1;
                buffer[head] = item;
            }          
        }

        /// <summary>
        /// Removes an item from the back of the Deque. This method is not 
        /// commonly used, but is provided for sake of completeness. It 
        /// returns null if the Deque is currently empty.
        /// </summary>
        /// <returns>The removed item, or null if empty</returns>
        public override E PopBack()
        {
            //makes certain that the deque is not empty
            if (head < 0) return default(E);

            //obtains the item and overwrites it
            E item = buffer[tail];
            buffer[tail] = default(E);

            //updates the tail position or resets the deque
            if (head == tail) head = tail = -1;
            else if (tail > 0) tail = tail - 1;
            else tail = buffer.Length - 1;

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
            //makes certain that the deque is not empty
            if (head < 0) return default(E);

            //obtains the item and overwrites it
            E item = buffer[head];
            buffer[head] = default(E);

            //updates the head position or resets the deque
            if (head == tail) head = tail = -1;
            else head = (head + 1) % buffer.Length; 

            return item;
        }

        /// <summary>
        /// Removes all items from the deque. Causing the collection to 
        /// revert to it's original initialized state.
        /// </summary>
        public override void Clear()
        {
            head = tail = -1;
            buffer = new E[DBC];
        }

        /// <summary>
        /// Generates an enumeration of all the items in the deque, ranging 
        /// from the front to the back.
        /// </summary>
        /// <returns>An enumeration of the deque</returns>
        public override IEnumerator<E> GetEnumerator()
        {
            //takes care of the empty deque case
            if (head < 0) yield break;
            int index = head;

            //iterates the items from head to tail
            while (true) 
            {
                yield return buffer[index];
                if (index == tail) yield break;
                index = (index + 1) % buffer.Length;
            }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method for resizing the internal buffer. It copies all the 
        /// values offset by the head pointer.
        /// </summary>
        /// <param name="new_size">Size of the new array</param>
        private void Resize(int new_size)
        {
            E[] temp = new E[new_size];
            int offset = 0;
            int size = this.Count;

            //copies the items to the new array offset by head
            for (int index = 0; index < size; index++)
            {
                offset = (index + head) % buffer.Length;
                temp[index] = buffer[offset];
            }

            head = 0;
            tail = size - 1;
            buffer = temp;
        }

        /// <summary>
        /// Helper method for reseting the internal array, with the desired 
        /// starting capacity.
        /// </summary>
        /// <param name="cap">The starting capacity</param>
        private void Reset(int cap)
        {
            head = tail = -1;
            buffer = new E[cap];
        }

        #endregion /////////////////////////////////////////////////////////////
    }
}