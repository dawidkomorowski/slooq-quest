using System;
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
        }

        public Level Level { get; }
        public Player Player { get; }

        public void MoveUp()
        {
            var x = Player.Tile.X;
            var y = Player.Tile.Y;

            Level.GetTile(x, y + 1).TileObject = Player;
        }

        public void MoveDown()
        {
            var x = Player.Tile.X;
            var y = Player.Tile.Y;

            Level.GetTile(x, y - 1).TileObject = Player;
        }

        public void MoveLeft()
        {
            var x = Player.Tile.X;
            var y = Player.Tile.Y;

            Level.GetTile(x - 1, y).TileObject = Player;
        }

        public void MoveRight()
        {
            var x = Player.Tile.X;
            var y = Player.Tile.Y;

            Level.GetTile(x + 1, y).TileObject = Player;
        }
    }
}