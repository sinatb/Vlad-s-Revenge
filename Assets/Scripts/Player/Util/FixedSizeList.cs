using System;
using System.Collections.Generic;
using System.Linq;

namespace Player.Util
{
    public class FixedSizeList<T>
    {
        private readonly int _maxSize;
        private readonly LinkedList<T> _list;

        public FixedSizeList(int maxSize)
        {
            if (maxSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxSize), "Size must be greater than 0.");
        
            _maxSize = maxSize;
            _list = new LinkedList<T>();
        }

        public void Add(T item)
        {
            if (_list.Count >= _maxSize)
            {
                _list.RemoveLast(); 
            }
            _list.AddFirst(item);
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _list.Count)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
            
                var node = _list.First;
                for (int i = 0; i < index; i++)
                {
                    node = node?.Next;
                }
                return node.Value;
            }
        }

        public int Count => _list.Count;
        
    }
}
