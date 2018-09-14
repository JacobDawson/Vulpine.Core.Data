/**
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
