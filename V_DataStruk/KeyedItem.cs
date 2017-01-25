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

//SC

namespace Vulpine.Core.Data
{
    /// <summary>
    /// A keyed item is any generic item that has a corresponding key associated
    /// with it. When a keyed item is compared with another keyed item, it compares
    /// based on the keys, rather than on the items themselves. Therefore, the key
    /// must not be null and should have a proper hash function defined, to avoid
    /// unnecessary hash collisions. There are no restrictions on the item itself.
    /// This is useful when you want to sort or reference items based on some
    /// other factor, namely the key for that item.
    /// </summary>
    /// <typeparam name="K">The key type of the pair</typeparam>
    /// <typeparam name="E">The value type of the pair</typeparam>
    /// <remarks>Last Update: 2016-06-02</remarks>
    public sealed class KeyedItem<K, E> : IComparable<KeyedItem<K, E>>
    {
        #region Class Definitions...

        //stores the key and the item
        private K key;
        private E item;

        /// <summary>
        /// Constructs a new keyed item.
        /// </summary>
        /// <param name="key">Key for the item</param>
        /// <param name="value">The item contained</param>
        /// <exception cref="ArgumentNullException">If the key is null</exception>
        public KeyedItem(K key, E value)
        {
            //we do not allow for null keys to exist
            if (key == null) throw new ArgumentNullException("key");

            this.key = key;
            this.item = value;
        }

        /// <summary>
        /// Constructs a fake key, which doesn't point to any item. This is
        /// useful in searching for a matching key inside a collection.
        /// </summary>
        /// <param name="key">The fake key</param>
        /// <exception cref="ArgumentNullException">If the key is null</exception>
        public KeyedItem(K key)
        {
            //we do not allow for null keys to exist
            if (key == null) throw new ArgumentNullException("key");

            this.key = key;
            this.item = default(E);
        }

        /// <summary>
        /// Generates a string listing both the key and the item.
        /// </summary>
        /// <returns>The pair as a string</returns>
        public override string ToString()
        {
            string sitem = (item == null) ? "NULL" : item.ToString();
            return String.Format("{0} = {1}", key, sitem);
        }

        /// <summary>
        /// Determines if two keyed items are equivalent to each other
        /// by comparing their keys. As long as the keys match, they
        /// are considered the same item, even if the items themselves
        /// differ.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if the objects are equal</returns>
        public override bool Equals(object obj)
        {
            //makes sure the object is a keyed item
            var other = obj as KeyedItem<K, E>;
            if (other == null) return false;

            //compares the keys
            return key.Equals(other.key);
        }

        /// <summary>
        /// Hashes the key for the current keyed item pair. The item
        /// itself is not used in constructing the hash code.
        /// </summary>
        /// <returns>Hash value for the key</returns>
        public override int GetHashCode()
        {
            return key.GetHashCode();
        }

        #endregion /////////////////////////////////////////////////////////////

        #region Class Properties...

        /// <summary>
        /// Obtains the item assigned to the current key. 
        /// </summary>
        public E Item
        {
            get { return item; }
        }

        /// <summary>
        /// Obtains the key referencing the current item.
        /// </summary>
        public K Key
        {
            get { return key; }
        }

        #endregion /////////////////////////////////////////////////////////////

        #region IComparable Implementation...

        /// <summary>
        /// Compares the key of the current keyed item to the key of another
        /// keyed item. The default comparison method for the keys is used.
        /// </summary>
        /// <param name="other">Keyed item to compare</param>
        /// <returns>A negative number if the current pair comes first, a
        /// positive number if it comes second, and zero if they are equal.
        /// </returns>
        public int CompareTo(KeyedItem<K, E> other)
        {
            //checks for a valid target
            if (other == null) return -1;

            //attempts to use the default comparison method for keys
            var comp = key as IComparable<K>;
            if (comp != null) return comp.CompareTo(other.Key);

            //throws an exception if we are unable to compare keys
            throw new InvalidOperationException();
        }

        #endregion /////////////////////////////////////////////////////////////
    }
}
