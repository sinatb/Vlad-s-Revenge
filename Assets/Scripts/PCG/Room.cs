using System.Collections.Generic;
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
        public bool                hasEnemy = true;
        private int                _enemyCount;
        private PcgStyle           _style;    
        private bool               _isActive;
        private DifficultySettings _difficulty;
        private List<GameObject>   _usedTiles;
 
        public void Init(int[,] grid, PcgStyle style, DifficultySettings difficulty)
        {
            Grid = (int[,]) grid.Clone();
            _style = style;
            _difficulty = difficulty;
            _usedTiles = new List<GameObject>();
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
                        var prefab = GameManager.GetStylePool(_style).GetRandomFloor();
                        prefab.SetActive(true);
                        prefab.transform.position = new Vector3(j - (float)width / 2, i - (float)width / 2, 0);
                        _usedTiles.Add(prefab);
                    }
                    else
                    {
                        var prefab = GameManager.GetStylePool(_style).GetRandomWall();
                        prefab.SetActive(true);
                        prefab.transform.position = new Vector3(j - (float)width / 2, i - (float)width / 2, 0);
                        _usedTiles.Add(prefab);
                    }
                }
            }
        }

        public Vector3 GetRandomFloor()
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
                _enemyCount += _difficulty.GetCount(v.enemy);
                for (var i = 0; i < _difficulty.GetCount(v.enemy); i++)
                {
                    var e = GameManager.Instance.enemies.GetPooledObject(v.enemy.enemyName);
                    e.SetActive(true);
                    e.transform.position = GetRandomFloor();
                    _usedTiles.Add(e);
                }
            }
        }

        public void KillEnemy()
        {
            _enemyCount--;
            if (_enemyCount <= 0)
                hasEnemy = false;
        }
        public void Activate()
        {
            if (_isActive) return;
            VisualizeGrid();
            PlaceEnemies();
            _isActive = true;
        }

        public void Deactivate()
        {
            foreach (var t in _usedTiles)
            {
                t.SetActive(false);
            }
            _isActive = false;
        }
    }
}