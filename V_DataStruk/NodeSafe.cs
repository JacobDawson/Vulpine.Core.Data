using System;
using System.Collections.Generic;

using Vulpine.Core.Data.Exceptions;


namespace Vulpine.Core.Data
{
    /// <summary>
    /// This class provides a read-only wraper to all classes that implement the
    /// Node interface. This allows for external code to inspect the structor
    /// of Trees and Graphs and the like, while maintaing the integrity of there
    /// internal structor. It also employes lazy evaluation, creating new wrapers
    /// for Nodes only when required. This allows it to be more effecent than
    /// copying the entire structor verbatum.
    /// </summary>
    /// <typeparam name="T">The data type of the node</typeparam>
    /// <remarks>Last Update: 2016-05-24</remarks>
    public sealed class NodeSafe<T> : Node<T>
    {
        #region Class Definitions...

        private const string SET_ERR =
            "It's not possable to set the value contained by a Safe Node, " +
            "as doing so could disrupt the underlying data structor that " +
            "the Safe Node was ment to protect. ";

        //stores a refrence to the interior node
        private Node<T> inner;

        /// <summary>
        /// Creates a read-only node by linking to another node, thus
        /// creating a safe refrence to that node.
        /// </summary>
        /// <param name="inner">The node to refrence</param>
        public NodeSafe(Node<T> inner)
        {
            this.inner = inner;
        }

        /// <summary>
        /// Generates a string representation of the node, repeating the
        /// contents of the node in string format.
        /// </summary>
        /// <returns>The node in string format</returns>
        public override string ToString()
        {
            //if we've been disposed, give an error message
            if (inner == null) return "Deleated Node";

            //calls upon the inner node
            return inner.ToString();
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Node Linkings...

        /// <summary>
        /// The data contained within the node. It is denied write
        /// acces inorder to protect the original node. 
        /// </summary>
        public T Data
        {
            get 
            {
                if (inner == null) return default(T);
                else return inner.Data; 
            }
            set
            {
                //we arn't allowed to change the internal structor
                throw new InvalidOperationException(SET_ERR);
            }
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Node Implementation...

        /// <summary>
        /// Lists the collective children of the node. Any given node may 
        /// have zero or more children, but null children are not included.
        /// </summary>
        /// <returns>The children of the node</returns>
        public IEnumerable<Node<T>> ListChildren()
        {
            //if we have been disposed, there is nothing to return
            if (inner == null) yield break;

            //wraps each child in another safe node, to protect it
            foreach (Node<T> child in inner.ListChildren())
                yield return new NodeSafe<T>(child);
        }

        /// <summary>
        /// Preforms a shallow clearing of the read-only node. The internior
        /// node cannot be disposed because it may still be in use else-where.
        /// </summary>
        public void Dispose()
        {
            //here we only do a shallow disposal
            inner = null;
        }

        #endregion ///////////////////////////////////////////////////////////////
    }
}
