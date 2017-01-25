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
    /// A Basic Tree provides the most basic, yet complete, implementation of a
    /// binary search tree. By default, it dose not re-balance the tree after insertion 
    /// or removal. As such, the success of the tree is highly dependent on insertion 
    /// order. In the worst case, it dose not preform any better than a linked list.
    /// However, it is possible for derived classes to balance the tree by overwriting
    /// the virtual methods CreateNode(), RebalInsert(), and RebalDelete(). You can
    /// gage how well-balanced a tree is by checking its depth.
    /// </summary>
    /// <typeparam name="E">The element type of the tree</typeparam>
    /// <remarks>Last Update: 2016-06-04</remarks>
    public class TreeBasic<E> : Tree<E>
        where E : IComparable<E>
    {
        #region Class Definitions...

        /// <summary>
        /// Creates an empty tree, specifying an optional comparison function,
        /// used for sorting the items that are inserted into the tree.
        /// </summary>
        /// <param name="comp">Comparison operator to be used</param>
        public TreeBasic(Comparison<E> comp = null)
        {
            //initializes the tree
            comparer = comp;
            Clear();
        }

        /// <summary>
        /// Creates a new tree, containing multiple items.
        /// </summary>
        /// <param name="items">The items of the tree</param>
        public TreeBasic(params E[] items)
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
        public TreeBasic(IEnumerable<E> items)
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
                root = CreateNode(item);
                size = depth = 1; return;
            }

            NodeBinary<E> node = root;
            NodeBinary<E> parrent = null;
            int comp = 1;

            while (node != null)
            {
                //preforms the comparison
                comp = Compare(item, node.Data);
                parrent = node;

                //determines which way to travel down the tree
                node = (comp < 0) ? node.Left : node.Right;
            }

            //insert the new item after the leaf
            node = CreateNode(item);
            if (parrent == null) root = node;
            else if (comp < 0) parrent.Left = node;
            else parrent.Right = node;

            //preforms any re-balancing, if applicable
            RebalInsert(node);

            //updates size and resets height
            size = size + 1;
            depth = -1;
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
            //attempts to locate the node in the tree
            NodeBinary<E> node = FindNode(targ);

            //checks for items that are not found
            if (node == null) return false;

            //reduces the task to removing the in-order successor
            node = ReplaceSucessor(node);
            NodeBinary<E> parent = node.Parent;
            NodeBinary<E> child = node.Right;

            //obtains the singular child of the node
            if (child == null) child = node.Left;
            if (child != null) child.Parent = null;

            //preforms any re-balancing, if applicable
            RebalDelete(node, child);

            //deletes the node accordingly
            if (parent == null) root = child;
            else if (parent.Right == node) parent.Right = child;
            else if (parent.Left == node) parent.Left = child;

            //updates size and resets height
            size = size - 1;
            depth = -1;

            node.Dispose();
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
            //attempts to locate the node in the tree
            NodeBinary<E> node = FindNode(targ);

            //indicates if the item was successfully found
            return (node != null);
        }

        /// <summary>
        /// Retrieves an item from the tree, matching the given target. 
        /// It returns null if no match can be found.
        /// </summary>
        /// <param name="target">Target to match</param>
        /// <returns>The matching item, or null if not found</returns>
        public override E Retreve(E targ)
        {
            //attempts to locate the node in the tree
            NodeBinary<E> node = FindNode(targ);
            if (node == null) return default(E);

            //returns the desired item
            return node.Data;
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
            //checks for an empty tree
            if (root == null) return default(E);

            NodeBinary<E> node = root;

            while (node.GetChild(max) != null)
            {
                //finds the right-(left)-most item
                node = node.GetChild(max);
            }

            return node.Data;
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

            NodeBinary<E> node = root;
            NodeBinary<E> parent = null;

            while (node.GetChild(max) != null)
            {
                //finds the right-(left)-most item
                parent = node;
                node = node.GetChild(max);
            }

            //obtains the singular child of the node
            NodeBinary<E> child = node.GetChild(!max);
            if (child != null) child.Parent = null;

            //preforms any re balancing, if applicable
            RebalDelete(node, child);

            //deletes the node accordingly
            if (parent == null) root = child;
            else if (parent.Right == node) parent.Right = child;
            else if (parent.Left == node) parent.Left = child;

            node.Parent = null;
            size = size - 1;
            depth = -1;

            //disposes of the node and returns its data
            node.Dispose();
            return node.Data;
        }

        #endregion //////////////////////////////////////////////////////////

        #region Balancing Opperations...

        /// <summary>
        /// Creates a new node containing the designated item. By default,
        /// it uses ordinary, binary nodes. However, sub-classes can override
        /// it to create special nodes that store additional data.
        /// </summary>
        /// <param name="item">Item inside the node</param>
        /// <returns>A new tree node</returns>
        protected virtual NodeBinary<E> CreateNode(E item)
        {
            //uses a plain, ordinary, binary node
            return new NodeBinary<E>(item);
        }

        /// <summary>
        /// Attempts to re-balance the tree after a node has been inserted.
        /// By default it dose nothing, but sub-classes can override it.
        /// </summary>
        /// <param name="node">Node that was inserted</param>
        protected virtual void RebalInsert(NodeBinary<E> node) {}

        /// <summary>
        /// Attempts to re-balance the tree before a node has been removed.
        /// By default it dose nothing, but sub-classes can override it.
        /// </summary>
        /// <param name="node">Node to be deleted</param>
        /// <param name="child">Child of node to be deleted</param>
        protected virtual void RebalDelete
            (NodeBinary<E> node, NodeBinary<E> child) {}

        #endregion //////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method, locates a node who's item matches the target. 
        /// If no such node is found, it returns null.
        /// </summary>
        /// <param name="target">Target item</param>
        /// <returns>The node containing the target</returns>
        protected NodeBinary<E> FindNode(E target)
        {
            //used in searching for the node
            NodeBinary<E> node = root;
            int comp = 1;

            while (node != null && comp != 0)
            {
                //preforms the comparison
                comp = Compare(target, node.Data);

                //determines which way to travel down the tree
                if (comp < 0) node = node.Left;
                if (comp > 0) node = node.Right;
            }

            //returns the node if found
            return node;
        }

        /// <summary>
        /// Helper method, replaces the given node with it's in-order
        /// successor or predecessor. Note that it is possible for 
        /// the node to be it's own successor.
        /// </summary>
        /// <param name="node">Node to be replaced</param>
        /// <returns>The successor of the node</returns>
        protected NodeBinary<E> ReplaceSucessor(NodeBinary<E> node)
        {
            //determines if the node is its own successor
            if (node.Left == null || node.Right == null) return node;

            //used to find the in-order successor
            NodeBinary<E> child = node.Right;

            //finds the in-order successor
            while (child.Left != null) 
                child = child.Left;

            //copies the containing item
            node.Data = child.Data;
            return child;
        }

        #endregion //////////////////////////////////////////////////////////
    }
}
