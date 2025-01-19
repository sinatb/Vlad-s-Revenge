using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player/PlayerClassData",fileName = "PlayerData")]
    public class PlayerClassData : ScriptableObject
    {
        public string           className;
        public Sprite           model;
        public float            maximumHealth;
        public int              maximumSpeed;
        public float            damage;
        public float            attackCooldown;
        
    }
}