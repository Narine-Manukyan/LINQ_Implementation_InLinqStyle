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
    /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
    public class MySelectEnumerable<TSource, TResult> : IEnumerable<TResult>
    {
        /// <summary>
        /// A sequence of values to invoke a transform function on.
        /// </summary>
        private readonly IEnumerable<TSource> source;

        /// <summary>
        /// A transform function to apply to each source element.(For Select)
        /// </summary>
        private readonly Func<TSource, TResult> selector;

        /// <summary>
        /// The parameterfull constructor.
        /// </summary>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each source element.</param>
        public MySelectEnumerable(IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
        }

        /// <summary>
        /// Implementing the Get Enumerator for MyEnumerable.
        /// </summary>
        public IEnumerator<TResult> GetEnumerator()
        {
            return new MySelectEnumerator<TSource, TResult>(this.source, this.selector);
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
