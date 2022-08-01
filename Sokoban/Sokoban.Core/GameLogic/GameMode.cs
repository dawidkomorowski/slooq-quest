using System;
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

        public void DeleteSlooq()
        {
            for (var x = 0; x < Level.Width; x++)
            {
                for (var y = 0; y < Level.Height; y++)
                {
                    var tile = Level.GetTile(x, y);

                    if (tile.TileObject is Crate crate)
                    {
                        if (crate.Type is CrateType.Slooq)
                        {
                            tile.TileObject = null;
                            return;
                        }
                    }
                }
            }

            throw new InvalidOperationException("Cannot delete Slooq from level as it was not found.");
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

                if (crate.IsLocked)
                {
                    return;
                }

                crateTargetTile.TileObject = crate;

                crate.OnMove();
            }

            targetTile.TileObject = Player;
        }
    }
}