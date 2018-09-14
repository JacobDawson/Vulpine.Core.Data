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
using System.Collections;
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
    /// <remarks>Last Update: 2016-08-29</remarks>
    public static class VEnumerable
    {
        #region Element Access...

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

        #endregion /////////////////////////////////////////////////////////////////

        #region Set Opperations...

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

        #endregion /////////////////////////////////////////////////////////////////

        #region Statistics...

        /// <summary>
        /// Finds the median value in the source data. The median is defined to
        /// be the 0.5 percentile.
        /// </summary>
        /// <param name="source">The source of the data</param>
        /// <returns>The median value</returns>
        public static double Median(this IEnumerable<Double> source)
        {
            //calls on the method below
            return source.Percentile(0.5);
        }

        /// <summary>
        /// Finds the median value in the sorce data by first converting it to a
        /// floating point format. The median is defined to be the 0.5 percentile.
        /// </summary>
        /// <typeparam name="T">Type of the element source</typeparam>
        /// <param name="source">The source of the data</param>
        /// <param name="selector">Method to convert the sequence</param>
        /// <returns>The median floating-point value</returns>
        public static double Median<T>
            (this IEnumerable<T> source, Func<T, Double> selector)
        {
            //composes the less generic method with selection
            return source.Select(selector).Percentile(0.5);
        }

        /// <summary>
        /// Finds the generic median in the source data. The median is defined to
        /// be the 0.5 percentile.
        /// </summary>
        /// <typeparam name="T">Type of the element source</typeparam>
        /// <param name="source">The source of the data</param>
        /// <returns>The generic median</returns>
        public static T Median<T>(this IEnumerable<T> source)
        {
            //calls on the method below
            return source.Percentile(0.5);
        }

        /// <summary>
        /// Finds the desired percentile value in the source data. The Nth percentile is
        /// defind to be the minimum value greater than N percent of the data.
        /// </summary>
        /// <param name="source">The source of the data</param>
        /// <param name="per">The desired percentile, between 0.0 and 1.0</param>
        /// <returns>The desired percentile value</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the percentage is outside
        /// the range of zero to one</exception>
        public static double Percentile(this IEnumerable<Double> source, double per)
        {
            //checks for exceptions
            if (source == null) throw new ArgumentNullException("source");
            if (per < 0.0 || per >= 1.0) throw new ArgumentOutOfRangeException("per");

            //calculates the index and the rate
            double[] sorted = source.ToArray().ArraySort();
            double rate = (double)(sorted.Length - 1) * per;
            int index = (int)Math.Floor(rate);

            //obtains the brackiting set
            double v1 = sorted[index];
            double v2 = sorted[index + 1];
            double t = rate - (double)index;

            //preforms liniar interpolation
            return ((1.0 - t) * v1) + (t * v2);

        }

        /// <summary>
        /// Finds the desired persentile value in the sorce data, by first converting the
        /// data to a floating-point format. The Nth percentile is defind to be the minimum 
        /// value greater than N percent of the data.
        /// </summary>
        /// <typeparam name="T">Type of the element source</typeparam>
        /// <param name="source">The source of the data</param>
        /// <param name="selector">Method to convert the sequence</param>
        /// <param name="per">The desired percentile, between 0.0 and 1.0</param>
        /// <returns>The desired percentile value</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the percentage is outside
        /// the range of zero to one</exception>
        public static double Percentile<T>
            (this IEnumerable<T> source, Func<T, Double> selector, double per)
        {
            //composes the less generic method with selection
            return source.Select(selector).Percentile(per);
        }

        /// <summary>
        /// Finds the desired generic persentile in the sorce data. The Nth percentile 
        /// is defind to be the minimum value greater than N percent of the data.
        /// </summary>
        /// <typeparam name="T">Type of the element source</typeparam>
        /// <param name="source">The source of the data</param>
        /// <param name="per">The desired percentile, between 0.0 and 1.0</param>
        /// <returns>The desired generic persentile</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the percentage is outside
        /// the range of zero to one</exception>
        public static T Percentile<T>(this IEnumerable<T> source, double per)
        {
            //checks for exceptions
            if (source == null) throw new ArgumentNullException("source");
            if (per < 0.0 || per > 1.0) throw new ArgumentOutOfRangeException("per");

            //calculates the index
            var sorted = source.OrderBy(x => x);
            double rate = (double)source.Count() - 1.0;
            int index = (int)Math.Ceiling(per * rate);

            //returns the quantile
            return sorted.ElementAt(index);
        }

        /// <summary>
        /// Divides the data source into uniform quantile regions, returning the boundaries
        /// between each region. This includes the minimum and maximum values. For instance,
        /// a count of four would result in the second and third quartiles, the median, and 
        /// the min and max, listed in order.
        /// </summary>
        /// <param name="source">The source of the data</param>
        /// <param name="n">Number of quantile regions</param>
        /// <returns>The boundaries between quantiles</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the number of regions is 
        /// less than one</exception>
        public static IEnumerable<Double> Quantile(this IEnumerable<Double> source, int n)
        {
            //checks for exceptions
            if (source == null) throw new ArgumentNullException("source");
            if (n < 1) throw new ArgumentOutOfRangeException("step");

            //calculates the step size
            double[] sorted = source.ToArray().ArraySort();
            double step = (sorted.Length - 1.0) / (double)n;

            //returturns the minimum
            yield return sorted[0];

            //calculates the inner quartiles
            for (int i = 1; i < n; i++)
            {
                //obtains the brackiting set
                int index = (int)Math.Floor(i * step);
                double v1 = sorted[index];
                double v2 = sorted[index + 1];
                double t = (i * step) - (double)index;

                //preforms liniar interpolation
                yield return((1.0 - t) * v1) + (t * v2);
            }

            //returns the maximum
            yield return sorted[sorted.Length - 1];
        }

        /// <summary>
        /// Divides the data source into uniform quantile regions, returning the boundaries
        /// between each region. This includes the minimum and maximum values. For instance,
        /// a count of four would result in the second and third quartiles, the median, and 
        /// the min and max, listed in order.
        /// </summary>
        /// <typeparam name="T">Type of the element source</typeparam>
        /// <param name="source">The source of the data</param>
        /// <param name="selector">Method to convert the sequence</param>
        /// <param name="n">Number of quantile regions</param>
        /// <returns>The boundaries between quantiles</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the number of regions is 
        /// less than one</exception>
        public static IEnumerable<Double> Quantile<T>
            (this IEnumerable<T> source, Func<T, Double> selector, int n)
        {
            //composes the less generic method with selection
            return source.Select(selector).Quantile(n);
        }

        /// <summary>
        /// Helper method that uses a modified version of selection sort
        /// to sort an array of floating point values in-place.
        /// </summary>
        /// <param name="set">Values to sort</param>
        /// <returns>The array after sorting</returns>
        private static double[] ArraySort(this double[] set)
        {
            //sets up values
            double temp = 0.0;
            int min = 0;
            int n = set.Length;

            //preformes the sorting
            for (int index = 0; index < n - 1; index++)
            {
                min = index;
                for (int scan = index + 1; scan < n; scan++)
                {
                    if (set[scan] < set[min])
                        min = scan;
                }

                //swaps the values
                temp = set[min];
                set[min] = set[index];
                set[index] = temp;
            }

            return set;
        }

        #endregion /////////////////////////////////////////////////////////////////









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


        private static IEnumerable<String> FormatRecursive
            (this IEnumerable source, string format, int level)
        {
            //we do not want to keep recursing forever
            if (level > 10) yield break;

            foreach (var item in source)
            {
                var formatable = item as IFormattable;
                var iterator = item as IEnumerable;

                if (formatable != null)
                {
                    //uses the provided format string
                    yield return formatable.ToString(format, null);
                }
                else
                {
                    //uses the default ToString() method
                    yield return item.ToString();
                }

                if (iterator != null)
                {
                    //receusivly iterates over each sub-level
                    var nl = iterator.FormatRecursive(format, level + 1);
                    foreach (string sub in nl)
                        yield return sub.PadLeft(level * 4);
                }
            }
        }


        
        
    }
}
