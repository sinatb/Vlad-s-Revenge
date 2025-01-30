
namespace Perks.Interfaces
{
    /// <summary>
    /// Used to mark perks that increase stats.
    /// </summary>
    public interface IStatsIncrease
    {
        public void IncreaseStats(Player.Player p);
    }
}