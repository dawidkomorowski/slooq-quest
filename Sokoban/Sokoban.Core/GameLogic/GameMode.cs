﻿using System;
using System.Collections.Generic;
using Sokoban.Core.LevelModel;

namespace Sokoban.Core.GameLogic
{
    public class GameMode
    {
        public GameMode(Level level)
        {
            Level = level;

            for (var x = 0; x < level.Width; x++)
            {
                for (var y = 0; y < level.Height; y++)
                {
                    var tileObject = level.GetTile(x, y).TileObject;
                    if (tileObject is Player player)
                    {
                        if (Player != null)
                        {
                            throw new ArgumentException("Level contains multiple players.");
                        }

                        Player = player;
                    }
                }
            }

            if (Player is null)
            {
                throw new ArgumentException("Level does not contain player.");
            }
        }

        public Level Level { get; }
        public Player Player { get; }

        public void MoveUp()
        {
            Move(0, 1);
        }

        public void MoveDown()
        {
            Move(0, -1);
        }

        public void MoveLeft()
        {
            Move(-1, 0);
        }

        public void MoveRight()
        {
            Move(1, 0);
        }

        public bool IsLevelComplete()
        {
            for (var x = 0; x < Level.Width; x++)
            {
                for (var y = 0; y < Level.Height; y++)
                {
                    var tile = Level.GetTile(x, y);

                    if (tile.CrateSpot != null)
                    {
                        if (tile.TileObject is Crate crate)
                        {
                            if (tile.CrateSpot.Type != crate.CrateSpotType)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void Move(int deltaX, int deltaY)
        {
            var x = Player.Tile.X;
            var y = Player.Tile.Y;

            var targetX = x + deltaX;
            var targetY = y + deltaY;

            if (Level.IsOutsideOfLevel(targetX, targetY))
            {
                return;
            }

            var targetTile = Level.GetTile(targetX, targetY);

            if (targetTile.TileObject is Wall)
            {
                return;
            }

            if (targetTile.TileObject is Crate crate)
            {
                var crateTargetX = targetX + deltaX;
                var crateTargetY = targetY + deltaY;

                if (Level.IsOutsideOfLevel(crateTargetX, crateTargetY))
                {
                    return;
                }

                var crateTargetTile = Level.GetTile(crateTargetX, crateTargetY);

                if (crateTargetTile.TileObject != null)
                {
                    return;
                }

                if (StopRedCrate_Mechanics(crate))
                {
                    return;
                }

                if (StopBlueCrate_Mechanics(crate))
                {
                    return;
                }

                if (StopAllButGreenCrate_Mechanics(crate, crateTargetTile))
                {
                    return;
                }

                crateTargetTile.TileObject = crate;
            }

            targetTile.TileObject = Player;
        }

        #region Crate mechanics

        private bool StopRedCrate_Mechanics(Crate crate)
        {
            if (crate.Type != CrateType.Red)
            {
                return false;
            }

            return crate.CrateSpotType == crate.Tile.CrateSpot?.Type;
        }

        private bool StopBlueCrate_Mechanics(Crate crate)
        {
            if (crate.Type != CrateType.Blue)
            {
                return false;
            }

            if (!_blueCrateMechanicsState.ContainsKey(crate))
            {
                _blueCrateMechanicsState.Add(crate, 5);
            }
            else
            {
                _blueCrateMechanicsState[crate]--;
            }

            return _blueCrateMechanicsState[crate] <= 0;
        }

        private readonly Dictionary<Crate, int> _blueCrateMechanicsState = new Dictionary<Crate, int>();

        private bool StopAllButGreenCrate_Mechanics(Crate crate, Tile targetTile)
        {
            if (crate.Type is CrateType.Green)
            {
                return false;
            }

            return targetTile.Ground == Ground.Green;
        }

        #endregion
    }
}