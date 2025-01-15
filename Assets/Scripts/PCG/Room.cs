using Difficulty;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PCG
{
    public class Room : MonoBehaviour
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public int[,]              Grid { get; private set; }
        private PcgStyle           _style;      
        private bool               _isActive;
        private DifficultySettings _difficulty;

        public void Init(int[,] grid, PcgStyle style, DifficultySettings difficulty)
        {
            Grid = (int[,]) grid.Clone();
            _style = style;
            _difficulty = difficulty;
        }
        private void VisualizeGrid()
        {
            var width = Grid.GetLength(0);
            var height = Grid.GetLength(1);
            for (var i=0; i < height; i++)
            {
                for (var j=0; j < width; j++)
                {
                    if (Grid[i, j] == 1)
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

        private Vector3 GetRandomFloor()
        {
            var x = Random.Range(0,Grid.GetLength(0));
            var y = Random.Range(0,Grid.GetLength(1));
            while (Grid[y, x] != 1)
            {
                y = Random.Range(0,Grid.GetLength(1));
                x = Random.Range(0, Grid.GetLength(0));
            }

            return new Vector3(
                x - (float)Grid.GetLength(0) / 2,
                y - (float)Grid.GetLength(1) / 2,
                0
            );
        }
        private void PlaceEnemies()
        {
            foreach (var v in _difficulty.enemies)
            {
                for (var i = 0; i < _difficulty.GetCount(v.enemy); i++)
                {
                    var e = GameManager.Instance.enemies.GetPooledObject(v.enemy.enemyName);
                    e.SetActive(true);
                    e.transform.position = GetRandomFloor();
                }
            }
        }
        public void Activate()
        {
            if (_isActive) return;
            VisualizeGrid();
            PlaceEnemies();
            _isActive = true;
        }
    }
}