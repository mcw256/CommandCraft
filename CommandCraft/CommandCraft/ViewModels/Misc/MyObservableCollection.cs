using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Grabcraft_Helper.ViewModels.Misc
{
    public delegate void OnCollectionChange(string a);

    public class MyObservableCollection<T> : ICollection<T>
    {
        #region Constructor
        public MyObservableCollection(string instanceName, OnCollectionChange onPropertyChange)
        {
            _items = new List<T>();
            _instanceName = instanceName;
            OnCollectionChange += onPropertyChange;
        }
        #endregion

        #region Properties
        ICollection<T> _items;
        public event OnCollectionChange OnCollectionChange;
        private string _instanceName;

        public int Count => _items.Count;

        public bool IsReadOnly => false;

        #endregion

        #region Methods
        public void Add(T item)
        {
            _items.Add(item);
            OnCollectionChange?.Invoke(_instanceName);
        }

        public void Clear()
        {
            _items.Clear();
            OnCollectionChange?.Invoke(_instanceName);
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        #endregion Methods
    }


}
