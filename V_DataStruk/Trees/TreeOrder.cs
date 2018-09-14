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

namespace Vulpine.Core.Data.Trees
{
    /// <summary>
    /// This enumeration lists all the different possible ways of traversing a binary
    /// search tree using depth first search. Because each node in a binary tree
    /// can have two different children, this gives two possible paths to follow at
    /// each junction. Because of this it is possible to traverse a tree in a
    /// number of different ways.
    /// <remarks>Last Update: 2016-06-04</remarks>
    /// </summary>
    public enum TreeOrder
    {
        /// <summary>
        /// At each branch in the tree, the parent is visited first before
        /// either of its children. Thus items on the left side wind up
        /// appearing before items on the right.
        /// </summary>
        PreOrder,

        /// <summary>
        /// At each branch in the tree, the parent is visited after the left
        /// child, but before the right child. When applied to a binary search
        /// tree, this makes the items appear in sorted order.
        /// </summary>
        InOrder,

        /// <summary>
        /// At each branch in the tree, the parent is visited last after
        /// both of its children. Thus items on the right side wind up
        /// appearing before items on the left.
        /// </summary>
        PostOrder,
    }
}
