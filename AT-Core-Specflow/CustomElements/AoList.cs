using System;
using System.Collections;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AT_Core_Specflow.CustomElements
{
    public class AoList<T> : IList<T>
    {
        public AoList(IElementLocator locator, IEnumerable<By> bys, bool cache)
        {
            _locator = locator;
            Bys = bys;
            CacheLookup = cache;
        }
        protected readonly IEnumerable<By> Bys;
        protected bool CacheLookup;
        protected string Title { get; set; }
        private readonly IElementLocator _locator;
        private List<IWebElement> _collectionWebElements;

        private List<IWebElement> CollectionWebElements

        {
            get
            {
                if (CacheLookup && _collectionWebElements != null) return _collectionWebElements;
                _collectionWebElements = new List<IWebElement>();
                _collectionWebElements.AddRange(_locator.LocateElements(Bys));

                return _collectionWebElements;
            }
        }

        private List<T> _list;

        private List<T> List
        {
            get
            {
                if (CacheLookup || _list != null) return _list;
                _list = new List<T>();
                CollectionWebElements.ForEach(element =>
                {
                    var wrapperElement = Activator.CreateInstance(typeof(T), _locator, Bys, CacheLookup, string.Empty);
                    _list.Add((T)wrapperElement);
                });
                return _list;

            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }
        

        public void Add(T item)
        {
            List.Add(item);
        }

        public void Clear()
        {
            List.Clear();
        }

        public bool Contains(T item)
        {
            return List.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            List.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return List.Remove(item);
        }

        public int Count => List.Count;

        public bool IsReadOnly => false;

        public int IndexOf(T item)
        {
            return List.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            List.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            List.RemoveAt(index);
        }

        public T this[int index]
        {
            get => List[index];
            set => throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
