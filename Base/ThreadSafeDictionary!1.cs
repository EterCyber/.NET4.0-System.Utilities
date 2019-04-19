namespace System.Utilities.Base
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;

    public class ThreadSafeDictionary<T>
    {
        private Dictionary<string, T> dic;
        private static int readerTimeout;
        private ReaderWriterLock rwlock;
        private static int writerTimeout;

        static ThreadSafeDictionary()
        {
            ThreadSafeDictionary<T>.readerTimeout = 0x3e8;
            ThreadSafeDictionary<T>.writerTimeout = 0x3e8;
        }

        public ThreadSafeDictionary()
        {
            this.rwlock = new ReaderWriterLock();
            this.dic = new Dictionary<string, T>();
        }

        public ThreadSafeDictionary(int _readerTimeout, int _writerTimeout)
        {
            this.rwlock = new ReaderWriterLock();
            this.dic = new Dictionary<string, T>();
            ThreadSafeDictionary<T>.readerTimeout = _readerTimeout;
            ThreadSafeDictionary<T>.writerTimeout = _writerTimeout;
        }

        public void Add(string key, T val)
        {
            this.Add(key, val, ThreadSafeDictionary<T>.writerTimeout);
        }

        public void Add(string key, T val, int timeout)
        {
            this.rwlock.AcquireWriterLock(timeout);
            try
            {
                this.dic[key] = val;
            }
            finally
            {
                this.rwlock.ReleaseWriterLock();
            }
        }

        public void Clear()
        {
            this.Clear(ThreadSafeDictionary<T>.writerTimeout);
        }

        public void Clear(int timeout)
        {
            this.rwlock.AcquireWriterLock(timeout);
            try
            {
                this.dic.Clear();
            }
            finally
            {
                this.rwlock.ReleaseWriterLock();
            }
        }

        public bool ContainsKey(string key)
        {
            return this.ContainsKey(key, ThreadSafeDictionary<T>.readerTimeout);
        }

        public bool ContainsKey(string key, int timeout)
        {
            bool flag;
            this.rwlock.AcquireReaderLock(timeout);
            try
            {
                flag = this.dic.ContainsKey(key);
            }
            finally
            {
                this.rwlock.ReleaseReaderLock();
            }
            return flag;
        }

        public int Count()
        {
            return this.Count(ThreadSafeDictionary<T>.readerTimeout);
        }

        public int Count(int timeout)
        {
            int count;
            this.rwlock.AcquireReaderLock(timeout);
            try
            {
                count = this.dic.Count;
            }
            finally
            {
                this.rwlock.ReleaseReaderLock();
            }
            return count;
        }

        public T Get(string key)
        {
            return this.Get(key, ThreadSafeDictionary<T>.readerTimeout);
        }

        public T Get(string key, int timeout)
        {
            T local2;
            this.rwlock.AcquireReaderLock(timeout);
            try
            {
                T local;
                this.dic.TryGetValue(key, out local);
                local2 = local;
            }
            finally
            {
                this.rwlock.ReleaseReaderLock();
            }
            return local2;
        }

        public void Remove(string key)
        {
            this.Remove(key, ThreadSafeDictionary<T>.writerTimeout);
        }

        public void Remove(string key, int timeout)
        {
            this.rwlock.AcquireWriterLock(timeout);
            try
            {
                this.dic.Remove(key);
            }
            finally
            {
                this.rwlock.ReleaseWriterLock();
            }
        }

        public T this[string key]
        {
            get
            {
                T local;
                this.rwlock.AcquireReaderLock(ThreadSafeDictionary<T>.readerTimeout);
                try
                {
                    local = this.dic[key];
                }
                finally
                {
                    this.rwlock.ReleaseReaderLock();
                }
                return local;
            }
            set
            {
                this.rwlock.AcquireWriterLock(ThreadSafeDictionary<T>.writerTimeout);
                try
                {
                    this.dic[key] = value;
                }
                finally
                {
                    this.rwlock.ReleaseWriterLock();
                }
            }
        }
    }
}

