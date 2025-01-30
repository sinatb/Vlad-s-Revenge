using Managers;
using UnityEngine;

namespace Enemies
{
    public class Archer : BaseEnemy
    {
        private Vector2Int _selectedShootingPoint;
        protected override Vector2Int TargetLocation()
        {
            var p = GameManager.Instance.player.GetLocation();
            var g = GameManager.Instance.ActiveRoom.Grid;

            if ((_selectedShootingPoint - p).magnitude < 3.0f)
                _selectedShootingPoint = Vector2Int.zero;
            
            if (_selectedShootingPoint != Vector2Int.zero)
                return _selectedShootingPoint;
            
            var xPos = Random.Range(-g.GetLength(0)/2, g.GetLength(0)/2);
            var yPos = Random.Range(-g.GetLength(1)/2, g.GetLength(1)/2);
            var loc = GetGridLocation(xPos, yPos);
            while (GameManager.Instance.ActiveRoom.EnemyGrid[loc.y, loc.x] == 1 || 
                   GameManager.Instance.ActiveRoom.Grid[loc.y, loc.x] == 0)
            {
                xPos = Random.Range(-g.GetLength(0)/2, g.GetLength(0)/2);
                yPos = Random.Range(-g.GetLength(1)/2, g.GetLength(1)/2);
                loc = GetGridLocation(xPos, yPos);
            }

            _selectedShootingPoint = new Vector2Int(xPos, yPos);
            return _selectedShootingPoint;
        }
    }
}