using System.Collections.Generic;
using UnityEngine;

namespace PCG
{
    public class Room : MonoBehaviour
    {
        private Vector2Int       _centerPosition;
        private int              _width;
        private int              _height;
        private List<GameObject> _elements;

        public void Init(Vector2Int centerPosition, int width, int height)
        {
            _centerPosition = centerPosition;
            _width = width;
            _height = height;
        }
    }
}