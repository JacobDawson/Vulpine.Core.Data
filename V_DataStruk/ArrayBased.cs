/**
 *  This file is an integral part of the Vulpine Core Library: 
 *  Copyright (c) 2016-2017 Benjamin Jacob Dawson. 
 *
 *      https://www.jacobs-den.org/projects/core-library/
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
