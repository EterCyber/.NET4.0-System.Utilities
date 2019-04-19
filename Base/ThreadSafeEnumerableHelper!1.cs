namespace System.Utilities.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ThreadSafeEnumerableHelper<T> : IEnumerable<T>, IEnumerable
    {
        private readonly IEnumerable<T> innerEnumerable;
        private readonly object syncObject;

        public ThreadSafeEnumerableHelper(IEnumerable<T> inner, object syncObj)
        {
            this.syncObject = syncObj;
            this.innerEnumerable = inner;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ThreadSafeEnumerator<T>(this.innerEnumerable.GetEnumerator(), this.syncObject);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

