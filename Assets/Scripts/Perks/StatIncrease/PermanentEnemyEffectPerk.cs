using Effects;
using UnityEngine;

namespace Perks.StatIncrease
{
    [CreateAssetMenu(menuName = "Perks/EnemyEffectors/PermanentEnemyEffectPerk", fileName = "PermanentEnemyEffectPerk")]
    public class PermanentEnemyEffectPerk : Perk
    {
        public InstantEffect effect;
    }
}