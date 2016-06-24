using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data
{
    /// <summary>
    /// This structor represnts a given index into an index-base data structor, sutch
    /// as a list. It exists so that we can use negative indicies to indicate postions
    /// from the end of the data structor. Because there may be many data structors that
    /// use sutch an indexing system, it helps to include the logic inside a single Index
    /// structor, rather than use plane intergers. It also suports implicit conversion
    /// from all major interger types, so there should be no difrence in functionality
    /// from the user's perspective.
    /// </summary>
    /// <remarks>Last Update: 2016-05-19</remarks>
    public struct Index
    {
        #region Class Definitions...

        //stores the original value given
        private int index;

        /// <summary>
        /// Constructs a new index with the given value
        /// </summary>
        /// <param name="index">Value of the index</param>
        public Index(int index)
        {
            this.index = index;
        }

        /// <summary>
        /// Returns the object as a string
        /// </summary>
        /// <returns>The object as a string</returns>
        public override string ToString()
        {
            return index.ToString();
        }

        /// <summary>
        /// Determins if this object is equal to another object.
        /// </summary>
        /// <param name="obj">Object of comparison</param>
        /// <returns>True if the objects are equal</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Index)) return false;
            return index.Equals(((Index)obj).index);
        }

        /// <summary>
        /// Generates a sudo-unique hash code for this particular object.
        /// </summary>
        /// <returns>The hash of the object</returns>
        public override int GetHashCode()
        {
            return index.GetHashCode();
        }

        #endregion //////////////////////////////////////////////////////////////////

        #region Index Operators...

        /// <summary>
        /// Calculates the desired index, given the size of the data structor.
        /// </summary>
        /// <param name="size">Size of the sata structor</param>
        /// <returns>The desried index, or null if the index would be
        /// outside the ragne of the data structor</returns>
        public int? GetIndex(int size)
        {
            if (index >= size)
            {
                //the index is outside our accepted range
                return null;
            }
            else if (index >= 0)
            {
                //we simply use the index
                return index;
            }
            else if (index >= -size)
            {
                //we infer the index to be counting backwards from
                //the end of our indexed data structor
                return (size + index);
            }
            else
            {
                //the index is outside our accepted range
                return null;
            }
        }

        /// <summary>
        /// Obtains the original value given, which may be negative or lie
        /// outisde the range of the data structor.
        /// </summary>
        /// <returns></returns>
        public int GetOriginal()
        {
            //simply return the index unmodified
            return index;
        }

        #endregion //////////////////////////////////////////////////////////////////

        #region Class Conversions...

        public static implicit operator Index(int n)
        { return new Index(n); }

        public static implicit operator Index(short n)
        { return new Index(n); }

        public static implicit operator Index(byte n)
        { return new Index(n); }

        public static implicit operator Index(ushort n)
        { return new Index(n); }

        public static implicit operator Index(sbyte n)
        { return new Index(n); }

        #endregion //////////////////////////////////////////////////////////////////
    }
}
