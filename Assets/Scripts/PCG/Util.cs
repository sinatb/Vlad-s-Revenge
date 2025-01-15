using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PCG
{
    public static class Util
    {
        public static int[,] GenerateNoiseGrid(int width, int height, int density)
        {
            var grid = new int[height, width];
            for (var i = 0; i < height; i++)
            {
                for (var j=0; j<width; j++)
                {
                    grid[i, j] = Random.Range(0, 100) > density ? 0 : 1;
                }
            }
            return grid;            
        }

        public static int[,] ApplyRules(int[,] grid, int count)
        {
            var updatedGrid = (int[,])grid.Clone();
            
            for (var i = 0; i < count; i++)
            {
                var tmpGrid = new int[grid.GetLength(0), grid.GetLength(1)];
                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    for (var k = 0; k < grid.GetLength(0); k++)
                    {
                        var neighboursCount = CountNeighbours(k,j,updatedGrid);
                        tmpGrid[k, j] = neighboursCount>4 ? 1 : 0;
                    }
                }
                updatedGrid = tmpGrid;
            }
            return updatedGrid;
        }

        public static GameObject GetRandomPrefab(List<GameObject> objects)
        {
            return objects[Random.Range(0, objects.Count)];
        }
        private static int CountNeighbours(int x, int y, int[,] grid)
        {
            var width = grid.GetLength(0); 
            var height = grid.GetLength(1);
            var count = 0;

            for (var dy = -1; dy <= 1; dy++)
            {
                for (var dx = -1; dx <= 1; dx++)
                {
                    if (dx == 0 && dy == 0) continue; 
                    
                    var nx = x + dx;
                    var ny = y + dy;

                    if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                    {
                        count += grid[nx, ny];
                    }
                }
            }

            return count;
        }
        
        public static void PrintGrid(int[,] grid)
        {
            var str = new StringBuilder();
            for (var i = 0; i < grid.GetLength(1); i++)
            {
                for (var j=0; j <grid.GetLength(0); j++)
                {
                    str.Append(grid[i, j] + " ");
                }

                str.Append("\n");
            }
            Debug.Log(str);
        }
        
    }
}