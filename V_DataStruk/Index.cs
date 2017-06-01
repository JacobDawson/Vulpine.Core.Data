/**
 *  This file is an integral part of the Vulpine Core Library: 
 *  Copyright (c) 2016-2017 Benjamin Jacob Dawson. 
 *
 *      http://www.jakesden.com/corelibrary.html
 *
 *  This file is licensed under the Apache License, Version 2.0 (the "License"); 
 *  you may not use this file except in compliance with the License. You may 
 *  obtain a copy of the License at:
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.    
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//SC

namespace Vulpine.Core.Data
{
    /// <summary>
    /// This structure represents a given index into an index-base data structure, such
    /// as a list. It exists so that we can use negative indices to indicate positions
    /// from the end of the data structure. Because there may be many data structure that
    /// use such an indexing system, it helps to include the logic inside a single Index
    /// structure, rather than use plane integers. It also supports implicit conversion
    /// from all major integer types, so there should be no difference in functionality
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
        /// Calculates the desired index, given the size of the data structure.
        /// </summary>
        /// <param name="size">Size of the data structure</param>
        /// <returns>The desired index, or null if the index would be
        /// outside the range of the data structure</returns>
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
                //the end of our indexed data structure
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
        /// outside the range of the data structure.
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
