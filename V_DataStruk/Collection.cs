using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine.Core.Data
{
    /// <summary>
    /// This abstract class provides the basic funcitonality of all collection-type
    /// data structors in the core library. A colleciton is any group of items that
    /// share some common structor. Furthermore, it is not permissable to store NULL
    /// items in a colleciton, as NULL values have the special connotation of "This
    /// item is missing from the collection". It is similar to the ICollection interface 
    /// but diffrent in certain key aspects. First of all, it is an abstract class, 
    /// not an interface. It includes a default dequeue method, providing queue-like 
    /// operations for all collecitons. Not all collections implement the default 
    /// remove method, so it should be used with caution. Although it dose implement
    /// the ICollection interface, this is done primarly to suport LINQ operations,
    /// which run more effecently when the sequence being processed implements ICollection.
    /// </summary>
    /// <typeparam name="E">The element type of the collection</typeparam>
    /// <remarks>Last Update: 2016-06-14</remarks>
    public abstract class VCollection<E> : ICollection<E>,  IDisposable
    {
        #region Default Properties...

        /// <summary>
        /// Represents the number of items in the collection.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Determins if the collection is empty or contains items. It is
        /// set to true if empty and false if otherwise.
        /// </summary>
        public virtual bool Empty
        {
            get { return Count <= 0; }
        }

        /// <summary>
        /// Determins if the collection only allows read access. By default 
        /// data structors allow both read and wright access.
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return false; }
        }
        
        #endregion //////////////////////////////////////////////////////////////

        #region Default Operations...

        /// <summary>
        /// Determins if the collection contains a paticular item.
        /// </summary>
        /// <param name="item">The item to test for containment</param>
        /// <returns>True if the item is contained in the collection,
        /// false if otherwise</returns>
        public abstract bool Contains(E item);

        /// <summary>
        /// Inserts an item into the collection.
        /// </summary>
        /// <param name="item">The item to be inserted</param>
        /// <remarks>It overloads the (+) operator</remarks>
        /// <exception cref="ArgumentNullException">If null is inserted</exception>
        public abstract void Add(E item);

        /// <summary>
        /// Removes a single item at random from the colleciton. The
        /// exact item removed is determined by the data structor 
        /// being implemented. If the collection is empty it returns null.
        /// </summary>
        /// <returns>The item that was removed, or null if empty</returns>
        public abstract E Dequeue();

        /// <summary>
        /// Removes a given item from the collection. If the collection contains
        /// duplicate values, the first instance found is removed. It returns
        /// true if the item was succesfully removed, and false if otherwise.
        /// </summary>
        /// <param name="item">Item to be removed</param>
        /// <returns>True if the item was succesfuly removed</returns>
        /// <exception cref="NotSuportedException">If the underlying data
        /// structor dose not suport explitit removal of items</exception>
        public abstract bool Remove(E item);

        /// <summary>
        /// Removes all items from the collection. Causing the collection
        /// to revert to it's original, initialised state.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Creates an enumeration over all the items in the collection. 
        /// </summary>
        /// <returns>An enumerator over the colection</returns>
        public abstract IEnumerator<E> GetEnumerator();

        #endregion //////////////////////////////////////////////////////////////

        #region Additional Opperations...

        /// <summary>
        /// Inserts multiple items into the colleciton.
        /// </summary>
        /// <param name="items">Items to insert</param>
        /// <exception cref="ArgumentNullException">If a null value is 
        /// inserted into the collection</exception>
        public virtual void Add(params E[] items)
        {
            //simply calls the add method repeatidly
            foreach (E item in items) Add(item);
        }

        /// <summary>
        /// Inserts multiple items into the colleciton.
        /// </summary>
        /// <param name="items">Items to insert</param>
        /// <remarks>It overloads the (+) operator</remarks>
        /// <exception cref="ArgumentNullException">If a null value is 
        /// inserted into the collection</exception>
        public virtual void Add(IEnumerable<E> items)
        {
            //simply calls the add method repeatidly
            foreach (E item in items) Add(item);
        }

        /// <summary>
        /// Copies the elements of the colleciton into an array with a given
        /// offset. This is implemented primarly in suport of the ICollection
        /// interface, for which it is required.
        /// </summary>
        /// <param name="array">The array in which to place the items</param>
        /// <param name="offset">The offest index into the array</param>
        /// <exception cref="ArgumentNullException">If the array is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the offset is less
        /// than zero, or dose not leave enough room for the collection to fit
        /// into the rest of the array</exception>
        public virtual void CopyTo(E[] array, int offset)
        {
            //checks that the colleciton will fit into the array
            if (array == null) throw new ArgumentNullException("array");
            if (offset < 0 || offset > array.Length - Count)
                throw new ArgumentOutOfRangeException("offset");

            //copies the items into the array, one by one
            foreach (E item in this)
            {
                array[offset] = item;
                offset = offset + 1;
            }
        }

        /// <summary>
        /// Provides a dispose method to comply with the IDisposable syntax,
        /// as many collections require cleanup after use. By default, it
        /// simply clears the collection of all data.
        /// </summary>
        public virtual void Dispose()
        {
            Clear();
        }

        #endregion //////////////////////////////////////////////////////////////

        #region Operator Overlodes...

        /// <summary>
        /// Calls the Add() method and returns a refrence to the same collection.
        /// This is so that multiple insertions can be chained together.
        /// </summary>
        public static VCollection<E> operator+(VCollection<E> container, E item)
        { 
            container.Add(item); 
            return container; 
        }

        /// <summary>
        /// Calls the Add() method and returns a refrence to the same collection.
        /// This is so that multiple insertions can be chained together.
        /// </summary>
        public static VCollection<E> operator+(VCollection<E> container, IEnumerable<E> items)
        { 
            container.Add(items); 
            return container; 
        }

        #endregion //////////////////////////////////////////////////////////////

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
