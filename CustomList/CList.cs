using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CustomList
{
    [DebuggerDisplay("Count = {Count}")]
    public class CList<T> : IEnumerable<T>
    {
        private readonly int LIST_MAX_SIZE = 8000000;
        private readonly List<List<T>> _data;
        private int _dataPosition = -1;
        private int _listPosition = -1;
        public long Count
        {
            get
            {
                return (_dataPosition >= 0) ? ((long)_dataPosition * LIST_MAX_SIZE) + (1 + _listPosition) : -1;
            }
        }

        public CList(List<List<T>> data)
        {
            _data = data;
        }

        public CList(T[] arr)
        {
            Add(arr);
        }

        public CList()
        {
            _data = new List<List<T>>();
        }

        public void Add(T elem)
        {
            if (_dataPosition == -1 || _listPosition == LIST_MAX_SIZE - 1)
            {
                _data.Add(new List<T>());
                ++_dataPosition;
                _listPosition = -1;
            }
            _data[_dataPosition].Add(elem);
            ++_listPosition;
        }
        //
        public void Add(T[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                Add(arr[i]);
            }
        }

        public void Add(List<T> list)
        {
            foreach (var elem in list)
            {
                Add(elem);
            }
        }

        public T this[long i]
        {
            get
            {
                try
                {
                    int data_pos = (int)(i / LIST_MAX_SIZE);
                    int list_pos = (int)(i % LIST_MAX_SIZE);
                    return _data[data_pos][list_pos];
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
                    _data[data_pos][list_pos] = value;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Clear()
        {
            foreach (var list in _data)
            {
                list.Clear();
            }
            _data.Clear();
            _listPosition = -1;
            _dataPosition = -1;
        }

        internal virtual long IndexOf(List<List<T>> array, T value, long startIndex, long count)
        {
            long num = startIndex + count;
            for (long index = startIndex; index < num; ++index)
            {
                if (Equals(array[(int)(index / LIST_MAX_SIZE)][(int)(index % LIST_MAX_SIZE)], value))
                {
                    return index;
                }
            }
            return -1;
        }

        public long IndexOf(T item)
        {
            return _data == null
                ? throw new ArgumentNullException(nameof(_data))
                : 0 > Count
                ? throw new ArgumentOutOfRangeException(nameof(Count), "ArgumentOutOfRange_Index")
                : Count < 0
                    ? throw new ArgumentOutOfRangeException(nameof(Count), "ArgumentOutOfRange_Count")
                    : IndexOf(_data, item, 0, Count);
        }

        //public bool Remove(T item)
        //{
        //    throw new NotImplementedException();
        //}

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

        public T[] ToArray()
        {
            var destinationArray = new T[Count];
            Array.Copy(_data[0].ToArray(), 0, destinationArray, 0, _data[0].Count);
            for (int i = 1; i < _data.Count; ++i)
            {
                Array.Copy(_data[i].ToArray(), 0, destinationArray, i * _data[i - 1].Count, _data[i].Count);
            }
            return destinationArray;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            long index = this.IndexOf(item);
            if (index < 0)
                return false;
            this.RemoveAt(index);
            return true;
        }

        public void RemoveAt(long index)
        {
            if (index >= this.Count)
            {
                throw new ArgumentException("Index is higher than number of elements");
            }

            int real_index_1 = (int)(index / LIST_MAX_SIZE);
            int real_index_2 = (int)(index % LIST_MAX_SIZE);
            this._data[real_index_1].RemoveAt(real_index_2);
            if (this._data[real_index_1].Count == 0)
            {
                this._data.RemoveAt(real_index_1);
            }
        }
    }
}
