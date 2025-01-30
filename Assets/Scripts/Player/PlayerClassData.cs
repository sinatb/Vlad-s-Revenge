using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player/PlayerClassData",fileName = "PlayerData")]
    public class PlayerClassData : ScriptableObject
    {
        public string           className;
        public Sprite           model;
        public Sprite           special;
        public float            maximumHealth;
        public int              maximumSpeed;
        public float            damage;
        public float            attackCooldown;
        public float            specialCooldown;
        public bool             flipSprite;
    }
}