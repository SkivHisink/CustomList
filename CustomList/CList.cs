using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomList
{
    class CList<T> : IEnumerable<T>
    {
        private int LIST_MAX_SIZE = 102400;
        private List<List<T>> data;
        private int data_position = -1;
        private int list_position = -1;
        public long Count
        {
            get => (long)(1 + data_position) * (long)(1 + list_position);
        }
        public CList(List<List<T>> data_)
        {
            data = data_;
        }

        public CList()
        {
            data = new List<List<T>>();
        }

        public void Add(T elem)
        {
            if (data_position == -1 || list_position == LIST_MAX_SIZE - 1)
            {
                data.Add(new List<T>());
                ++data_position;
                list_position = -1;
            }
            data[data_position].Add(elem);
            ++list_position;
        }

        public T this[long i]
        {
            get
            {
                try
                {
                    int data_pos = (int)(i / LIST_MAX_SIZE);
                    int list_pos = (int)(i % LIST_MAX_SIZE);
                    return data[data_pos][list_pos];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
            set
            {
                try
                {
                    int data_pos = (int)(i / LIST_MAX_SIZE);
                    int list_pos = (int)(i % LIST_MAX_SIZE);
                    data[data_pos][list_pos] = value;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Clear()
        {
            foreach (var list in data)
            {
                list.Clear();
            }
            list_position = -1;
            data_position = -1;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; ++i)
            {
                yield return this[i];
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
