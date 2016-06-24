﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vulpine.Core.Data.Exceptions;

namespace Vulpine.Core.Data
{
    /// <summary>
    /// A Binary Node allows for up to two succeseors per node, labled the left
    /// and right child respectfully. It is possable for a Binary Node to have
    /// no children, in which case it is called a Leaf. Each Binary Node also
    /// contains a special refrence to it's Parrent node. The Parent of a Node
    /// is a Node that contains the original as a child. The Parent refrence is
    /// updated automaticly, whenever you update the Left and Right references.
    /// </summary>
    /// <typeparam name="T">The data type of the node</typeparam>
    /// <remarks>Last Update: 2016-05-24</remarks>
    public class NodeBinary<T> : Node<T>
    {
        #region Class Definitions...

        //the item being refrenced
        private T item;

        //the children of the current node
        private NodeBinary<T> left;
        private NodeBinary<T> right;

        //the parent of the current node
        private NodeBinary<T> parent;

        /// <summary>
        /// Constructs a new binary tree node.
        /// </summary>
        /// <param name="item">The data inside the node</param>
        /// <param name="left">The node's left child</param>
        /// <param name="right">The node's right child</param>
        public NodeBinary(T item, 
            NodeBinary<T> left = null, NodeBinary<T> right = null)
        {
            this.item = item;
            this.left = left;
            this.right = right;

            //takes care of assigning parrent links
            if (left != null) left.parent = this;
            if (right != null) right.parent = this;

            parent = null;
        }

        /// <summary>
        /// Generates a string representation of the node, repeating the
        /// contents of the node in string format.
        /// </summary>
        /// <returns>The node in string format</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Node: ");
            if (item == null) sb.Append("<NULL>");
            else sb.Append(item);

            if (left != null)
            {
                sb.Append(" -> ");
                if (left.item == null) sb.Append("<NULL>");
                else sb.Append(left.item);

                if (right != null)
                {
                    sb.Append(", ");
                    if (right.item == null) sb.Append("<NULL>");
                    else sb.Append(right.item);
                }
            }
            else if (right != null)
            {
                sb.Append(" -> ");
                if (right.item == null) sb.Append("<NULL>");
                else sb.Append(right.item);
            }

            return sb.ToString();
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Node Linkings...
        
        /// <summary>
        /// The data contained within the node.
        /// </summary>
        public T Data
        {
            get { return item; }
            set { item = value; }
        }

        /// <summary>
        /// The left child of the node. Setting this value updates the
        /// left child's parent refrence.
        /// </summary>
        public NodeBinary<T> Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
                if (left != null) 
                left.parent = this;
            }
        }

        /// <summary>
        /// The right child of the node. Setting this value updates the
        /// right child's parent refrence.
        /// </summary>
        public NodeBinary<T> Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
                if (right != null) 
                right.parent = this;
            }
        }

        /// <summary>
        /// The parent of the current node. The parent refrence is
        /// maintained internaly, but may be set to NULL in order
        /// to clear the refrence.
        /// </summary>
        public NodeBinary<T> Parent
        {
            get 
            { 
                return parent; 
            }
            set 
            {
                if (value != null) return;
                else parent = null;
            }
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Node Opperations...

        /// <summary>
        /// Finds the unique sibling of the current node: the opposite child 
        /// of the parent of this node. If the node dose not have a sibling, 
        /// null is returned.
        /// </summary>
        /// <returns>The node's sibling, or null</returns>
        public NodeBinary<T> GetSibling()
        {
            //obtains the sibling of the current node
            if (parent == null) return null;
            if (this == parent.left) return parent.right;
            if (this == parent.right) return parent.left;

            return null;
        }

        /// <summary>
        /// Obtains either the left or right child of the node. A value of
        /// true will return the right child, while a value of false will
        /// return the left.
        /// </summary>
        /// <param name="right">Set to true to return the right child,
        /// set to false to return the left</param>
        /// <returns>The designated child</returns>
        public NodeBinary<T> GetChild(bool right)
        {
            //returns the desired child
            return (right) ? this.right : this.left;
        }

        /// <summary>
        /// Returns the non-null children of this node. Left comes 
        /// before right, if the node has two children.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Node<T>> ListChildren()
        {
            if (left != null) yield return left;
            if (right != null) yield return right;
        }

        /// <summary>
        /// Removes all linked refrences from this node inorder to avoid
        /// memory leaks. This should only be called when the node structure
        /// is being broken down.
        /// </summary>
        public void Dispose()
        {
            left = null;
            right = null;
            parent = null;
        }

        #endregion ///////////////////////////////////////////////////////////////
    }

}
