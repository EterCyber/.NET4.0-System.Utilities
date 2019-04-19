namespace System.Utilities.Base
{
    using System.Utilities.Common;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;

    public class ThreadSafeList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private readonly List<T> _inner;
        private readonly object _lock;

        public ThreadSafeList()
        {
            this._lock = new object();
            this._inner = new List<T>();
        }

        public ThreadSafeList(IEnumerable<T> self)
        {
            this._lock = new object();
            this._inner = self.ConvertToList<T>();
        }

        public void Add(T item)
        {
            lock (this._lock)
            {
                this._inner.Add(item);
            }
        }

        public void Add(T t, Predicate<T> match)
        {
            if (match != null)
            {
                T item = this.Find(match);
                if (item != null)
                {
                    this.Remove(item);
                }
                this.Add(t);
            }
        }

        public void AddUniqueTF(IEnumerable<T> items, IComparer<T> comparaer)
        {
            lock (this._lock)
            {
                this._inner.Sort(comparaer);
                foreach (T local in items)
                {
                    if (this._inner.BinarySearch(local, comparaer) < 0)
                    {
                        this._inner.Add(local);
                    }
                }
            }
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            lock (this._lock)
            {
                return new ReadOnlyCollection<T>(this);
            }
        }

        public void Clear()
        {
            lock (this._lock)
            {
                this._inner.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (this._lock)
            {
                return this._inner.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (this._lock)
            {
                this._inner.CopyTo(array, arrayIndex);
            }
        }

        public bool Exists(Predicate<T> match)
        {
            if (match != null)
            {
                lock (this._lock)
                {
                    foreach (T local in this._inner)
                    {
                        if (match(local))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public T Find(Predicate<T> match)
        {
            if (match != null)
            {
                lock (this._lock)
                {
                    return this._inner.Find(match);
                }
            }
            return default(T);
        }

        public List<T> FindAll(Predicate<T> match)
        {
            if (match != null)
            {
                lock (this._lock)
                {
                    return this._inner.FindAll(match);
                }
            }
            return null;
        }

        public void ForEach(Action<T> action)
        {
            if (action != null)
            {
                lock (this._lock)
                {
                    foreach (T local in this._inner)
                    {
                        action(local);
                    }
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            lock (this._lock)
            {
                return new ThreadSafeEnumerator<T>(this._inner.GetEnumerator(), this._lock);
            }
        }

        public int IndexOf(T item)
        {
            lock (this._lock)
            {
                return this._inner.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (this._lock)
            {
                this._inner.Insert(index, item);
            }
        }

        public bool Remove(T item)
        {
            lock (this._lock)
            {
                return this._inner.Remove(item);
            }
        }

        public void RemoveAll(Predicate<T> match)
        {
            if (match != null)
            {
                lock (this._lock)
                {
                    this._inner.RemoveAll(match);
                }
            }
        }

        public void RemoveAt(int index)
        {
            lock (this._lock)
            {
                this._inner.RemoveAt(index);
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            lock (this._lock)
            {
                return new ThreadSafeEnumerator<T>(this._inner.GetEnumerator(), this._lock);
            }
        }

        public void TrimExcess()
        {
            lock (this._lock)
            {
                this._inner.TrimExcess();
            }
        }

        public int Count
        {
            get
            {
                lock (this._lock)
                {
                    return this._inner.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public T this[int index]
        {
            get
            {
                lock (this._lock)
                {
                    return this._inner[index];
                }
            }
            set
            {
                lock (this._lock)
                {
                    this._inner[index] = value;
                }
            }
        }
    }
}

