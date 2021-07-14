using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CustomList
{
    class CList<T> 
    {
        private int LIST_MAX_SIZE = 102400;
        private List<List<T>> data;
        private int data_position = -1;
        private int list_position = -1;
        
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


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
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
