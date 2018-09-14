/**
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

namespace Vulpine.Core.Data
{
    /// <summary>
    /// A Linear Node contains a single reference to the next node in sequence,
    /// which may be NULL in a terminating sequence. This way a chain of nodes can
    /// be formed, by chaining each node to the next. A Linear node also contains
    /// a special reference to the previous node in sequence. This link however is
    /// not considered one of the node's children. Updating either the Previous or
    /// Next links will overwrite the corresponding link on the corresponding node.
    /// This means that (A.Next = B) has the same effect as (B.Prev = A).
    /// </summary>
    /// <typeparam name="T">The data type of the node</typeparam>
    /// <remarks>Last Update: 2016-05-24</remarks>
    public class NodeLiniar<T> : Node<T>
    {
        #region Class Definitions...

        //the item being referenced
        private T item;

        //the neighboring nodes in the chain
        private NodeLiniar<T> next;
        private NodeLiniar<T> prev;

        /// <summary>
        /// Constructs a new node.
        /// </summary>
        /// <param name="item">The data inside the node</param>
        /// <param name="prev">The node's predecessor</param>
        /// <param name="next">The node's successor</param>
        public NodeLiniar(T item, NodeLiniar<T> prev = null, 
            NodeLiniar<T> next = null)
        {
            //sets the item of the node
            this.item = item;

            //sets the neighbors using the property values
            this.Next = next;
            this.Prev = prev;
        }

        /// <summary>
        /// Generates a string representation of the node, repeating the
        /// contents of the node in string format.
        /// </summary>
        /// <returns>The node in string format</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Node: ");
            if (item == null) sb.Append("<NULL>");
            else sb.Append(item);

            if (next != null)
            {
                sb.Append(" -> ");
                if (next.item == null) sb.Append("<NULL>");
                else sb.Append(next.item);
            }

            return sb.ToString();
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Node Linkings...
    
        /// <summary>
        /// The data contained within the node.
        /// </summary>
        public T Data
        {
            get { return item; }
            set { item = value; }
        }

        /// <summary>
        /// The successor node. Changing this value updates the new 
        /// successor's predecessor to reflect the change. To clear
        /// the value, set it to null.
        /// </summary>
        public NodeLiniar<T> Next
        {
            get 
            { 
                return next; 
            }
            set 
            {
                next = value;
                if (next != null) 
                next.prev = this;
            }         
        }

        /// <summary>
        /// The predecessor node. Changing this value updates the new
        /// predecessor's successor to reflect the change. To clear
        /// the value, set it to null.
        /// </summary>
        public NodeLiniar<T> Prev
        {
            get
            {
                return prev;
            }
            set
            {
                prev = value;
                if (prev != null)
                prev.next = this;
            }          
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Node Implementation...

        /// <summary>
        /// Returns the next node as this node's only child if it is not
        /// null. Otherwise, it returns an empty list.
        /// </summary>
        /// <returns>The child of the node</returns>
        public IEnumerable<Node<T>> ListChildren()
        {
            if (next == null) yield break;
            else yield return next;
        }

        /// <summary>
        /// Removes all linked references from this node in order to avoid
        /// memory leaks. This should only be called when the node structure
        /// is being broken down.
        /// </summary>
        public void Dispose() 
        {
            next = null;
            prev = null;
        }

        #endregion ///////////////////////////////////////////////////////////////
    }
}
