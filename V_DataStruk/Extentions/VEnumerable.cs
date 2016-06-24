using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vulpine.Core.Data.Lists;
using Vulpine.Core.Data.Queues;

namespace Vulpine.Core.Data.Extentions
{
    /// <summary>
    /// Contains a set of extention methods for the IEnumerable interface. Included are
    /// methods which the LINQ system is curently lacking, as well as some methods which
    /// can be preformed more effecently than in LINQ.
    /// </summary>
    /// <remarks>Last Update: 2016-06-12</remarks>
    public static class VEnumerable
    {
        /// <summary>
        /// Retrieves the element at a specific index in a sequence. This method is
        /// prefered to Enumerable.ElementAt() as it offers the optimal computaiton 
        /// for both the IList and Indexed interfaces.
        /// </summary>
        /// <typeparam name="T">Type of the elements source</typeparam>
        /// <param name="source">The source sequence for the elements</param>
        /// <param name="index">Index of the element to retrieve</param>
        /// <returns>The desired element</returns>
        public static T FindByIndex<T>(this IEnumerable<T> source, Index index)
        {
            if (source == null) throw new ArgumentNullException();

            //atempts to use the Indexed interface
            var listA = source as Indexed<T>;
            if (listA != null) return listA.GetItem(index);

            //failing that, it tries to use the IList interface
            var listB = source as IList<T>;
            if (listB != null)
            {
                int? x = index.GetIndex(listB.Count);
                return (x != null) ? listB[x.Value] : default(T);
            }

            //obtains the actual index
            int x2 = index.GetOriginal();

            if (x2 < 0)
            {
                //reverses the order if searching from the back
                source = source.Reverse();
                x2 = -(x2 + 1);
            }

            //deffers to the default method
            return source.ElementAt(x2);
        }

        /// <summary>
        /// Finds the largest vlaue in a given sequence.
        /// </summary>
        /// <typeparam name="T">Type of the element source</typeparam>
        /// <param name="source">The source sequence for the elements</param>
        /// <returns>The largest value in the sequence</returns>
        public static T MaxValue<T>(this IEnumerable<T> source)
        {
            Func<T, T> sel = x => x;
            return MaxValue(source, sel);
        }

        /// <summary>
        /// Finds the largest value in a given sequence by comparing the
        /// key values returned by a selector function.
        /// </summary>
        /// <typeparam name="T">Type of the element source</typeparam>
        /// <typeparam name="K">Type of the selector key</typeparam>
        /// <param name="source">The source sequence for the elements</param>
        /// <param name="selector">A function to retreve keys from elements</param>
        /// <returns>The value that corispondes to the largest key</returns>
        public static T MaxValue<T, K>(this IEnumerable<T> source,
            Func<T, K> selector)
        {
            //checks for null arguments
            if (source == null || selector == null)
                throw new ArgumentException();

            //uses the default comparison for the keys
            var comp = Comparer<K>.Default;
            T max_val = default(T);
            K max = default(K);

            using (var iter = source.GetEnumerator())
            {
                //checks for an empty sequence
                if (!iter.MoveNext()) return default(T);

                max_val = iter.Current;
                max = selector(max_val);

                //iterates over the sequence
                while (iter.MoveNext())
                {
                    var x = selector(iter.Current);

                    if (comp.Compare(x, max) > 0)
                    {
                        max_val = iter.Current;
                        max = x;
                    }
                }
            }

            //returns the maximum value
            return max_val;
        }

        /// <summary>
        /// Finds the smallest vlaue in a given sequence.
        /// </summary>
        /// <typeparam name="T">Type of the element source</typeparam>
        /// <param name="source">The source sequence for the elements</param>
        /// <returns>The smallest value in the sequence</returns>
        public static T MinValue<T>(this IEnumerable<T> source)
        {
            Func<T, T> sel = x => x;
            return MinValue(source, sel);
        }

        /// <summary>
        /// Finds the smallest value in a given sequence by comparing the
        /// key values returned by a selector function.
        /// </summary>
        /// <typeparam name="T">Type of the element source</typeparam>
        /// <typeparam name="K">Type of the selector key</typeparam>
        /// <param name="source">The source sequence for the elements</param>
        /// <param name="selector">A function to retreve keys from elements</param>
        /// <returns>The value that corispondes to the smallest key</returns>
        public static T MinValue<T, K>(this IEnumerable<T> source,
            Func<T, K> selector)
        {
            //checks for null arguments
            if (source == null || selector == null)
                throw new ArgumentException();

            //uses the default comparison for the keys
            var comp = Comparer<K>.Default;
            T min_val = default(T);
            K min = default(K);

            using (var iter = source.GetEnumerator())
            {
                //checks for an empty sequence
                if (!iter.MoveNext()) return default(T);

                min_val = iter.Current;
                min = selector(min_val);

                //iterates over the sequence
                while (iter.MoveNext())
                {
                    var x = selector(iter.Current);

                    if (comp.Compare(x, min) < 0)
                    {
                        min_val = iter.Current;
                        min = x;
                    }
                }
            }

            //returns the maximum value
            return min_val;
        }

        /// <summary>
        /// Determins if one sequence is the subset of another sequence, that is if
        /// all the elements of the first sequence are also elements of the second
        /// sequence. It returns true if the first sequence is a subset, and false
        /// if otherwise.
        /// </summary>
        /// <typeparam name="T">Type of the element source</typeparam>
        /// <param name="source">Sequence to check for subset status</param>
        /// <param name="other">Sequence to check agenst</param>
        /// <returns>True if the source is a subset of the other sequence, 
        /// false if otherwise</returns>
        public static bool IsSubsetOf<T>(this IEnumerable<T> source,
            IEnumerable<T> other)
        {
            //checks for null arguments
            if (source == null || other == null)
                throw new ArgumentException();

            //start by assuming it is a subset
            bool check = true;

            //look for an item that lies outside the other set
            foreach (var item in source)
            {
                check &= other.Contains(item);
                if (!check) break;
            }

            return check;
        }

        /// <summary>
        /// Determins if the source sequence overlaps a given target sequence, that is
        /// when the intersection of the two sequences is non-empty. It returns true
        /// if the sequences overlap, and false if otherwise.
        /// </summary>
        /// <typeparam name="T">Type of the element source</typeparam>
        /// <param name="source">First sequence</param>
        /// <param name="other">Second sequence</param>
        /// <returns>True if the sequences overlap, false if otherwise</returns>
        public static bool Overlaps<T>(this IEnumerable<T> source,
            IEnumerable<T> other)
        {
            //checks for null arguments
            if (source == null || other == null)
                throw new ArgumentException();

            //start by assuming there is no overlap
            bool check = false;

            //look for an item contained in both sets
            foreach (var item in source)
            {
                check |= other.Contains(item);
                if (check) break;
            }

            return check;
        }


        public static IEnumerable<ICollection<T>> Buffer<T>
            (this IEnumerable<T> source, int size, int step)
        {
            //checks the validity of the arguments
            if (source == null) throw new ArgumentException();
            if (size < 1) throw new ArgumentException();
            if (step < 1) throw new ArgumentException();

            int count = 0;
            var buckets = new DequeLinked<VList<T>>();
            
            //NOTE: This needs work!!

            foreach (var item in source)
            {
                if (count % step == 0)
                {
                    //dequeues the last bucket and returns it
                    var list = buckets.PopFront();
                    if (list != null) yield return list;

                    //creates a new bucket for future items
                    buckets.PushBack(new VListArray<T>(size));
                }

                //adds the item to each active bucket
                foreach (var list in buckets) list.Add(item);

                count++;
            }
        }
        
    }
}
