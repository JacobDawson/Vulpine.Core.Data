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
    /// This class provides a read-only wrapper to all classes that implement the
    /// Node interface. This allows for external code to inspect the structure
    /// of Trees and Graphs and the like, while maintaining the integrity of there
    /// internal structure. It also employs lazy evaluation, creating new wrappers
    /// for Nodes only when required. This allows it to be more efficient than
    /// copying the entire structure verbatim.
    /// </summary>
    /// <typeparam name="T">The data type of the node</typeparam>
    /// <remarks>Last Update: 2016-05-24</remarks>
    public sealed class NodeSafe<T> : Node<T>
    {
        #region Class Definitions...

        //stores a reference to the interior node
        private Node<T> inner;

        /// <summary>
        /// Creates a read-only node by linking to another node, thus
        /// creating a safe reference to that node.
        /// </summary>
        /// <param name="inner">The node to reference</param>
        public NodeSafe(Node<T> inner)
        {
            this.inner = inner;
        }

        /// <summary>
        /// Generates a string representation of the node, repeating the
        /// contents of the node in string format.
        /// </summary>
        /// <returns>The node in string format</returns>
        public override string ToString()
        {
            //if we've been disposed, give an error message
            if (inner == null) return "Deleted Node";

            //calls upon the inner node
            return inner.ToString();
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Node Linkings...

        /// <summary>
        /// The data contained within the node. It is denied write
        /// access in order to protect the original node. 
        /// </summary>
        public T Data
        {
            get 
            {
                if (inner == null) return default(T);
                else return inner.Data; 
            }
            set
            {
                //we aren't allowed to change the internal structure
                throw new InvalidOperationException();
            }
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Node Implementation...

        /// <summary>
        /// Lists the collective children of the node. Any given node may 
        /// have zero or more children, but null children are not included.
        /// </summary>
        /// <returns>The children of the node</returns>
        public IEnumerable<Node<T>> ListChildren()
        {
            //if we have been disposed, there is nothing to return
            if (inner == null) yield break;

            //wraps each child in another safe node, to protect it
            foreach (Node<T> child in inner.ListChildren())
                yield return new NodeSafe<T>(child);
        }

        /// <summary>
        /// Preforms a shallow clearing of the read-only node. The interior
        /// node cannot be disposed because it may still be in use else-where.
        /// </summary>
        public void Dispose()
        {
            //here we only do a shallow disposal
            inner = null;
        }

        #endregion ///////////////////////////////////////////////////////////////
    }
}
