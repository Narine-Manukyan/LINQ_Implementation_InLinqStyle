using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQImplementation
{
    /// <summary>
    /// Class which implements IEnumerator interface For Where.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    public class MyWhereEnumerator<TSource> : IEnumerator<TSource>
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
        /// <param name="predicate">A transform function to apply to each source element.</param>
        public MyWhereEnumerator(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            this.source = source;
            this.predicate = predicate;
            this.enumerator = this.source.GetEnumerator();
            this.current = default(TSource);
            this.position = -1;
        }

        /// <summary>
        /// Get current element. 
        /// </summary>
        public TSource Current
        {
            get
            {
                return this.current;
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
            while (this.enumerator.MoveNext())
            {
                if (this.predicate(this.enumerator.Current))
                {
                    this.current = this.enumerator.Current;
                    return true;
                }
            }
            return false;
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
