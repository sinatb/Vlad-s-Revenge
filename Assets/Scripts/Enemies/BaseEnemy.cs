using System;
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
        public float                            _health;
        public int                              _blood;
        private List<InstantEffect>              _instantEffects;
        private Dictionary<TimedEffect, float>   _timedEffects;
        public Image                             healthStatus;
        
        private void Awake()
        {
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
            _health = data.health;
            _blood = data.bloodBonus;
            _timedEffects.Clear();
            ApplyInstantEffects();
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
                gameObject.SetActive(false);
            }
        }
    }
}