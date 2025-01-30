using System;
using System.Collections;
using System.Collections.Generic;
using Effects;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class BaseEnemy : MonoBehaviour
    {
        public EnemyData                         data;
        private float                            _health;
        private int                              _blood;
        private List<InstantEffect>              _instantEffects;
        private Dictionary<TimedEffect, float>   _timedEffects;
        private EnemyAI                          _ai;
        private bool                             _canMove = true;
        private Vector2Int                       _enemyGridLocation;
        public Image                             healthStatus;
        private void Awake()
        {
            _ai = new EnemyAI();
            _instantEffects = new List<InstantEffect>();
            _timedEffects = new Dictionary<TimedEffect, float>();
        }
        public void IncreaseBlood(int amount)
        {
            _blood += amount;
        }
        private IEnumerator<WaitForSeconds> TakeEffectDamage(TimedEffect te)
        {
            while (_timedEffects[te] > 0)
            {
                yield return new WaitForSeconds(te.tickTime);
                _timedEffects[te] -= te.tickTime;
                TakeDamage(te.amount);
            }
            _timedEffects.Remove(te);
        }
        private void ApplyInstantEffects()
        {
            foreach(var ie in _instantEffects)
                ie.Apply(this);
        }
        public void DecreaseHealthPercentage(int amount)
        {
            _health -= _health * amount / 100;
        }
        public void AddEffect(BaseEffect e)
        {
            if (e is TimedEffect te)
            {
                if (!_timedEffects.ContainsKey(te))
                {
                    _timedEffects.Add(te, te.effectTime);
                    StartCoroutine(TakeEffectDamage(te));
                }
                else
                {
                    _timedEffects[te] = te.effectTime;
                }
            }
            else if (e is InstantEffect ie)
            {
                _instantEffects.Add(ie);
                ApplyInstantEffects();
            }
        }
        private void OnEnable()
        {
            if (GameManager.Instance.ActiveRoom == null)
                return;

            var loc = GetGridLocation();
            _enemyGridLocation = new Vector2Int(loc.x, loc.y);
            GameManager.Instance.ActiveRoom.EnemyGrid[loc.y, loc.x] = 1;
            
            _health = data.health;
            _blood = data.bloodBonus;
            _ai.Setup();
            _timedEffects.Clear();
            ApplyInstantEffects();
            _canMove = true;
            healthStatus.color = Color.black;
        }
        public void TakeDamage(float damage)
        {
            _health -= damage;
            healthStatus.color = Color.white;
            var healthGroup = (int)(_health / data.health * 4); 
            var index = 4 - (healthGroup < 0 ? 0 : healthGroup);
            healthStatus.sprite = data.healthUI[index >= 0 ? index : 0];
            if (_health <= 0)
            {
                GameManager.Instance.player.AddBlood(_blood);
                GameManager.Instance.ActiveRoom.KillEnemy();
                GameManager.Instance.ActiveRoom.EnemyGrid[_enemyGridLocation.y, _enemyGridLocation.x] = 0;
                gameObject.SetActive(false);
            }
        }
        private IEnumerator MoveDelay()
        {
            yield return new WaitForSeconds(data.moveDelay);
            _canMove = true;
        }
        private void Update()
        {
            if (!GameManager.Instance.IsGameRunning)
                return;
            var path = _ai.GeneratePathToPlayer(GetLocation());
            if (_canMove && path.Count > 0)
            {
                GameManager.Instance.ActiveRoom.EnemyGrid[_enemyGridLocation.y, _enemyGridLocation.x] = 0;
                transform.position = new Vector3(path[0].x, path[0].y, 0);
                _enemyGridLocation = GetGridLocation();
                GameManager.Instance.ActiveRoom.EnemyGrid[_enemyGridLocation.y, _enemyGridLocation.x] = 1;
                _canMove = false;
                StartCoroutine(MoveDelay());
            }
        }
        private Vector2Int GetGridLocation()
        {
            var g = GameManager.Instance.ActiveRoom.Grid;
            var x = (int)Math.Round(transform.position.x + (float)g.GetLength(0) / 2);
            var y = (int)Math.Round(transform.position.y + (float)g.GetLength(1) / 2);
            return new Vector2Int(x, y);
        }
        private Vector2Int GetLocation()
        {
            var x = (int)(transform.position.x);
            var y = (int)(transform.position.y);
            return new Vector2Int(x, y);
        }

    }
}