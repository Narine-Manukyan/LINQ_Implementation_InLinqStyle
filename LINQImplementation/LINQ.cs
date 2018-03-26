using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQImplementation
{
    /// <summary>
    /// A library which implements some LINQ methods as extension methods in LINQ style.
    /// </summary>
    public static class LINQ
    {
        /// <summary>
        /// Specifies the type and shape of the elements in the returned sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each source element.</param>
        /// <returns>An IEnumerable<T> whose elements are the result of invoking the transform function on each element of source.</returns>
        public static IEnumerable<TResult> ExtensionSelect<TSource, TResult>
            (this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null)
                throw new ArgumentNullException("Null Exception - Source.");
            if (selector == null)
                throw new ArgumentNullException("Null Exception - Selector.");
            return new MySelectEnumerable<TSource, TResult>(source, selector);
        }

        /// <summary>
        ///  Filters elements from a collection based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An IEnumerable<T> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An IEnumerable<T> that contains elements from the input sequence that satisfy the condition.</returns>
        public static IEnumerable<TSource> ExtensionWhere<TSource>
            (this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("Null Exception - Source.");
            if (predicate == null)
                throw new ArgumentNullException("Null Exception - Predicate.");
            return new MyWhereEnumerable<TSource>(source, predicate);
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by the function represented in keySelector.</typeparam>
        /// <param name="source">An IEnumerable<T> whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <returns>A sequence of objects of type TSource and a key.</returns>
        public static IEnumerable<IGrouping<TKey, TSource>> ExtensionGroupBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException("Null Exception - Source.");
            if (keySelector == null)
                throw new ArgumentNullException("Null Exception - Key Selector.");
            // Creating new Dictionary.
            Dictionary<TKey,TSource> dict= ExtensionToDictionary(source, keySelector);
            // For each group.
            foreach (var item in dict)
            {
                // Returning instance of class Grouping.
                yield return new Grouping<TKey, TSource>(item.Key, source);
            }
        }

        /// <summary>
        /// Returns a collection as a List.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The IEnumerable<T> to create a List<T> from.</param>
        /// <returns>A List<T> that contains elements from the input sequence.</returns>
        public static List<TSource> ExtensionToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("Null Exception - Source.");
            // Creating new list.
            List<TSource> list = new List<TSource>();
            foreach (TSource item in source)
            {
                // Adding elements to list.
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// Sorts the elements of a collection in a particular direction (ascending) according to a key. 
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>An IOrderedEnumerable<TElement> whose elements are sorted according to a key.</returns>
        public static IOrderedEnumerable<TSource> ExtensionOrderBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException("Null Exception - Source.");
            if (keySelector == null)
                throw new ArgumentNullException("Null Exception - Key Selector.");
            return new MyOrderByEnumerable<TSource>(source).CreateOrderedEnumerable(keySelector, null, false);
        }

        /// <summary>
        /// Sorts the elements of a collection in a particular direction (descending) according to a key. 
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>An IOrderedEnumerable<TElement> whose elements are sorted according to a key.</returns>
        public static IOrderedEnumerable<TSource> ExtensionOrderByDescending<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException("Null Exception - Source.");
            if (keySelector == null)
                throw new ArgumentNullException("Null Exception - Key Selector.");
            return new MyOrderByEnumerable<TSource>(source).CreateOrderedEnumerable(keySelector, null, true);
        }

        /// <summary>
        /// Converts a collection intoa dictionary according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An IEnumerable<T> to create a Dictionary[TKey, TValue> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <returns>A Dictionary[TKey, TValue> that contains keys and values.</returns>
        public static Dictionary<TKey, TSource> ExtensionToDictionary<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException("Null Exception - Source.");
            if (keySelector == null)
                throw new ArgumentNullException("Null Exception - Key Selector.");
            // Creating new Dictionary.
            Dictionary<TKey,TSource> dict = new Dictionary<TKey, TSource>();
            foreach (TSource item in source)
            {
                // Key for element.
                TKey key = keySelector(item);
                // Add element if key doesn't exist.
                if(!dict.Keys.Contains(key))
                    dict.Add(key, item);
            }
            return dict;
        }
    }
}
