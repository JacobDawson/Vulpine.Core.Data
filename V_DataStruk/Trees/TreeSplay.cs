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

namespace Vulpine.Core.Data.Trees
{
    /// <summary>
    /// A Splay Tree is a unique binary search tree that forgoes the desire to keep
    /// the tree balanced. Instead it moves the last accessed item up to the root of
    /// the tree after every operation. This helps exploit locality of reference by
    /// ensuring that the most frequently accessed nodes are all found near the 
    /// root of the tree. Therefore this implementation will be most useful when few
    /// items in the tree are accessed frequently and the rest are accessed infrequently.
    /// </summary>
    /// <typeparam name="E">The element type of the tree</typeparam>
    /// <remarks>Last Update: 2016-06-12</remarks>
    public sealed class TreeSplay<E> : Tree<E>
    where E : IComparable<E>
    {
        #region Class Definitions...

        /// <summary>
        /// Creates an empty tree, specifying an optional comparison function,
        /// used for sorting the items that are inserted into the tree.
        /// </summary>
        /// <param name="comp">Comparison operator to be used</param>
        public TreeSplay(Comparison<E> comp = null)
        {
            //initializes the tree
            comparer = comp;
            Clear();
        }

        /// <summary>
        /// Creates a new tree, containing multiple items.
        /// </summary>
        /// <param name="items">The items of the tree</param>
        public TreeSplay(params E[] items)
        {
            //initializes the tree
            comparer = null;
            Clear();

            //inserts the items one at a time
            foreach (var item in items) Add(item);
        }

        /// <summary>
        /// Creates a new tree, containing multiple items.
        /// </summary>
        /// <param name="items">The items of the tree</param>
        public TreeSplay(IEnumerable<E> items)
        {
            //initializes the tree
            comparer = null;
            Clear();

            //inserts the items one at a time
            foreach (var item in items) Add(item);
        }

        #endregion //////////////////////////////////////////////////////////

        #region Tree Operations...

        /// <summary>
        /// Inserts an item into the tree.
        /// </summary>
        /// <param name="item">The item to be inserted</param>
        /// <exception cref="ArgumentNullException">If null is inserted</exception>
        public override void Add(E item)
        {
            if (item == null) throw new ArgumentNullException("item");

            if (root == null)
            {
                //item inserted as the new root
                root = new NodeBinary<E>(item);
                size = depth = 1; return;
            }

            Splay(item, true);

            //used in inserting an item to the tree
            NodeBinary<E> node = new NodeBinary<E>(item);
            int comp = Compare(item, root.Data);

            if (comp <= 0)
            {
                //sets the root as the right child
                node.Left = root.Left;
                node.Right = root;
                root.Left = null;
            }        
            else
            {
                //sets the root as the left child
                node.Right = root.Right;
                node.Left = root;
                root.Right = null;
            }

            //updates size and sets the root
            size = size + 1;
            root = node;
        }

        /// <summary>
        /// Removes a given item from the tree. If the tree contains duplicate 
        /// values, the first instance found is removed. It returns true if the 
        /// item was successfully removed, and false if otherwise.
        /// </summary>
        /// <param name="item">Item to be removed</param>
        /// <returns>True if the item was successfully removed</returns>
        public override bool Remove(E targ)
        {
            //checks for an empty tree
            if (root == null) return false;

            Splay(targ, false);
            NodeBinary<E> temp = root;

            //checks to see if the item was actually found
            if (Compare(targ, root.Data) != 0) return false;

            if (root.Right == null)
            {
                //simple case: delete the root
                root = root.Left;
                if (root != null) 
                root.Parent = null;
            }
            else if (root.Left == null)
            {
                //simple case: delete the root
                root = root.Right;
                if (root != null) 
                root.Parent = null;
            }
            else
            {
                //we must use the in-order successor
                NodeBinary<E> node = root.Left;

                //safely deleats the root and updates the tree
                root = root.Right;
                root.Parent = null;
                SplayMinMax(false);

                //reattaches the old root's sub-tree
                root.Left = node;
            }

            //updates size and disposes of the old root
            size = size - 1;
            temp.Dispose();

            return true;
        }

        /// <summary>
        /// Determines if the tree contains an item matching the given target.
        /// </summary>
        /// <param name="targ">Target to match</param>
        /// <returns>True if a similar item is contained in the tree,
        /// false if otherwise</returns>
        public override bool Contains(E targ)
        {
            //if the tree is empty, it can't contain anything
            if (root == null) return false;

            Splay(targ, false);

            //indicates if the item was successfully found
            return targ.CompareTo(root.Data) == 0;
        }

        /// <summary>
        /// Retrieves an item from the tree, matching the given target. 
        /// It returns null if no match can be found.
        /// </summary>
        /// <param name="target">Target to match</param>
        /// <returns>The matching item, or null if not found</returns>
        public override E Retreve(E targ)
        {
            //checks for an empty tree
            if (root == null) return default(E);

            Splay(targ, false);

            //checks for items that are not found
            int comp = Compare(targ, root.Data);
            return (comp == 0) ? root.Data : default(E);
        }

        /// <summary>
        /// Retrieves either the minimum or the maximum valued item in the
        /// tree, based on the item's sorted order. It returns null if the
        /// tree is currently empty.
        /// </summary>
        /// <param name="max">Set true for the maximum value, or false for
        /// the minimum value</param>
        /// <returns>The maximum or minimum value, or null if empty</returns>
        public override E GetMinMax(bool max)
        {
            if (root == null) return default(E);
            SplayMinMax(max);
            return root.Data;
        }

        /// <summary>
        /// Same as the GetMinMax() method, except that it also removes the
        /// item from the tree after obtaining it.
        /// </summary>
        /// <param name="max">Set true for the maximum value, or false for
        /// the minimum value</param>
        /// <returns>The maximum or minimum value, or null if empty</returns>
        public override E RemoveMinMax(bool max)
        {
            //checks for an empty tree
            if (root == null) return default(E);

            SplayMinMax(max);
            NodeBinary<E> temp = root;

            //simple case delete because of minmax
            if (root.Right == null) root = root.Left;
            else if (root.Left == null) root = root.Right;

            //fixes the root's parent reference
            if (root != null) root.Parent = null;

            //disposes of the old root
            size = size - 1;
            temp.Dispose();

            return temp.Data;
        }

        #endregion //////////////////////////////////////////////////////////

        #region Splaying Operations...

        /// <summary>
        /// Helper method, splays the node containing the target up to the
        /// root of the tree. If the target is not found, the last node
        /// reached is used instead.
        /// </summary>
        /// <param name="target">Item to be splayed</param>
        /// <param name="insert">Set true if target is to be inserted,
        /// and false otherwise</param>
        private void Splay(E target, bool insert)
        {
            //can't splay an empty tree
            if (root == null) return;
            depth = -1;

            NodeBinary<E> node = root;
            NodeBinary<E> parrent = null;
            int comp = 1;

            while (node != null && comp != 0)
            {
                //re-preforms the comparison
                comp = Compare(target, node.Data);
                parrent = node;

                //must keep searching if we are doing an insert
                if (insert && comp == 0) comp = -1;

                //determines which way to travel down the tree
                if (comp < 0) node = node.Left;
                if (comp > 0) node = node.Right;
            }

            while (parrent != root)
            {
                //splays the node or its parent to the root
                if (Zig(parrent)) continue;
                if (ZigZig(parrent)) continue;
                if (ZigZag(parrent)) continue;
            }

            //makes sure that nothing is above the root
            if (root != null) root.Parent = null;
        }

        /// <summary>
        /// Helper method, splays the node containing either the maximum
        /// or minimum value up to the root of the tree.
        /// </summary>
        /// <param name="max">True to splay the maximum value, false 
        /// for the minimum value</param>
        private void SplayMinMax(bool max)
        {
            //can't splay an empty tree
            if (root == null) return;

            NodeBinary<E> target = root;
            depth = -1;

            //obtains the minmax value in the tree
            while (target.GetChild(max) != null)
                target = target.GetChild(max);

            while (target != root)
            {
                //splays the min or max to the root
                if (Zig(target)) continue;
                if (ZigZig(target)) continue;
                if (ZigZag(target)) continue;
            }

            //makes sure that nothing is above the root
            if (root != null) root.Parent = null;
        }

        /// <summary>
        /// Preforms a Zig operation by moving the target node to the
        /// root in a single rotation. This step is only preformed if
        /// the parent is the root.
        /// </summary>
        /// <param name="node">The target node</param>
        /// <returns>True if the operation succeeds</returns>
        private bool Zig(NodeBinary<E> node)
        {
            //obtains the parent of the node
            NodeBinary<E> parr = node.Parent;

            //only zig if the parent is the root
            if (parr != root) return false;

            //rotates the parent to pull the node up
            if (node == parr.Left) RotateRight(parr);
            if (node == parr.Right) RotateLeft(parr);

            return true;
        }

        /// <summary>
        /// Preforms a Zig-Zig operation in which the grandparent is
        /// rotated first. This is only preformed when the node and the
        /// parent are on the same side.
        /// </summary>
        /// <param name="node">The target node</param>
        /// <returns>True if the operation succeeds</returns>
        private bool ZigZig(NodeBinary<E> node)
        {
            //obtains the parent and grandparent
            NodeBinary<E> parr = node.Parent;
            NodeBinary<E> grad = parr.Parent;

            //inverts the latter from node to grandparent
            if (node == parr.Left && parr == grad.Left)
            {
                RotateRight(grad);
                RotateRight(parr);
                return true;
            }

            //inverts the latter from node to grandparent
            if (node == parr.Right && parr == grad.Right)
            {
                RotateLeft(grad);
                RotateLeft(parr);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Preforms a Zig-Zag operation in which the parent is rotated
        /// first. This is only preformed when the node and the parent
        /// are on the opposite sides.
        /// </summary>
        /// <param name="node">The target node</param>
        /// <returns>True if the operation succeeds</returns>
        private bool ZigZag(NodeBinary<E> node)
        {
            //obtains the parent and grandparent
            NodeBinary<E> parr = node.Parent;
            NodeBinary<E> grad = parr.Parent;

            //must zigzag if node and parent are opposite children
            if (node == parr.Right && parr == grad.Left)
            {
                RotateLeft(parr);
                RotateRight(grad);
                return true;
            }

            //must zigzag if node and parent are opposite children
            if (node == parr.Left && parr == grad.Right)
            {
                RotateRight(parr);
                RotateLeft(grad);
                return true;
            }

            return false;
        }

        #endregion //////////////////////////////////////////////////////////
    }
}
