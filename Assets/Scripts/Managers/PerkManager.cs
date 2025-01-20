using System.Collections.Generic;
using NUnit.Framework;
using Perks;
using UnityEngine;

namespace Managers
{
    public class PerkManager : MonoBehaviour
    {
        private static PerkManager _instance;
        public static PerkManager Instance => _instance;
        public List<Perk> perks;
        private List<Perk> _availablePerks;
        private int _maxChance;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            _availablePerks = new List<Perk>(perks);
            foreach (var p in Instance._availablePerks)
            {
                _maxChance += p.chance;
            }
        }

        public static void UpdatePerks()
        {
            Instance._maxChance = 0;
            Instance._availablePerks = new List<Perk>();
            foreach (var p in Instance.perks)
            {
                if (!GameManager.Instance.player.Perks.Contains(p))
                {
                    Instance._maxChance += p.chance;
                    Instance._availablePerks.Add(p);
                }
            }
        }
        public static Perk GetPerk()
        {
            var random = Random.Range(0, Instance._maxChance);
            var cumulativeChance = 0;
            foreach (var p in Instance._availablePerks)
            {
                cumulativeChance += p.chance;
                if (random <= cumulativeChance)
                {
                    Instance._maxChance -= p.chance;
                    Instance._availablePerks.Remove(p);
                    return p;
                }
            }

            return null;
        }
    }
}
