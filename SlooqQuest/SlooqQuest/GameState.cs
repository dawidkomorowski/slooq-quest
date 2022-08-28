using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using SlooqQuest.Core.GameLogic;
using SlooqQuest.Core.LevelModel;

namespace SlooqQuest
{
    internal sealed class GameState
    {
        public GameState()
        {
            Levels = LoadLevels();
            CurrentLevel = Levels.First();
            SavedGame = LoadGame();

            var level = Level.CreateEmptyLevelValidForGameMode();
            GameMode = new GameMode(level);
        }

        public GameMode GameMode { get; private set; }

        public IReadOnlyList<LevelInfo> Levels { get; }
        public LevelInfo CurrentLevel { get; set; }

        public SavedGame? SavedGame { get; private set; }

        public void NewGame()
        {
            CurrentLevel = Levels.First();
            SavedGame = new SavedGame { CurrentLevelName = CurrentLevel.Name };
            SaveGame();
            RefreshLevelsUnlockedInfo();

            RecreateGameMode();
        }

        public void Continue()
        {
            Debug.Assert(SavedGame != null, nameof(SavedGame) + " != null");
            CurrentLevel = Levels.Single(l => l.Name == SavedGame.CurrentLevelName);
            RefreshLevelsUnlockedInfo();

            RecreateGameMode();
        }

        public void OnLevelComplete()
        {
            Debug.Assert(SavedGame != null, nameof(SavedGame) + " != null");
            CurrentLevel = GetNextLevel();

            if (!CurrentLevel.IsUnlocked)
            {
                SavedGame.CurrentLevelName = CurrentLevel.Name;
                SaveGame();
                RefreshLevelsUnlockedInfo();
            }

            RecreateGameMode();
        }

        public void RecreateGameMode()
        {
            var level = LoadLevel(CurrentLevel.FileName);
            GameMode = new GameMode(level);
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

        private static SavedGame? LoadGame()
        {
            var saveFile = Path.Combine("SavedGames", "SavedGame.json");
            if (!File.Exists(saveFile))
            {
                return null;
            }

            var json = File.ReadAllText(saveFile);
            var savedGame = JsonSerializer.Deserialize<SavedGame>(json);
            return savedGame;
        }

        private void SaveGame()
        {
            const string saveDirectory = "SavedGames";

            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            var saveFile = Path.Combine(saveDirectory, "SavedGame.json");

            Debug.Assert(SavedGame != null, nameof(SavedGame) + " != null");
            var saveData = JsonSerializer.Serialize(SavedGame);

            File.WriteAllText(saveFile, saveData);
        }

        private void RefreshLevelsUnlockedInfo()
        {
            Debug.Assert(SavedGame != null, nameof(SavedGame) + " != null");

            foreach (var levelInfo in Levels)
            {
                levelInfo.IsUnlocked = false;
            }

            foreach (var levelInfo in Levels)
            {
                if (levelInfo.Name == SavedGame.CurrentLevelName)
                {
                    levelInfo.IsUnlocked = true;
                    break;
                }
                else
                {
                    levelInfo.IsUnlocked = true;
                }
            }
        }

        private static Level LoadLevel(string fileName)
        {
            var levelPath = Path.Join("Levels", $"{fileName}.sokoban-level");
            var serializedLevel = File.ReadAllText(levelPath);
            return Level.Deserialize(serializedLevel);
        }

        private LevelInfo GetNextLevel()
        {
            var nextLevel = Levels.SkipWhile(l => l != CurrentLevel).Skip(1).FirstOrDefault();
            return nextLevel ?? Levels.Last();
        }
    }


    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class LevelInfo
    {
        public string Name { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public bool IsUnlocked { get; set; } = false;
    }

    internal sealed class SavedGame
    {
        public string CurrentLevelName { get; set; } = string.Empty;
    }
}