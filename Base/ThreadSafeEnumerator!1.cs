namespace System.Utilities.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;

    public class ThreadSafeEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
    {
        private readonly IEnumerator<T> innerEnumerator;
        private readonly object syncObject;

        public ThreadSafeEnumerator(IEnumerator<T> inner, object syncObj)
        {
            this.innerEnumerator = inner;
            this.syncObject = syncObj;
            Monitor.Enter(this.syncObject);
        }

        public void Dispose()
        {
            Monitor.Exit(this.syncObject);
        }

        public bool MoveNext()
        {
            return this.innerEnumerator.MoveNext();
        }

        public void Reset()
        {
            this.innerEnumerator.Reset();
        }

        public T Current
        {
            get
            {
                return this.innerEnumerator.Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }
    }
}

