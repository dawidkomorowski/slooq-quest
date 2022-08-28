using System;
using System.IO;
using SlooqQuest.Core;
using SlooqQuest.Core.EditorLogic;
using SlooqQuest.Core.GameLogic;
using SlooqQuest.Core.LevelModel;

namespace SlooqQuest.Editor
{
    internal sealed class EditorState
    {
        public EditorState()
        {
            EditMode = new EditMode(LoadDefaultLevel());
            EditMode.LevelModified += EditModeOnLevelModified;
            GameMode = new GameMode(Level.CreateEmptyLevelValidForGameMode());
        }

        public EditMode EditMode { get; }
        public GameMode GameMode { get; private set; }
        public Mode Mode { get; private set; } = Mode.Edit;

        public void ToggleMode()
        {
            Mode = Mode switch
            {
                Mode.Edit => Mode.Game,
                Mode.Game => Mode.Edit,
                _ => throw new ArgumentOutOfRangeException()
            };

            if (Mode is Mode.Game)
            {
                var level = Level.Deserialize(EditMode.Level.Serialize());
                GameMode = new GameMode(level);
            }
        }

        private static Level LoadDefaultLevel()
        {
            var newLevelPath = Path.Join("Levels", "NewLevel.sokoban-level");
            if (File.Exists(newLevelPath))
            {
                var serializedLevel = File.ReadAllText(newLevelPath);
                return Level.Deserialize(serializedLevel);
            }
            else
            {
                var defaultLevelPath = Path.Join("Levels", "DefaultLevel.sokoban-level");
                var serializedLevel = File.ReadAllText(defaultLevelPath);
                return Level.Deserialize(serializedLevel);
            }
        }

        private void EditModeOnLevelModified(object? sender, EventArgs e)
        {
            File.WriteAllText(Path.Join("Levels", "NewLevel.sokoban-level"), EditMode.Level.Serialize());
        }
    }
}