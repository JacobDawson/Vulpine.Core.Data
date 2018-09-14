﻿/**
 *  This file is an integral part of the Vulpine Core Library
 *  Copyright (c) 2016-2018 Benjamin Jacob Dawson
 *
 *      http://www.jakesden.com/corelibrary.html
 *
 *  The Vulpine Core Library is free software; you can redistribute it 
 *  and/or modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License, or (at your option) any later version.
 *
 *  The Vulpine Core Library is distributed in the hope that it will 
 *  be useful, but WITHOUT ANY WARRANTY; without even the implied 
 *  warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 *  See the GNU Lesser General Public License for more details.
 *
 *      https://www.gnu.org/licenses/lgpl-2.1.html
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data.Exceptions
{
    /// <summary>
    /// When dealing with data sturctors, many methods will require some arguments
    /// to be within a certain interger range, sutch as indexes. Also, when collections
    /// are passed as argumetns, there size may also be expected to be withing a
    /// certain range. This is the method thrown in all these cases, when the size
    /// of something goes outside it's expected range.
    /// </summary>
    /// <remarks>Last Update: 2015-10-01</remarks>
    public sealed class ArgRangeExcp : DataStruckExcp
    {
        #region Class Definitions...

        /// <summary>
        /// The default message generated by a boundary exception, prior to 
        /// applying any special case formating.
        /// </summary>
        public const string MSG = "The argument \"{0}\" was outside the "
            + "expected range between {1} and {2}, inclusive. ";

        //stores the name of the argument that caused the exception
        private string arg;

        //stores a refrence to the bounds
        private int lower;
        private int upper;

        //stores a refrence to the value given
        private int actual;

        /// <summary>
        /// Creates a new argument range exception, forwarding the bounds 
        /// onward tward the exception handeler.
        /// </summary>
        /// <param name="arg">The name of the argument</param>
        /// <param name="actual">The value that was given</param>
        /// <param name="low">The lower end of the boundry</param>
        /// <param name="high">The upper end of the bountry</param>
        private ArgRangeExcp(string arg, int actual, int low, int high) : base()
        {
            this.arg = arg;
            this.actual = actual;
            this.lower = low;
            this.upper = high;
        }

        #endregion /////////////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// Generates a custom message, indicating the nature of the exception
        /// as well as the upper and lower bounds of the expected range.
        /// Read-Only
        /// </summary>
        public override string Message
        {
            get { return String.Format(MSG, arg, lower, upper); }
        }

        /// <summary>
        /// The name of the argument witch caused the exception
        /// </summary>
        public string Argument
        {
            get { return arg; }
        }

        /// <summary>
        /// The lower end of the bound. Read-Only.
        /// </summary>
        public int Lower
        {
            get { return lower; }
        }

        /// <summary>
        /// The upper end of the bound. Read-Only.
        /// </summary>
        public int Upper
        {
            get { return upper; }
        }

        /// <summary>
        /// The value that generated the error. Read-Only.
        /// </summary>
        public int Actual
        {
            get { return actual; }
        }

        #endregion /////////////////////////////////////////////////////////////////////

        #region Factory Methods...

        /// <summary>
        /// Determins if a given index lies outside a given range and throws
        /// a corisponding ArgRangeException if that is the case. Otherwise, it
        /// dose nothing.
        /// </summary>
        /// <param name="arg">The name of the argument being checked</param>
        /// <param name="x">The paramater to check</param>
        /// <param name="low">Lower end of the excpected range, inclusive</param>
        /// <param name="high">Upper end of the excpected range, inclusive</param>
        public static void Check(string arg, int x, int low, int high)
        {
            //throws an exception if the check condtions are not met
            if (x < low || x > high) throw new ArgRangeExcp(arg, x, low, high);
        }

        /// <summary>
        /// Determins if a given index lies outside a given range and throws
        /// a corisponding ArgRangeException if that is the case. The lower end
        /// of the range is impliclitly zero.
        /// </summary>
        /// <param name="arg">The name of the argument being checked</param>
        /// <param name="x">The paramater to check</param>
        /// <param name="high">Upper end of the excpected range, inclusive</param>
        public static void Check(string arg, int x, int high)
        {
            //throws an exception if the check condtions are not met
            if (x < 0 || x > high) throw new ArgRangeExcp(arg, x, 0, high);
        }

        /// <summary>
        /// Determins if an interger value is atleast as mutch as some minimum
        /// value and throws an ArgRangeExcepton if that is not the case.
        /// </summary>
        /// <param name="arg">The name of the argument being checked</param>
        /// <param name="x">The parameter to check</param>
        /// <param name="low">Lower end of the excpected range, inclusive</param>
        public static void Atleast(string arg, int x, int low)
        {
            //throws an exception if the check condtions are not met
            if (x < low) throw new ArgRangeExcp(arg, x, low, Int32.MaxValue);
        }

        public static void CheckLenOff(string arg, int length, int offset, int max)
        {
            if (length + offset > max)
            throw new ArgRangeExcp(arg, length, 0, max - offset);
        }

        #endregion /////////////////////////////////////////////////////////////////////
    }
}
