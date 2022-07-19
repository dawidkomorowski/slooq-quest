using System;
using System.IO;
using Sokoban.Core;
using Sokoban.Core.EditorLogic;
using Sokoban.Core.GameLogic;
using Sokoban.Core.LevelModel;

namespace Sokoban.Editor
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
            var defaultLevelPath = Path.Join("Levels", "NewLevel.sokoban-level");
            if (File.Exists(defaultLevelPath))
            {
                var serializedLevel = File.ReadAllText(defaultLevelPath);
                return Level.Deserialize(serializedLevel);
            }
            else
            {
                return new Level();
            }
        }

        private void EditModeOnLevelModified(object? sender, EventArgs e)
        {
            File.WriteAllText(Path.Join("Levels", "NewLevel.sokoban-level"), EditMode.Level.Serialize());
        }
    }
}