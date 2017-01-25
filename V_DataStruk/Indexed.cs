/**
 *  This file is an integral part of the Vulpine Core Library: 
 *  Copyright (c) 2016-2017 Benjamin Jacob Dawson. 
 *
 *      https://www.jacobs-den.org/projects/core-library/
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
