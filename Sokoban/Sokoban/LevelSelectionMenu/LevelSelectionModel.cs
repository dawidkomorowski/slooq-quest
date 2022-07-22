﻿using System.Collections.Generic;
using System.Linq;

namespace Sokoban.LevelSelectionMenu
{
    internal sealed class LevelSelectionModel
    {
        private readonly List<LevelInfo> _levels = new List<LevelInfo>();
        private readonly List<LevelInfo> _previousLevels = new List<LevelInfo>();
        private readonly List<LevelInfo> _nextLevels = new List<LevelInfo>();

        public LevelSelectionModel(IEnumerable<LevelInfo> levels)
        {
            _levels.AddRange(levels);

            SelectedLevel = _levels.First();
            _nextLevels.AddRange(_levels.Skip(1));
        }

        public LevelInfo SelectedLevel { get; private set; }
        public IReadOnlyList<LevelInfo> PreviousLevels => _previousLevels.AsReadOnly();
        public IReadOnlyList<LevelInfo> NextLevels => _nextLevels.AsReadOnly();

        public void SelectPreviousLevel()
        {
            if (PreviousLevels.Count == 0)
            {
                return;
            }

            _nextLevels.Insert(0, SelectedLevel);
            SelectedLevel = _previousLevels.Last();
            _previousLevels.RemoveAt(_previousLevels.Count - 1);
        }

        public void SelectNextLevel()
        {
            if (NextLevels.Count == 0)
            {
                return;
            }

            _previousLevels.Add(SelectedLevel);
            SelectedLevel = _nextLevels.First();
            _nextLevels.RemoveAt(0);
        }
    }
}