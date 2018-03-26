using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQImplementation
{
    /// <summary>
    /// Class for implementation of IGrouping...for using in ExtendedGroupBy Method in Linq class.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    public class Grouping<TKey, TSource> : IGrouping<TKey, TSource>
    {
        // Key for Key-Value pair.
        private TKey key;
        // IEnumerable collection of values.
        private IEnumerable<TSource> values;
        // Property for key;
        public TKey Key
        {
            get { return this.key; }
        }
        /// <summary>
        /// Parameterfull constructor.
        /// </summary>
        public Grouping(TKey key, IEnumerable<TSource> values)
        {
            this.key = key;
            this.values = values;
        }

        /// <summary>
        /// Implementing GetEnumerator().
        /// </summary>
        public IEnumerator<TSource> GetEnumerator()
        {
            return this.values.GetEnumerator();
        }

        /// <summary>
        /// Implementing IEnumerable.GetEnumerator().
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
