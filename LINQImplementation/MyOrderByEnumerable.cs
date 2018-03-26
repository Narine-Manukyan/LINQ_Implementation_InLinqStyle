using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQImplementation
{
    /// <summary>
    /// Class which implements IEnumerable interface.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    public class MyOrderByEnumerable<TSource> : IOrderedEnumerable<TSource>
    {
        /// <summary>
        /// A sequence of values to invoke a transform function on.
        /// </summary>
        internal IEnumerable<TSource> source;

        /// <summary>
        /// The parameterfull constructor.
        /// </summary>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        public MyOrderByEnumerable(IEnumerable<TSource> source)
        {
            this.source = source;
        }

        /// <summary>
        /// Performs a subsequent ordering on the elements of an IOrderedEnumerable<TElement> according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key produced by keySelector.</typeparam>
        /// <param name="keySelector">The Func[T, TResult> used to extract the key for each element.</param>
        /// <param name="comparer">The IComparer<T> used to compare keys for placement in the returned sequence.</param>
        /// <param name="descending">true to sort the elements in descending order; false to sort the elements in ascending order.</param>
        /// <returns>An IOrderedEnumerable<TElement> whose elements are sorted according to a key.</returns>
        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer, bool descending)
        {
            // Creating List from source.
            List<TSource> list = new List<TSource>(source);
            if (descending == true)
                // Returning sorted list.
                return list.OrderByDescending(keySelector);
            // Returning sorted list in descending order.
            return list.OrderBy(keySelector);
        }

        /// <summary>
        /// Implementing the Get Enumerator for MyEnumerable.
        /// </summary>
        public IEnumerator<TSource> GetEnumerator()
        {
            // Creating List from source.
            List<TSource> list = new List<TSource>(source);
            return list.GetEnumerator();
        }

        /// <summary>
        /// Implementing the IEnumerable.GetEnumerator().
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
