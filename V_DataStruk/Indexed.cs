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
    /// Represents an indexed collection of items, with methods to retrieve and delete
    /// items by index, as well as find the index of a given item. It differs significantly
    /// from the IList interface, in that it dose not allow for the order of the items
    /// to be changed once inserted into the list. It also allows for the use of the
    /// Index class, as opposed to ordinary integers. 
    /// </summary>
    /// <typeparam name="E">The element type of the indexed collection</typeparam>
    /// <remarks>Last Update: 2016-06-09</remarks>
    public interface Indexed<E> : ICollection<E>
    {
        /// <summary>
        /// Accesses the items in the collection by index.
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <returns>The desired item</returns>
        E this[Index index] { get; }

        /// <summary>
        /// Finds the index of the requested item within the collection. If the
        /// collection contains duplicate items, it returns the index of the first 
        /// item found.
        /// </summary>
        /// <param name="item">The item to be found</param>
        /// <returns>The index of the target item, or null if the item is not 
        /// contained within the list</returns>
        int? IndexOf(E item);

        /// <summary>
        /// Retrieves the item from the collection at the given index. Negative 
        /// indices indicate positions from the end of the collection. If the index 
        /// is invalid, then null is returned.
        /// </summary>
        /// <param name="index">Index of the item to be retrieved</param>
        /// <returns>The requested item, or null if the index was invalid</returns>
        E GetItem(Index index);

        /// <summary>
        /// Removes an item from the collection. Negative indices indicate positions 
        /// from the end of the collection. If the index is invalid, no items are 
        /// removed and null is returned.
        /// </summary>
        /// <param name="index">Index of the item to be removed</param>
        /// <returns>The removed item, or null if the index was invalid</returns>
        E RemoveAt(Index index);
    }
}
