using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data
{
    /// <summary>
    /// A keyed item is any generic item that has a corisponding key asociated
    /// with it. When a keyed item is compared with another keyed item, it compares
    /// based on the keys, rather than on the items themselves. Therefore, the key
    /// must not be null and should have a proper hash funciton defined, to avoid
    /// unnessary hash collisions. There are no restrictions on the item itself.
    /// This is usefull when you want to sort or refrence items based on some
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
        /// Cronstructs a fake key, which dosent point to any item. This is
        /// usefull in searching for a matching key inside a collection.
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
        /// Determins if two keyed items are equivlent to each other
        /// by comparing their keys. As long as the keys match, they
        /// are considered the same item, even if the items themselves
        /// differ.
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if the objects are equal</returns>
        public override bool Equals(object obj)
        {
            //makes shure the object is a keyed item
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
        /// Obtains the key refrencing the current item.
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

            //atempts to use the default comparison method for keys
            var comp = key as IComparable<K>;
            if (comp != null) return comp.CompareTo(other.Key);

            //throws an exception if we are unable to compare keys
            throw new InvalidOperationException();
        }

        #endregion /////////////////////////////////////////////////////////////
    }
}
