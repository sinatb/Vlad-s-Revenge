using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class Warrior : BaseEnemy
    {
        protected override Vector2Int TargetLocation()
        {
            var p = GameManager.Instance.player.GetLocation();
            var g = GameManager.Instance.ActiveRoom.Grid;
            var xCoef = Random.Range(-1, 1);
            var yCoef = Random.Range(-1, 1);
            var loc = GetGridLocation(p.x + xCoef, p.y + yCoef);
            while (loc.x < 0 || loc.x >= g.GetLength(0) ||
                   loc.y < 0 || loc.y >= g.GetLength(1))
            {
                xCoef = Random.Range(-10, 10);
                yCoef = Random.Range(-10, 10);
                loc = GetGridLocation(p.x + xCoef, p.y + yCoef);
            }
            return new Vector2Int(p.x + xCoef, p.y + yCoef);
        }
    }
}