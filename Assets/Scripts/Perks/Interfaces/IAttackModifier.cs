using Combat;

namespace Perks.Interfaces
{
    public interface IAttackModifier
    {
        public void ModifyAttack(Player.Player player, PlayerAttackData attackData);
    }
}