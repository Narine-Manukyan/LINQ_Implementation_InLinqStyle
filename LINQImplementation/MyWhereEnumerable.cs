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
    public class MyWhereEnumerable<TSource> : IEnumerable<TSource>
    {
        /// <summary>
        /// A sequence of values to invoke a transform function on.
        /// </summary>
        private readonly IEnumerable<TSource> source;

        /// <summary>
        /// A transform function to apply to each source element.(For Where)
        /// </summary>
        private readonly Func<TSource, bool> predicate;

        /// <summary>
        /// The parameterfull constructor.
        /// </summary>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="predicate">A transform function to apply to each source element.</param>
        public MyWhereEnumerable(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
        }

        /// <summary>
        /// Implementing the Get Enumerator for MyEnumerable.
        /// </summary>
        public IEnumerator<TSource> GetEnumerator()
        {
            return new MyWhereEnumerator<TSource>(this.source, this.predicate);
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
