using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data
{
    /// <summary>
    /// This abstract class provides the basic functionality of all dictionary-type
    /// data structors in the core library. A ditionary is a group of two distinct
    /// types of items, keys and values, where the keys share some sort of relational
    /// property with the values. Neither NULL Keys or values may be stored in a
    /// dictionary, as the value NULL may carry other conotations. It differs greatly
    /// from the IDictionary interface, which more closly resembles the Table class. 
    /// Most importantly, it is insertion-only, it dose not allow removal of any kind. 
    /// Derived classes may implement their own specialised remove methods, however. 
    /// Although it implements the ICollection interface, this is done primarly to 
    /// suport LINQ operations, which run more effecently when the sequence being 
    /// processed implements ICollection. 
    /// </summary>
    /// <typeparam name="K">Key type of the dictionary</typeparam>
    /// <typeparam name="E">Element type of the dictionary</typeparam>
    /// <remarks>Last Update: 2016-06-20</remarks>
    public abstract class VDictionary<K, E> : ICollection<KeyedItem<K, E>>, 
        IDisposable 
    {
        #region Default Properties...

        /// <summary>
        /// Represents the number of key-value pairs in the dictionary.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Determins if the dictionary is empty or contains items. It is
        /// set to true if empty and false if otherwise.
        /// </summary>
        public virtual bool Empty
        {
            get { return Count <= 0; }
        }

        /// <summary>
        /// Determins if the dictionary requires all keys to be unique.
        /// If so, items with duplicate keys cannot be inserted. By default
        /// duplicate keys are allowed.
        /// </summary>
        public virtual bool UniqueKeys 
        {
            get { return false; }
        }

        /// <summary>
        /// Determins if the dictionary only allows read access. By default 
        /// data structors allow both read and wright access.
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        #endregion ///////////////////////////////////////////////////////////////

        #region Default Operations...

        /// <summary>
        /// Determins if a paticular key is contained in this dictionary.
        /// It returns true if the key exists, and false otherwise.
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>True if the key exists, false if otherwise</returns>
        public abstract bool HasKey(K key);

        /// <summary>
        /// Inserts a value with a given key into the dictionary. 
        /// </summary>
        /// <param name="key">Key of the item to be inserted</param>
        /// <param name="item">Item to be inserted</param>
        /// <exception cref="ArgumentNullException">If either the key or
        /// the item are null</exception>
        /// <exception cref=" InvalidOperationException">If a duplicate key
        /// is inserted when the dictionary requires unique keys</exception>
        public abstract void Add(K key, E item);

        /// <summary>
        /// Removes all items from the dictionary, causing the dictionary
        /// to revert to it's original initialised state.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Creates an enumeration over all the keys and values in the
        /// dictionary, together as keyed items. If you need something
        /// more spesific, consider ListKeys() or ListItems().
        /// </summary>
        /// <returns>An enumeration of keyed items</returns>
        public abstract IEnumerator<KeyedItem<K, E>> GetEnumerator();

        #endregion ///////////////////////////////////////////////////////////////

        #region Additional Opperations...

        /// <summary>
        /// Lists all the items contained within the dictionary.
        /// </summary>
        /// <returns>All the items of the table</returns>
        public virtual IEnumerable<E> ListItems()
        {
            //returns only the values from the default enumerator
            foreach (var temp in this) yield return temp.Item;
        }

        /// <summary>
        /// Lists all the keys contained within the dictionary.
        /// </summary>
        /// <returns>All the keys of the table</returns>
        public virtual IEnumerable<K> ListKeys()
        {
            //returns only the keys from the default enumerator
            foreach (var temp in this) yield return temp.Key;
        }

        /// <summary>
        /// Copies the key-value pairs of the dictionary into an array with a given
        /// offset. This is implemented primarly in suport of the ICollection
        /// interface, for which it is required.
        /// </summary>
        /// <param name="array">The array in which to place the items</param>
        /// <param name="offset">The offest index into the array</param>
        /// <exception cref="ArgumentNullException">If the array is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the offset is less
        /// than zero, or dose not leave enough room for the dictionary to fit
        /// into the rest of the array</exception>
        public virtual void CopyTo(KeyedItem<K, E>[] array, int offset)
        {
            //checks that the colleciton will fit into the array
            if (array == null) throw new ArgumentNullException("array");
            if (offset < 0 || offset > array.Length - Count)
                throw new ArgumentOutOfRangeException("offset");

            //copies the items into the array, one by one
            foreach (KeyedItem<K, E> pair in this)
            {
                array[offset] = pair;
                offset = offset + 1;
            }
        }

        /// <summary>
        /// Provides a dispose method to comply with the IDisposable syntax,
        /// as many dictionaries require cleanup after use. By default, it
        /// simply clears the dictionary of all data.
        /// </summary>
        public virtual void Dispose()
        {
            Clear();
        }

        #endregion ///////////////////////////////////////////////////////////////

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }

        void ICollection<KeyedItem<K, E>>.Add(KeyedItem<K, E> pair)
        { Add(pair.Key, pair.Item); }

        bool ICollection<KeyedItem<K, E>>.Contains(KeyedItem<K, E> pair)
        { return HasKey(pair.Key); }

        bool ICollection<KeyedItem<K, E>>.Remove(KeyedItem<K, E> pair)
        { throw new NotSupportedException(); }
    }
}
