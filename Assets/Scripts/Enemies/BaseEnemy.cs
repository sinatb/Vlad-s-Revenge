using System;
using System.Collections.Generic;
using Effects;
using Managers;
using UnityEngine;

namespace Enemies
{
    public class BaseEnemy : MonoBehaviour
    {
        public EnemyData data;
        public float               _health;
        public int                 _blood;
        private List<InstantEffect> _instantEffects;
        private List<TimedEffect>   _timedEffects;
        
        private void Awake()
        {
            _instantEffects = new List<InstantEffect>();
            _timedEffects = new List<TimedEffect>();
        }

        public void IncreaseBlood(int amount)
        {
            _blood += amount;
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
                _timedEffects.Add(te);
            else if (e is InstantEffect ie)
            {
                _instantEffects.Add(ie);
                ApplyInstantEffects();
            }
        }
        private void OnEnable()
        {
            _health = data.health;
            _blood = data.bloodBonus;
            ApplyInstantEffects();
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                GameManager.Instance.player.AddBlood(_blood);
                gameObject.SetActive(false);
            }
        }
    }
}