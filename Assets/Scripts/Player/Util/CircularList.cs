using System;
using System.Collections.Generic;
using System.Linq;

namespace Player.Util
{
    public class CircularList<T>
    {
        private readonly List<T> _elements;
        private readonly int     _capacity;
        private int              _index;

        public CircularList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentException("Capacity cannot be negative", nameof(capacity));
            _capacity = capacity;
            _elements = new List<T>(capacity);
            _index = 0;
        }

        public void Add(T element)
        {
            if (_elements.Count < _capacity)
            {
                _elements.Add(element);
            }
            else
            {
                _elements[_index] = element;
                _index = (_index + 1) % _capacity;
            }
        }

        public int CountEquals(T element)
        {
            return _elements.Count(e => e.Equals(element));
        }
        
    }
}
