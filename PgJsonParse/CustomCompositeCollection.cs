using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonParse
{
    public class CustomCompositeCollection : IList
    {
        public CustomCompositeCollection()
        {
        }

        public bool IsReadOnly { get { return false; } }
        public bool IsFixedSize { get { return false; } }

        public int Add(object value)
        {
            ObjectList.Add(value);
            return ObjectList.Count;
        }

        public void Clear()
        {
            ObjectList.Clear();
        }

        public bool Contains(object value)
        {
            return ObjectList.Contains(value);
        }

        public int IndexOf(object value)
        {
            return ObjectList.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            ObjectList.Insert(index, value);
        }

        public void Remove(object value)
        {
            ObjectList.Remove(value);
        }

        public void RemoveAt(int index)
        {
            ObjectList.RemoveAt(index);
        }

        public int Count { get { return ObjectList.Count; } }
        public object SyncRoot { get { return this; } }
        public bool IsSynchronized { get { return false; } }

        public object this[int i]
        {
            get { return ObjectList[i]; }
            set
            {
                ObjectList[i] = value;
            }
        }

        private List<object> ObjectList = new List<object>();

        public void CopyTo(Array array, int index)
        {

        }

        public IEnumerator GetEnumerator()
        {
            return ObjectList.GetEnumerator();
        }
    }
}