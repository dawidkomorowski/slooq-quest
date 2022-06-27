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

        public void SetGroundToNone()
        {
            SetGround(Ground.None);
        }

        public void SetGroundToBrown()
        {
            SetGround(Ground.Brown);
        }

        public void SetGroundToGreen()
        {
            SetGround(Ground.Green);
        }

        public void SetGroundToGray()
        {
            SetGround(Ground.Gray);
        }

        public void CreateRedGrayWall()
        {
            SetTileObject(new Wall { Type = WallType.RedGray });
        }

        public void CreateBrownCrate()
        {
            SetTileObject(new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown });
        }

        public void CreateRedCrate()
        {
            SetTileObject(new Crate { Type = CrateType.Red, CrateSpotType = CrateSpotType.Red });
        }

        public void CreateBrownCrateSpot()
        {
            CreateCrateSpot(CrateSpotType.Brown);
        }

        public void CreateRedCrateSpot()
        {
            CreateCrateSpot(CrateSpotType.Red);
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

            SetTileObject(new Player());
        }

        private void Move(int targetX, int targetY)
        {
            if (Level.IsOutsideOfLevel(targetX, targetY))
            {
                return;
            }

            SelectedTile = Level.GetTile(targetX, targetY);
        }

        private void SetGround(Ground ground)
        {
            SelectedTile.Ground = ground;
            OnLevelModified();
        }

        private void SetTileObject(TileObject tileObject)
        {
            SelectedTile.TileObject = tileObject;
            OnLevelModified();
        }

        private void CreateCrateSpot(CrateSpotType type)
        {
            SelectedTile.CrateSpot = new CrateSpot { Type = type };
            OnLevelModified();
        }

        private void OnLevelModified()
        {
            LevelModified?.Invoke(this, EventArgs.Empty);
        }
    }
}