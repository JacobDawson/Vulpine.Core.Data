using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//spell-checked

namespace Vulpine.Core.Data
{
    /// <summary>
    /// In simplest terms, a Node is a vertex of a directed graph that contains
    /// some data. Looking at it another way, Nodes connect isolated pieces of
    /// data to one another, allowing complex data structures to be built. Each
    /// node will contain a single piece of data and zero, one, or more references
    /// to other nodes. The nodes referenced by a given node are referred to as that
    /// node's children. Nodes are also made disposable, to clear up any cyclical
    /// connections that may lead to memory leaks.
    /// </summary>
    /// <typeparam name="E">The data type of the node</typeparam>
    /// <remarks>Last Update: 2016-05-24</remarks>
    public interface Node<T> : IDisposable
    {
        /// <summary>
        /// The data contained within the node. Read-Only
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// Lists the collective children of the node. Any given node may 
        /// have zero or more children, but null children are not included.
        /// </summary>
        /// <returns>The children of the node</returns>
        IEnumerable<Node<T>> ListChildren();
    }
}
