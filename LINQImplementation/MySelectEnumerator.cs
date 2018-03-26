using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQImplementation
{
    /// <summary>
    /// Class which implements IEnumerator interface For Select.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
    public class MySelectEnumerator<TSource, TResult> : IEnumerator<TResult>
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
        /// Enumerator for source.
        /// </summary>
        private IEnumerator<TSource> enumerator;
        /// <summary>
        /// The current element.
        /// </summary>
        private TSource current;
        /// <summary>
        /// The current position of the element.
        /// </summary>
        private int position;

        /// <summary>
        /// The parameterfull constructor.
        /// </summary>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each source element.</param>
        public MySelectEnumerator(IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            this.source = source;
            this.selector = selector;
            this.enumerator = this.source.GetEnumerator();
            this.current = default(TSource);
            this.position = -1;
        }

        /// <summary>
        /// Get current element. 
        /// </summary>
        public TResult Current
        {
            get
            {
                return this.selector(this.current);
            }
        }

        /// <summary>
        /// Get current object.
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public void Dispose()
        {
        }
        
        /// <summary>
        /// Moves to the next element.
        /// </summary>
        public bool MoveNext()
        {
            this.position++;
            if (this.enumerator.MoveNext())
                this.current = this.enumerator.Current;
            return this.position < this.source.Count();
        }

        /// <summary>
        /// Resets the collection.
        /// </summary>
        public void Reset()
        {
            this.enumerator = this.source.GetEnumerator();
            this.current = default(TSource);
            this.position = -1;
        }
    }
}
