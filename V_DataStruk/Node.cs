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
    /// In simplest terms, a Node is a vertex of a directed graph that contains
    /// some data. Looking at it another way, Nodes connect isolated pieces of
    /// data to one another, allowing complex data structures to be built. Each
    /// node will contain a single piece of data and zero, one, or more references
    /// to other nodes. The nodes referenced by a given node are referred to as that
    /// node's children. Nodes are also made disposable, to clear up any cyclical
    /// connections that may lead to memory leaks.
    /// </summary>
    /// <typeparam name="E">The data type of the node</typeparam>
    /// <remarks>Last Update: 2016-05-24</remarks>
    public interface Node<T> : IDisposable
    {
        /// <summary>
        /// The data contained within the node. Read-Only
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// Lists the collective children of the node. Any given node may 
        /// have zero or more children, but null children are not included.
        /// </summary>
        /// <returns>The children of the node</returns>
        IEnumerable<Node<T>> ListChildren();
    }
}
