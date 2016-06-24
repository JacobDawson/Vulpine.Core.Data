using System;

namespace Vulpine.Core.Data
{
    /// <summary>
    /// This interface serves as a flag for all data structors which use an
    /// array based implementation. It also exposes part of the internal
    /// array's properties, notably it's size. This is important for
    /// aproximating memory consumption.
    /// </summary>
    /// <remarks>Last Update: 2013-06-03</remarks>
    public interface ArrayBased
    {
        /// <summary>
        /// Represents the container's current capacity; the number of
        /// items the container is able to store without resizing, as
        /// opposed to the number it acutaly contains. Read-Only
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// Represents the container's base capacity. Althought the
        /// capacity may grow and shrink as needed, it will never drop
        /// below this value. Read-Only
        /// </summary>
        int BaseCapacity { get; }
    }
}
