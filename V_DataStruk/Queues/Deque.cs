﻿/**
 *  This file is an integral part of the Vulpine Core Library
 *  Copyright (c) 2016-2018 Benjamin Jacob Dawson
 *
 *      http://www.jakesden.com/corelibrary.html
 *
 *  The Vulpine Core Library is free software; you can redistribute it 
 *  and/or modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License, or (at your option) any later version.
 *
 *  The Vulpine Core Library is distributed in the hope that it will 
 *  be useful, but WITHOUT ANY WARRANTY; without even the implied 
 *  warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 *  See the GNU Lesser General Public License for more details.
 *
 *      https://www.gnu.org/licenses/lgpl-2.1.html
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.Queues
{
    /// <summary>
    /// A Deque, sometimes referred to as a double ended queue, is a list-like 
    /// data structure which allows insertion or removed of items at either 
    /// end of the deque. Two of the most common data structures in computer 
    /// science, stacks and queues, can both be implemented using deques. By 
    /// using PushFirst() and PopFirst() one can obtain a last-in first-out 
    /// order. By using PushLast() and PopFirst() a first-in first-out order 
    /// can be obtained. Although other data structures can also preform these 
    /// tasks, a deque is often more efficient due to it's limited interface.
    /// </summary>
    /// <typeparam name="E">The element type of the deque</typeparam>
    /// <remarks>Last Update: 2016-05-30</remarks>
    public abstract class Deque<E> : VCollection<E>
    {
        #region Class Definitions...

        /// <summary>
        /// Generates a string representation of the deque, displaying the 
        /// size of the deque, as well as the first and last items in the 
        /// deque.
        /// </summary>
        /// <returns>The deque as a string</returns>
        public override string ToString()
        {
            if (this.Empty) return "Empty Deque";
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Deque[{0}] (", Count);
            sb.Append(Front);

            if (Count > 1)
            {
                sb.Append(" - ");
                sb.Append(Back);
            }

            sb.Append(")");
            return sb.ToString();
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// The item currently at the front of the deque.
        /// </summary>
        public abstract E Front { get; }

        /// <summary>
        /// The item currently at the back of the deque.
        /// </summary>
        public abstract E Back { get; }

        #endregion /////////////////////////////////////////////////////////////

        #region Collection Implementation...

        /// <summary>
        /// Inserts an item onto the back of the Deque, resulting in a 
        /// default Queue style implementation.
        /// </summary>
        /// <param name="item">Item to be inserted</param>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public override void Add(E item)
        {
            //calls the abstract method
            PushBack(item);
        }

        /// <summary>
        /// Removes an item from the front of the Deque, resulting in a 
        /// default Queue style implementation. It returns null if the Deque 
        /// is currently empty.
        /// </summary>
        /// <returns>The removed item, or null if empty</returns>
        public override E Dequeue()
        {
            //calls the abstract method
            return PopFront();
        }

        /// <summary>
        /// This method is provided solely for the implementation of the 
        /// Collection interface. This method should never be called during 
        /// normal operating code. If you need to be able to remove arbitrary 
        /// items, consider using a different data structure.
        /// </summary>
        /// <param name="item">Item to be removed</param>
        /// <returns>Throws Exception</returns>
        /// <exception cref="NotSupportedException">It is not possible to 
        /// remove arbitrary items from a deque</exception>
        public override bool Remove(E item)
        {
            throw new NotSupportedException();
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Abstract Methods...

        /// <summary>
        /// Inserts an item onto the back of the Deque. Taken together with 
        /// PopFront() it can be used to implement a Queue, with First-In 
        /// First-Out order.
        /// </summary>
        /// <param name="item">Item to insert</param>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public abstract void PushBack(E item);

        /// <summary>
        /// Inserts an item onto the front of the Deque. Taken together with
        /// PopFront() it can be used to implement a Stack, with Last-In 
        /// First-Out order.
        /// </summary>
        /// <param name="item">Item to insert</param>
        /// <exception cref="ArgumentNullException">If null is inserted
        /// </exception>
        public abstract void PushFront(E item);

        /// <summary>
        /// Removes an item from the back of the Deque. This method is not 
        /// commonly used, but is provided for sake of completeness. It 
        /// returns null if the Deque is currently empty.
        /// </summary>
        /// <returns>The removed item, or null if empty</returns>
        public abstract E PopBack();

        /// <summary>
        /// Removes an item from the front of the Deque. This method is used
        /// in the implementation of both Stacks and Queues. It returns null
        /// if the Deque is currently empty.
        /// </summary>
        /// <returns>The removed item, or null if empty</returns>
        public abstract E PopFront();

        #endregion /////////////////////////////////////////////////////////////
    }
}