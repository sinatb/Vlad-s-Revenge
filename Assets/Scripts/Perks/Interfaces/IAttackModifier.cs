using Projectiles;

namespace Perks.Interfaces
{
    public interface IAttackModifier
    {
        public void ModifyAttack(Player.Player player, Projectile projectile);
    }
}