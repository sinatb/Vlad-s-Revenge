using PCG;

namespace Managers
{
    public class GameManager : BaseManager
    {
        public int       level;
        public Generator generator;

        private void Start()
        {
            var r = generator.GenerateRooms();
            r[0].Activate();
        }
    }
}