using UnityEngine;

namespace PCG
{
    public class Room : MonoBehaviour
    {
        public int[,]         Grid { get; private set; }
        private PcgStyle      _style;      
        private bool          _isActive;

        public void Init(int[,] grid, PcgStyle style)
        {
            Grid = (int[,]) grid.Clone();
        }
        private void VisualizeGrid(int[,] grid)
        {
            var width = grid.GetLength(0);
            var height = grid.GetLength(1);
            for (var i=0; i < height; i++)
            {
                for (var j=0; j < width; j++)
                {
                    if (grid[i, j] == 1)
                    {
                        var prefab = Util.GetRandomItem(_style.floorPrefab);
                        Instantiate(prefab,
                            new Vector3(j - (float)width/2, i - (float)width/2, 0),
                            Quaternion.identity);
                    }
                    else
                    {
                        var prefab = Util.GetRandomItem(_style.wallPrefab);
                        Instantiate(prefab, 
                            new Vector3(j - (float)width/2, i - (float)width/2, 0),
                            Quaternion.identity);
                    }
                }
            }
        }
        
    }
}