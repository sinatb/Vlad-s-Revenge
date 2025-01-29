using System;
using System.Collections.Generic;
using Aoiti.Pathfinding;
using Managers;
using UnityEngine;

namespace Enemies
{
    public class EnemyAI 
    {
        Pathfinder<Vector2Int> _pathfinder;

        public void Setup()
        {
            _pathfinder = new Pathfinder<Vector2Int>(GetDistance, GetNeighbourNodes, 1000);
        }
        public List<Vector2Int> GeneratePathToPlayer(Vector2Int currentPosition)
        {
            if (_pathfinder.GenerateAstarPath(currentPosition,
                    GameManager.Instance.player.GetLocation(),
                    out var path)
                )
            {
                return path;
            }
            return new List<Vector2Int>();
        }
        private float GetDistance(Vector2Int a, Vector2Int b)
        {
            return (a - b).sqrMagnitude;
        }
        Dictionary<Vector2Int, float> GetNeighbourNodes(Vector2Int pos)
        {   
            var neighbours = new Dictionary<Vector2Int, float>();
            
            var g = GameManager.Instance.ActiveRoom.Grid;
            var x = (int)Math.Round(pos.x + (float)g.GetLength(0) / 2);
            var y = (int)Math.Round(pos.y + (float)g.GetLength(1) / 2);

            if (GameManager.Instance.ActiveRoom.Grid[y, x+1] == 1)
                neighbours.Add(pos+Vector2Int.right, 1);
            if (GameManager.Instance.ActiveRoom.Grid[y, x-1] == 1)
                neighbours.Add(pos+Vector2Int.left, 1);
            if (GameManager.Instance.ActiveRoom.Grid[y+1, x] == 1)
                neighbours.Add(pos+Vector2Int.up, 1);
            if (GameManager.Instance.ActiveRoom.Grid[y-1, x] == 1)
                neighbours.Add(pos+Vector2Int.down, 1);

            return neighbours;
        } 
    }
}