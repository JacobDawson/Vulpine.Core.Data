using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.Trees
{
    /// <summary>
    /// This is a special extention of a binary node to include the
    /// balance factor of the subtree rooted at that node. It is
    /// intended for use only within AVL trees; it serves no 
    /// exterior purpous.
    /// </summary>
    /// <typeparam name="E">The data type of the node</typeparam>
    /// <remarks>Last Update: 2013-07-26</remarks>
    internal class NodeAVL<E> : NodeBinary<E>
    {
        //stores the balance mesure for the node compactly
        private sbyte balance;

        /// <summary>
        /// Constructs a new binary tree node.
        /// </summary>
        /// <param name="item">The data inside the node</param>
        /// <param name="bal">Balance factor of the node</param>
        public NodeAVL(E item, int bal = 0)
            : base(item) { balance = (sbyte)bal; }

        /// <summary>
        /// Represents the balance factor of the sub-tree rooted at
        /// this node. Negative numbers indicate a left-heavy node,
        /// while positive number indicate a right-heavy node.
        /// </summary>
        public int Balance
        {
            get { return balance; }
            set { balance = (sbyte)value; }
        }
    }
}
