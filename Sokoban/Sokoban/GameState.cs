using System.IO;
using Sokoban.Core.GameLogic;
using Sokoban.Core.LevelModel;

namespace Sokoban
{
    internal sealed class GameState
    {
        public GameState()
        {
            var level = LoadLevel("dotalevel1");
            GameMode = new GameMode(level);
        }

        public GameMode GameMode { get; private set; }
        public bool IsPendingRestart { get; private set; }

        public void RecreateGameMode()
        {
            var level = LoadLevel("dotalevel1");
            GameMode = new GameMode(level);
            IsPendingRestart = false;
        }

        public void AckComplete()
        {
            IsPendingRestart = true;
        }

        private static Level LoadLevel(string fileName)
        {
            var levelPath = Path.Join("Levels", $"{fileName}.sokoban-level");
            var serializedLevel = File.ReadAllText(levelPath);
            return Level.Deserialize(serializedLevel);
        }
    }
}