using System;
using Sokoban.Core.LevelModel;

namespace Sokoban.Core.EditorLogic
{
    public sealed class EditMode
    {
        public EditMode(Level level)
        {
            Level = level;
            SelectedTile = level.GetTile(0, 0);
        }

        public Level Level { get; }

        public Tile SelectedTile { get; private set; }

        public event EventHandler? LevelModified;

        public void MoveUp()
        {
            var targetX = SelectedTile.X;
            var targetY = SelectedTile.Y + 1;

            Move(targetX, targetY);
        }

        public void MoveDown()
        {
            var targetX = SelectedTile.X;
            var targetY = SelectedTile.Y - 1;

            Move(targetX, targetY);
        }

        public void MoveLeft()
        {
            var targetX = SelectedTile.X - 1;
            var targetY = SelectedTile.Y;

            Move(targetX, targetY);
        }

        public void MoveRight()
        {
            var targetX = SelectedTile.X + 1;
            var targetY = SelectedTile.Y;

            Move(targetX, targetY);
        }

        public void Delete()
        {
            SelectedTile.TileObject = null;
            SelectedTile.CrateSpot = null;
            OnLevelModified();
        }

        public void CreateRedGrayWall()
        {
            SelectedTile.TileObject = new Wall { Type = WallType.RedGray };
            OnLevelModified();
        }

        public void CreateBrownCrate()
        {
            SelectedTile.TileObject = new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown };
            OnLevelModified();
        }

        public void CreateRedCrate()
        {
            SelectedTile.TileObject = new Crate { Type = CrateType.Red, CrateSpotType = CrateSpotType.Red };
            OnLevelModified();
        }

        public void CreateBrownCrateSpot()
        {
            SelectedTile.CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
            OnLevelModified();
        }

        public void CreateRedCrateSpot()
        {
            SelectedTile.CrateSpot = new CrateSpot { Type = CrateSpotType.Red };
            OnLevelModified();
        }

        public void PlacePlayer()
        {
            for (var x = 0; x < Level.Width; x++)
            {
                for (var y = 0; y < Level.Height; y++)
                {
                    var tile = Level.GetTile(x, y);
                    if (tile.TileObject is Player)
                    {
                        tile.TileObject = null;
                    }
                }
            }

            SelectedTile.TileObject = new Player();
            OnLevelModified();
        }

        private void Move(int targetX, int targetY)
        {
            if (Level.IsOutsideOfLevel(targetX, targetY))
            {
                return;
            }

            SelectedTile = Level.GetTile(targetX, targetY);
        }

        private void OnLevelModified()
        {
            LevelModified?.Invoke(this, EventArgs.Empty);
        }
    }
}