using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vulpine.Core.Data.Exceptions;

namespace Vulpine.Core.Data.Trees
{
    public class TreeAVL2<E> : TreeBasic<E>
    where E : IComparable<E>
    {
        #region Class Definitions...

        ///// <summary>
        ///// Creates an new AVL search tree, empty of any items, with the 
        ///// desired itteration order and duplicte handeling.
        ///// </summary>
        ///// <param name="sort">How the tree should handel duplicats</param>
        ///// <param name="ord">How the tree should itterate it's items</param>
        //public TreeAVL2(TreeSort sort = TreeSort.None,
        //    TreeOrder ord = TreeOrder.InOrder) : base(sort, ord) {}

        ///// <summary>
        ///// Creates a new AVL search tree, containing the given ititial items, 
        ///// with the desired itteration order and duplicate handeling.
        ///// </summary>
        ///// <param name="sort">How the tree should handel duplicates</param>
        ///// <param name="ord">How the tree should itterate it's items</param>
        ///// <param name="items">Items to be inserted</param>
        //public TreeAVL2(TreeSort sort, TreeOrder ord, 
        //    params E[] items) : base(sort, ord) {}

        public TreeAVL2() : base() { }
        public TreeAVL2(params E[] items) : base(items) { }
        public TreeAVL2(IEnumerable<E> items) : base(items) { }

        #endregion /////////////////////////////////////////////////////////////

        #region Balancing Opperations...

        /// <summary>
        /// Creates a new AVL node containing the designated item.
        /// By definition, all leaf nodes start out balanced.
        /// </summary>
        /// <param name="item">Item inside the node</param>
        /// <returns>A new red-black node</returns>
        protected override NodeBinary<E> CreateNode(E item)
        {
            //generates a new AVL tree node
            return new NodeAVL<E>(item, 0);
        }

        /// <summary>
        /// Rebalance the tree after a node has been insterted, and
        /// sets all the balance factors accordingly.
        /// </summary>
        /// <param name="node">Node that was inserted</param>
        protected override void RebalInsert(NodeBinary<E> node)
        {
            //uses a trailing refrence in rebalancing
            NodeBinary<E> curr = node.Parent;
            NodeBinary<E> prev = node;

            while (curr != null)
            {
                //inrements the balance factor accordingly
                if (curr.Left == prev) IncBalance(curr, -1);
                if (curr.Right == prev) IncBalance(curr, 1);

                //rebalances the subtree at the curent node
                prev = curr;
                curr = InsertSingle(curr);
            }
        }

        /// <summary>
        /// Atempts to rebalance the tree before a node has been removed,
        /// and sets all the balance factors accordingly.
        /// </summary>
        /// <param name="node">Node to be deleted</param>
        /// <param name="child">Child of node to be deleted</param>
        protected override void RebalDelete(NodeBinary<E> node, NodeBinary<E> child)
        {
            //uses a trailing refrence in rebalancing
            NodeBinary<E> curr = node.Parent;
            NodeBinary<E> prev = node;

            while (curr != null)
            {
                ////inrements the balance factor accordingly
                //if (curr.Left == prev) IncBalance(curr, 1);
                //if (curr.Right == prev) IncBalance(curr, -1);

                //rebalances the subtree at the curent node
                NodeBinary<E> temp = curr;
                curr = RemoveSingle(curr, prev);
                prev = temp;
            }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Single Cases...

        /// <summary>
        /// Atempts to rebalance the sub-tree rooted at the given node. If the
        /// tree can be balanced, it returns null as no further rebalancing
        /// is required. Otherwise, it returns the next node in the sequence.
        /// </summary>
        /// <param name="node">Node to be rebalanced</param>
        /// <returns>Next node in sequence, or null</returns>
        private NodeBinary<E> InsertSingle(NodeBinary<E> node)
        {
            //obtains the nodes balance factor
            int bal = GetBalance(node);

            if (bal == 0) 
            {
                //we're already balanced
                return null;
            }
            else if (bal >= 2)
            {
                bal = GetBalance(node.Right);
                if (bal == -1) DoubleRot(node, true);
                else SingRotA(node, true);
                return null;
            }            
            else if (bal <= -2)
            {
                bal = GetBalance(node.Left);
                if (bal == 1) DoubleRot(node, false);
                else SingRotA(node, false);
                return null;
            }

            //must keep searching
            return node.Parent;
        }

        private NodeBinary<E> RemoveSingle(NodeBinary<E> node, NodeBinary<E> last)
        {
            //obtains the nodes balance factor
            int bal = GetBalance(node);

            if (bal == 0)
            {
                //our height is unchanged
                if (node.Left == last) IncBalance(node, 1);
                if (node.Right == last) IncBalance(node, -1);
                return null;
            }
            else if (bal == 1 || bal == -1)
            {
                SetBalance(node, 0);
                return node.Parent;
            }
            else if (bal >= 2)
            {
                bal = GetBalance(node.Right);
                if (bal == 0)
                {
                    NodeBinary<E> temp = node.Parent;
                    SingRotB(node, true);
                    return temp;
                }

                if (bal == 1) SingRotA(node, true);
                else DoubleRot(node, true);

                return null;
            }
            else if (bal <= -2)
            {
                bal = GetBalance(node.Right);
                if (bal == 0)
                {
                    NodeBinary<E> temp = node.Parent;
                    SingRotB(node, false);
                    return temp;
                }

                if (bal == 1) SingRotA(node, false);
                else DoubleRot(node, false);

                return null;
            }

            //must keep searching
            return node.Parent;
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Rotation Opperations...

        private void SingRotA(NodeBinary<E> node, bool left)
        {
            NodeBinary<E> child = node.GetChild(left);

            //calls for the correct rotation
            if (left) RotateLeft(node);
            else RotateRight(node);

            //updates the balances accordingly
            SetBalance(child, 0);
            SetBalance(node, 0);
        }

        private void SingRotB(NodeBinary<E> node, bool left)
        {
            NodeBinary<E> child = node.GetChild(left);

            //calls for the correct rotation
            if (left) RotateLeft(node);
            else RotateRight(node);

            //updates the balances accordingly
            int shift = left ? -1 : 1;
            IncBalance(child, shift);
            IncBalance(node, -shift);
        }

        private void DoubleRot(NodeBinary<E> node, bool rl)
        {
            //obtains the nodes invloves in the rotation
            NodeBinary<E> child = node.GetChild(rl);
            int bal = GetBalance(child.GetChild(!rl));

            //preforms the double rotation
            SingRotA(child, !rl);
            SingRotA(node, rl);

            if (rl)
            {
                //resets the balances for a right-left rotation
                if (bal == -1) SetBalance(child, 1);
                if (bal == 1) SetBalance(node, -1);
            }
            else
            {
                //resets the balances for a left-right rotation
                if (bal == -1) SetBalance(node, 1);
                if (bal == 1) SetBalance(child, -1);
            }        
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Balance Factor Access...

        /// <summary>
        /// Helper method; obtains the balance of the sub-tree rooted at 
        /// the given node. Null nodes are balanced by definition.
        /// </summary>
        /// <param name="node">Node to check</param>
        /// <returns>Balance factor of the sub-tree</returns>
        private static int GetBalance(NodeBinary<E> node)
        {
            if (node == null) return 0;
            NodeAVL<E> temp = (NodeAVL<E>)node;
            return temp.Balance;
        }

        /// <summary>
        /// Helper method; sets the balance factor stored in the node
        /// to the desired value. Setting a null node, causes no change.
        /// </summary>
        /// <param name="node">Node to alter</param>
        /// <param name="bal">Balance factor</param>
        private static void SetBalance(NodeBinary<E> node, int bal)
        {
            if (node == null) return;
            NodeAVL<E> temp = (NodeAVL<E>)node;
            temp.Balance = bal;
        }

        /// <summary>
        /// Helper method; inrements (or decrements) the node's balance
        /// factor by the desired abount. No change happens for null nodes.
        /// </summary>
        /// <param name="node">Node to alter</param>
        /// <param name="diff">Amount to shift balance</param>
        private static void IncBalance(NodeBinary<E> node, int diff)
        {
            if (node == null) return;
            NodeAVL<E> temp = (NodeAVL<E>)node;
            temp.Balance += diff;
        }

        #endregion //////////////////////////////////////////////////////////
    }
}
