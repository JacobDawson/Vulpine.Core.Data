using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.Trees
{
    /// <summary>
    /// This enumeration lists all the diffrent possable ways of traversing a binary
    /// search tree using depth first search. Because each node in a binary tree
    /// can have two diffrent children, this gives two possable paths to follow at
    /// each junction. Because of this it is possable to traverse a tree in a
    /// number of diffrent ways.
    /// <remarks>Last Update: 2016-06-04</remarks>
    /// </summary>
    public enum TreeOrder
    {
        /// <summary>
        /// At each branch in the tree, the parent is visited first before
        /// either of its children. Thus items on the left side wind up
        /// apearing before items on the right.
        /// </summary>
        PreOrder,

        /// <summary>
        /// At each branch in the tree, the parent is visited after the left
        /// child, but before the right child. When applied to a binary search
        /// tree, this makes the items apear in sorted order.
        /// </summary>
        InOrder,

        /// <summary>
        /// At each branch in the tree, the parent is visited last after
        /// borth of its children. Thus items on the right side wind up
        /// apearing before items on the left.
        /// </summary>
        PostOrder,
    }
}
