using Sokoban.Core.GameLogic;
using Sokoban.Core.LevelModel;

namespace Sokoban
{
    internal sealed class GameState
    {
        public GameState()
        {
            var level = Level.CreateTestLevel();
            GameMode = new GameMode(level);
        }

        public GameMode GameMode { get; private set; }
        public bool IsPendingRestart { get; private set; }

        public void RecreateGameMode()
        {
            var level = Level.CreateTestLevel();
            GameMode = new GameMode(level);
            IsPendingRestart = false;
        }

        public void AckComplete()
        {
            IsPendingRestart = true;
        }
    }
}