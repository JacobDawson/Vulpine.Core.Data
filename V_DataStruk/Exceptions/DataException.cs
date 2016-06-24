﻿using System;

namespace Vulpine.Core.Data.Exceptions
{
    /// <summary>
    /// This is the base class for all exceptions generated by and
    /// pretaning to data structors.
    /// </summary>
    public class DataStruckExcp : Exception
    {
        /// <summary>
        /// Creates a new exception with the given message.
        /// </summary>
        /// <param name="msg">A message explaning the nature and
        /// cause of the exception</param>
        public DataStruckExcp(string msg) : base(msg) { }

        /// <summary>
        /// Creates a new data exception. Sub-clases should provide
        /// a custom message by overriding the virtual Message property.
        /// </summary>
        public DataStruckExcp() : base() { }
    }
}
