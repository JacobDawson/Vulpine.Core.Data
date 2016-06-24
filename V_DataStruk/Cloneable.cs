using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data
{
    /// <summary>
    /// This interface is a generic version of the ICloneable interface.
    /// Something that seems to be strangly lacking in the default library.
    /// </summary>
    /// <typeparam name="T">Type of the cloned object</typeparam>
    /// <remarks>Last Update: 2013-09-16</remarks>
    public interface Cloneable<out T> : ICloneable
    {
        /// <summary>
        /// Generates a deep copy of the current object, protecting the
        /// original, possably mutable, object instance.
        /// </summary>
        /// <returns>A deep copy of the object</returns>
        new T Clone();
    }
}
