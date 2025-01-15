using System.Collections.Generic;
using UnityEngine;

namespace PCG
{
    public class Generator : MonoBehaviour
    {
        private List<Room>     _rooms = new List<Room>();
        public GeneratorParams _gp;
        public void SetParams(GeneratorParams gp)
        {
            _gp = gp;
        }

        private void Start()
        {
            GenerateRoom();
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
                        var prefab = Util.GetRandomPrefab(_gp.floorPrefab);
                        Instantiate(prefab,
                            new Vector3(j - (float)width/2, i - (float)width/2, 0),
                            Quaternion.identity);
                    }
                    else
                    {
                        var prefab = Util.GetRandomPrefab(_gp.wallPrefab);
                        Instantiate(prefab, 
                            new Vector3(j - (float)width/2, i - (float)width/2, 0),
                            Quaternion.identity);
                    }
                }
            }
        }
        public void GenerateRoom()
        {
            var grid = Util.GenerateNoiseGrid(_gp.roomWidth, _gp.roomHeight, _gp.roomDensity);
            grid = Util.ApplyRules(grid, _gp.ruleIterations);
            VisualizeGrid(grid);
        }
        
    }
}