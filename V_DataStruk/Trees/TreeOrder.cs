/**
 *  This file is an integral part of the Vulpine Core Library: 
 *  Copyright (c) 2016-2017 Benjamin Jacob Dawson. 
 *
 *      http://www.jakesden.com/corelibrary.html
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
