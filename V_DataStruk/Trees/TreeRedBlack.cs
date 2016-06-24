using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//spell-checked

namespace Vulpine.Core.Data.Trees
{
    /// <summary>
    /// A Red-Black Tree extends the notion of a Basic Tree by providing self-balancing
    /// operations for insert and delete. It dose this by including a flag on each
    /// node, indicating if that node is colored red or black. Using this information,
    /// it is able to guarantee that any path from the root to a leaf is never more that
    /// twice as long as any such path. This dose not result in a perfect balancing
    /// of the tree, but insertion and removal are very efficient, and it offers
    /// amortized O(log n) look-up time.
    /// </summary>
    /// <typeparam name="E">The element type of the tree</typeparam>
    /// <remarks>Last Update: 2016-06-12</remarks>
    public sealed class TreeRedBlack<E> : TreeBasic<E>
    where E : IComparable<E>
    {
        #region Class Definitions...

        /// <summary>
        /// Creates an empty tree, specifying an optional comparison function,
        /// used for sorting the items that are inserted into the tree.
        /// </summary>
        /// <param name="comp">Comparison operator to be used</param>
        public TreeRedBlack(Comparison<E> comp = null) : base(comp) { }

        /// <summary>
        /// Creates a new tree, containing multiple items.
        /// </summary>
        /// <param name="items">The items of the tree</param>
        public TreeRedBlack(params E[] items) : base(items) { }

        /// <summary>
        /// Creates a new tree, containing multiple items.
        /// </summary>
        /// <param name="items">The items of the tree</param>
        public TreeRedBlack(IEnumerable<E> items) : base(items) { }

        #endregion //////////////////////////////////////////////////////////

        #region Balancing Opperations...

        /// <summary>
        /// Creates a new red-black node containing the designated item.
        /// All nodes start out being colored black.
        /// </summary>
        /// <param name="item">Item inside the node</param>
        /// <returns>A new red-black node</returns>
        protected override NodeBinary<E> CreateNode(E item)
        {
            //generates a new red-black tree node
            return new NodeRedBlack<E>(item, false);
        }

        /// <summary>
        /// Re-balances the tree after a node has been inserted, and
        /// recolors all the nodes accordingly.
        /// </summary>
        /// <param name="node">Node that was inserted</param>
        protected override void RebalInsert(NodeBinary<E> node)
        {
            //colors the node red, and begins re-balancing
            SetRed(node, true);
            InsertCase1(node);
        }

        /// <summary>
        /// Attempts to re-balance the tree before a node has been removed,
        /// and recolors all the nodes accordingly.
        /// </summary>
        /// <param name="node">Node to be deleted</param>
        /// <param name="child">Child of node to be deleted</param>
        protected override void RebalDelete(NodeBinary<E> node, NodeBinary<E> child)
        {
            if (!IsRed(node))
            {
                //takes care of recoloring the tree
                if (child == null) DeleteCase1(node);
                else if (IsRed(child)) SetRed(child, false);
                else DeleteCase1(node); 
            }
        }

        #endregion //////////////////////////////////////////////////////////

        #region Insertion Cases...

        /// <summary>
        /// Takes care of the trivial insertion cases where either the
        /// node being inserted is the root, or it's parent is black.
        /// </summary>
        /// <param name="node">Node being processed</param>
        private void InsertCase1(NodeBinary<E> node)
        {
            if (node.Parent == null)
            {
                //color the root node black
                SetRed(node, false);
            }
            else if (IsRed(node.Parent))
            {
                //must continue if the parent is red
                InsertCase2(node);
            }
        }

        /// <summary>
        /// Takes care of the case where both the parent and the uncle
        /// are red, by panting them black and starting over with the
        /// grandparent recolored red.
        /// </summary>
        /// <param name="node">Node being processed</param>
        private void InsertCase2(NodeBinary<E> node)
        {
            //determines the parent and grandparent
            NodeBinary<E> parr = node.Parent;
            NodeBinary<E> grand = parr.Parent;
            NodeBinary<E> uncle = parr.GetSibling();

            //if the parent and uncle are both red, repaint them black
            //repaint the grandparent red and treat as a new insertion
            if (uncle != null && IsRed(uncle))
            {
                SetRed(parr, false);
                SetRed(uncle, false);
                SetRed(grand, true);
                InsertCase1(grand);
            }
            else
            {
                //we must go to insert case3
                InsertCase3(node);
            }
        }

        /// <summary>
        /// Takes care of the case where the parent is red but the uncle
        /// is black. The parent is rotated so that the target becomes
        /// the new parent.
        /// </summary>
        /// <param name="node">Node being processed</param>
        private void InsertCase3(NodeBinary<E> node)
        {
            //determines the parent and grandparent
            NodeBinary<E> parr = node.Parent;
            NodeBinary<E> grand = parr.Parent;

            if (node == parr.Right && parr == grand.Left)
            {
                RotateLeft(parr);
                node = node.Left;
            }
            else if (node == parr.Left && parr == grand.Right)
            {
                RotateRight(parr);
                node = node.Right;
            }

            InsertCase4(node);
        }

        /// <summary>
        /// After InsertCase3, this method causes the grandparent to be
        /// rotated so that both the parent and the target are pulled up.
        /// </summary>
        /// <param name="node">Node being processed</param>
        private void InsertCase4(NodeBinary<E> node)
        {
            //determines the parent and grandparent
            NodeBinary<E> parr = node.Parent;
            NodeBinary<E> grand = parr.Parent;

            //swaps the colors of parent and grandparent
            SetRed(parr, false);
            SetRed(grand, true);

            if (node == parr.Left && parr == grand.Left)
            {
                //rotates grandparent right
                RotateRight(grand);
            }
            else
            {
                //rotates grandparent left
                RotateLeft(grand);
            }
        }

        #endregion //////////////////////////////////////////////////////////

        #region Deletion Cases...

        /// <summary>
        /// Takes care of the trivial deletion cases where the root
        /// is being deleted. It also ensures the sibling is black
        /// by process of rotation.
        /// </summary>
        /// <param name="node">Node being processed</param>
        private void DeleteCase1(NodeBinary<E> node)
        {
            //no rotations necessary if new root
            if (node.Parent == null) return;

            NodeBinary<E> parent = node.Parent;            
            NodeBinary<E> sibling = node.GetSibling();

            if (IsRed(sibling))
            {
                //flips the parent and siblings colors
                SetRed(parent, true);
                SetRed(sibling, false);

                //sibling becomes the new grandparent
                if (node == parent.Left) RotateLeft(parent);
                else RotateRight(parent);
            }

            DeleteCase2(node);
        }

        /// <summary>
        /// Takes care of the case where both of the siblings children
        /// are black. It starts over with the parent if it happens to 
        /// be black, otherwise it is colored black to maintain order.
        /// </summary>
        /// <param name="node">Node being processed</param>
        private void DeleteCase2(NodeBinary<E> node)
        {
            NodeBinary<E> parent = node.Parent;
            NodeBinary<E> sibling = node.GetSibling();

            //determines if the children of S are black            
            bool test = IsRed(sibling.Right);
            test = test || IsRed(sibling.Left);

            if (!test && !IsRed(parent))
            {
                SetRed(sibling, true);
                DeleteCase1(parent);
            }
            else if (!test && IsRed(parent))
            {
                SetRed(sibling, true);
                SetRed(parent, false);
            }
            else
            {
                DeleteCase3(node);
            }
        }

        /// <summary>
        /// This case forces the siblings red child, if it has one, to
        /// be on the left of the left, or the right of the right of
        /// the parent, so that DeleteCase4 will work correctly.
        /// </summary>
        /// <param name="node">Node being processed</param>
        private void DeleteCase3(NodeBinary<E> node)
        {
            NodeBinary<E> parent = node.Parent;
            NodeBinary<E> sibling = node.GetSibling();

            //note that the sibling is always black due to case1
            //the sibling cannot have two black children due to case2

            if (node == parent.Left && !IsRed(sibling.Right))
            {
                SetRed(sibling, true);
                SetRed(sibling.Left, false);
                RotateRight(sibling);
            }
            else if (node == parent.Right && !IsRed(sibling.Left))
            {
                SetRed(sibling, true);
                SetRed(sibling.Right, false);
                RotateLeft(sibling);
            }

            DeleteCase4(node);
        }

        /// <summary>
        /// This case rotates the parent so that the siblings red child
        /// is pulled up. The siblings red child is set to black and the
        /// parent swaps colors with the sibling.
        /// </summary>
        /// <param name="node">Node being processed</param>
        private void DeleteCase4(NodeBinary<E> node)
        {
            NodeBinary<E> parent = node.Parent;
            NodeBinary<E> sibling = node.GetSibling();

            //sibling and parent exchange color
            SetRed(sibling, IsRed(parent));
            SetRed(parent, false);

            if (node == parent.Left)
            {
                SetRed(sibling.Right, false);
                RotateLeft(parent);
            }
            else
            {
                SetRed(sibling.Left, false);
                RotateRight(parent);
            }
        }

        #endregion //////////////////////////////////////////////////////////

        #region Red-Black Access...

        /// <summary>
        /// Helper method, used to determine if a given node is
        /// colored either red or black. Null nodes, are considered
        /// to be black by default.
        /// </summary>
        /// <param name="node">Node to check</param>
        /// <returns>True if the node is colored red, false if 
        /// it is colored black</returns>
        private static bool IsRed(NodeBinary<E> node)
        {
            var temp = node as NodeRedBlack<E>;
            return (temp != null) ? temp.IsRed : false;
        }

        /// <summary>
        /// Helper method, used to paint a given node either
        /// red or black in color. Setting a null node, causes
        /// no change to happen.
        /// </summary>
        /// <param name="node">Node to color</param>
        /// <param name="red">Set true to color the node red,
        /// and set false to color it black</param>
        private static void SetRed(NodeBinary<E> node, bool red)
        {
            var temp = node as NodeRedBlack<E>;
            if (temp != null) temp.IsRed = red;
        }

        #endregion //////////////////////////////////////////////////////////
    }

    /// <summary>
    /// This is a special extension of a binary node to include the
    /// red-black color of the node. It is intended for use only 
    /// within red-black trees.
    /// </summary>
    /// <typeparam name="E">The data type of the node</typeparam>
    /// <remarks>Last Update: 2016-06-12</remarks>
    internal sealed class NodeRedBlack<E> : NodeBinary<E>
    {
        //determines if the given node is red
        private bool is_red;

        /// <summary>
        /// Constructs a new binary tree node.
        /// </summary>
        /// <param name="item">The data inside the node</param>
        /// <param name="red">Set true to color the node red,
        /// false to color it black</param>
        public NodeRedBlack(E item, bool red = false)
            : base(item) { is_red = red; }

        /// <summary>
        /// Determines the color of the node. It returns true if the
        /// node is red, and false if it is black.
        /// </summary>
        public bool IsRed
        {
            get { return is_red; }
            set { is_red = value; }
        }
    }
}
