using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//spell-checked

using Vulpine.Core.Data.Queues;

namespace Vulpine.Core.Data.Trees
{
    /// <summary>
    /// A Tree, or more specifically a binary search tree, is a tree-like data structure
    /// where each node can have up to two children, labeled the right and left child
    /// respectfully. In addition, the left child always comes before the node in
    /// sorted order, and the right child comes after. This way, it is possible to
    /// find any node in the tree by searching either to the left or right from the 
    /// current node. When the tree is balanced, this equates to the binary search 
    /// algorithm. The minimum and maximum values in the tree can also be found in a 
    /// similar manner, by always following the left or right links.
    /// </summary>
    /// <typeparam name="E">The element type of the tree</typeparam>
    /// <remarks>Last Update: 2016-06-04</remarks>
    public abstract class Tree<E> : VCollection<E>
        where E : IComparable<E>
    {
        #region Class Definitions...

        //keeps track of both the size and depth
        protected int size;
        protected int depth;

        //stores the root of the tree
        protected NodeBinary<E> root;

        //stores a reference to the method of comparison
        protected Comparison<E> comparer;

        /// <summary>
        /// Generates a string representation of the tree, displaying
        /// the size and depth of the tree, as well as the item located
        /// at its root.
        /// </summary>
        /// <returns>The tree as a string</returns>
        public override string ToString()
        {
            if (root == null) return "Empty Tree";

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Tree[{0}, {1}]", Count, Depth);
            sb.Append(" rooted at ");
            sb.AppendFormat("<{0}>", root.Data);

            return sb.ToString();        
        }

        #endregion //////////////////////////////////////////////////////////

        #region Class Propertys...

        /// <summary>
        /// Determines if the tree is empty or contains items. It is
        /// set to true if empty and false if otherwise.
        /// </summary>
        public override bool Empty
        {
            get { return (root == null); }
        }

        /// <summary>
        /// Represents the number of items in the tree.
        /// </summary>
        public override int Count
        {
            get { return size; }
        }

        /// <summary>
        /// Represents the maximum depth of the tree, or the length of the
        /// longest path from the root to any given leaf. This serves as
        /// an indication of the tree's balance.
        /// </summary>
        public int Depth
        {
            get 
            {
                //recalculates the depth, only when necessary
                if (depth < 0) depth = CalcDepth(root);

                return depth;
            }
        }

        #endregion //////////////////////////////////////////////////////////

        #region Tree Opperations...

        /// <summary>
        /// Removes the minimum valued item from the tree, treating it as if
        /// it were a min-heap. It returns null if the tree is empty.
        /// </summary>
        /// <returns>The minimum value, or null if empty</returns>
        public override E Dequeue()
        {
            //treats the tree like a min-heap
            return RemoveMinMax(false);
        }

        /// <summary>
        /// Exposes the tree's internal structure in a safe way, by returning
        /// a read-only reference to the root node. This way, the structure of
        /// the tree can be explored without compromising its integrity.
        /// </summary>
        /// <returns>The root of the tree</returns>
        public Node<E> GetRoot()
        {
            //no need to bother wrapping the root if it's null
            if (root == null) return null;

            //generates a safe copy of the root
            return new NodeSafe<E>(root);
        }

        /// <summary>
        /// Removes all items from the tree and deconstruction the tree's 
        /// internal structure, thus preventing any memory leaks.
        /// </summary>
        public override void Clear()
        {
            //deletes the tree, starting from the root
            DeleteSubtree(root);

            //sets the initial conditions
            size = depth = 0;
            root = null;          
        }

        #endregion //////////////////////////////////////////////////////////

        #region Abstract Methods...

        /// <summary>
        /// Retrieves an item from the tree, matching the given target. 
        /// It returns null if no match can be found.
        /// </summary>
        /// <param name="target">Target to match</param>
        /// <returns>The matching item, or null if not found</returns>
        public abstract E Retreve(E target);

        /// <summary>
        /// Retrieves either the minimum or the maximum valued item in the
        /// tree, based on the item's sorted order. It returns null if the
        /// tree is currently empty.
        /// </summary>
        /// <param name="max">Set true for the maximum value, or false for
        /// the minimum value</param>
        /// <returns>The maximum or minimum value, or null if empty</returns>
        public abstract E GetMinMax(bool max);

        /// <summary>
        /// Same as the GetMinMax() method, except that it also removes the
        /// item from the tree after obtaining it.
        /// </summary>
        /// <param name="max">Set true for the maximum value, or false for
        /// the minimum value</param>
        /// <returns>The maximum or minimum value, or null if empty</returns>
        public abstract E RemoveMinMax(bool max);

        #endregion //////////////////////////////////////////////////////////

        #region Itteration Methods...

        /// <summary>
        /// Enumerates all the items in the tree, using the default
        /// in-order tree traversal. This ensures that the items are
        /// listed in sorted order.
        /// </summary>
        /// <returns>An enumerator over the tree</returns>
        public override IEnumerator<E> GetEnumerator()
        {
            //by default, we enumerate the tree in-order
            return ListIn(TreeOrder.InOrder).GetEnumerator();
        }

        /// <summary>
        /// Lists all the items in the tree in a depth-first manner,
        /// utilizing the desired tree traversal.
        /// </summary>
        /// <param name="ord">Desired tree traversal</param>
        /// <returns>The items in the tree</returns>
        public IEnumerable<E> ListIn(TreeOrder ord)
        {
            //uses a previous reference to search
            NodeBinary<E> curr = root;
            NodeBinary<E> prev = null;

            while (curr != null)
            {
                //searches down the left side of the tree
                while (prev == curr.Parent)
                {
                    if (ord == TreeOrder.PreOrder)
                        yield return curr.Data;

                    if (curr.Left == null)
                    { prev = null; break; }

                    prev = curr;
                    curr = curr.Left;
                }

                //searches across the tree to the right
                while (prev == curr.Left)
                {
                    if (ord == TreeOrder.InOrder)
                        yield return curr.Data;

                    if (curr.Right == null)
                    { prev = null; break; }

                    prev = curr;
                    curr = curr.Right;
                }

                //searches back up the right side
                if (prev == curr.Right)
                {
                    if (ord == TreeOrder.PostOrder)
                        yield return curr.Data;

                    prev = curr;
                    curr = curr.Parent;
                }
            }
        }

        /// <summary>
        /// Lists all the items in the tree in a breadth-first manor,
        /// utilizing the level order tree traversal.
        /// </summary>
        /// <returns>The items in the tree</returns>
        public IEnumerable<E> ListInLevelOrder()
        {
            //if the root is null, there is nothing to iterate
            if (root == null) yield break;

            //creates a queue to aid in the iterator
            int cap = (size / 2) + 1;
            var queue = new DequeArray<NodeBinary<E>>(cap);

            //pushes the root to start the process
            queue.PushBack(root);

            using (queue)
            {
                while (!queue.Empty)
                {
                    //visits the next node in the queue
                    NodeBinary<E> temp = queue.PopFront();
                    yield return temp.Data;

                    //adds the children of the node back into the queue
                    if (temp.Left != null) queue.PushBack(temp.Left);
                    if (temp.Right != null) queue.PushBack(temp.Right);
                }
            }
        }

        #endregion //////////////////////////////////////////////////////////

        #region Helper Methods...

        /// <summary>
        /// Helper method, preforms a left or counter-clockwise 
        /// rotation on the tree about a central pivot node.
        /// </summary>
        /// <param name="head">The pivot node</param>
        protected void RotateLeft(NodeBinary<E> head)
        {
            //obtains the node to be adopted
            NodeBinary<E> right = head.Right;
            NodeBinary<E> adopt = right.Left;
            NodeBinary<E> parr = head.Parent;

            //preforms the rotation
            right.Parent = null;
            right.Left = head;
            head.Right = adopt;

            //places the node at the rotation position
            if (parr == null) root = right;
            else if (parr.Left == head) parr.Left = right;
            else if (parr.Right == head) parr.Right = right;
        }

        /// <summary>
        /// Helper method, preforms a right or clockwise rotation
        /// on the tree about a central pivot node.
        /// </summary>
        /// <param name="head">The pivot node</param>
        protected void RotateRight(NodeBinary<E> head)
        {
            //obtains the node to be adopted
            NodeBinary<E> left = head.Left;
            NodeBinary<E> adopt = left.Right;
            NodeBinary<E> parr = head.Parent;

            //preforms the rotation
            left.Parent = null;
            left.Right = head;
            head.Left = adopt;

            //places the node at the rotation position
            if (parr == null) root = left;
            else if (parr.Left == head) parr.Left = left;
            else if (parr.Right == head) parr.Right = left;
        }

        /// <summary>
        /// Helper method, compares one item to another, using the specialized 
        /// comparison method if provided, otherwise it uses the default method 
        /// defined on the content type.
        /// </summary>
        /// <param name="item1">First item to be compared</param>
        /// <param name="item2">Second item to be compared</param>
        /// <returns>A negative number if item1 comes first, a positive number
        /// if it comes second, and zero if they are equal</returns>
        protected int Compare(E item1, E item2)
        {
            //uses the default comparison function if necessary
            if (comparer == null) return item1.CompareTo(item2);

            //uses the custom comparison function, then the default
            int comp = comparer.Invoke(item1, item2);
            if (comp == 0) comp = item1.CompareTo(item2);

            return comp;
        }

        #endregion //////////////////////////////////////////////////////////

        #region Recursive Methods...

        /// <summary>
        /// A recursive method which deletes an entire sub-tree by disposing
        /// of the root node and recursively deleting all of its children.
        /// This way, no cyclical references are left creating memory leaks.
        /// </summary>
        /// <param name="node">Root of the sub-tree</param>
        protected void DeleteSubtree(NodeBinary<E> node)
        {
            //reached a leaf of the tree
            if (node == null) return;

            //recurses down the tree
            DeleteSubtree(node.Left);
            DeleteSubtree(node.Right);

            //disposes of the node
            node.Dispose();
        }

        /// <summary>
        /// A recursive method used to calculate the maximum depth of
        /// a given sub-tree, indicated by its root node.
        /// </summary>
        /// <param name="node">Root of the sub-tree</param>
        /// <returns>The maximum depth of the sub-tree</returns>
        protected int CalcDepth(NodeBinary<E> node)
        {
            //reached a leaf of the tree
            if (node == null) return 0;

            //recurses down the tree
            int left = CalcDepth(node.Left);
            int right = CalcDepth(node.Right);
            return 1 + Math.Max(left, right);
        }

        #endregion //////////////////////////////////////////////////////////
    }
}
