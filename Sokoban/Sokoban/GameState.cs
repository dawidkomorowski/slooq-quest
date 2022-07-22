using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Sokoban.Core.GameLogic;
using Sokoban.Core.LevelModel;

namespace Sokoban
{
    internal sealed class GameState
    {
        public GameState()
        {
            Levels = LoadLevels();
            CurrentLevel = Levels.First();
            var level = Level.CreateEmptyLevelValidForGameMode();
            GameMode = new GameMode(level);
        }

        public GameMode GameMode { get; private set; }
        public bool IsPendingRestart { get; private set; }

        public IReadOnlyList<LevelInfo> Levels { get; }
        public LevelInfo CurrentLevel { get; set; }

        public void RecreateGameMode()
        {
            var level = LoadLevel(CurrentLevel.FileName);
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

        private static IReadOnlyList<LevelInfo> LoadLevels()
        {
            var path = Path.Join("Levels", "Levels.json");
            var json = File.ReadAllText(path);
            var levels = JsonSerializer.Deserialize<List<LevelInfo>>(json);

            if (levels is null)
            {
                throw new InvalidOperationException("Cannot load levels.");
            }

            return levels.AsReadOnly();
        }
    }


    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class LevelInfo
    {
        public string Name { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}