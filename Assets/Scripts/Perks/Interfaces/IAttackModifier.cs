using Projectiles;

namespace Perks.Interfaces
{
    public interface IAttackModifier
    {
        public void ModifyAttack(Player.Player player, PlayerProjectile projectile);
    }
}