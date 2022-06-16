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
        }

        public void CreateRedGrayWall()
        {
            SelectedTile.TileObject = new Wall { Type = WallType.RedGray };
        }

        public void CreateBrownCrate()
        {
            SelectedTile.TileObject = new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown };
        }

        public void CreateRedCrate()
        {
            SelectedTile.TileObject = new Crate { Type = CrateType.Red, CrateSpotType = CrateSpotType.Red };
        }

        public void CreateBrownCrateSpot()
        {
            SelectedTile.CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
        }

        public void CreateRedCrateSpot()
        {
            SelectedTile.CrateSpot = new CrateSpot { Type = CrateSpotType.Red };
        }

        private void Move(int targetX, int targetY)
        {
            if (Level.IsOutsideOfLevel(targetX, targetY))
            {
                return;
            }

            SelectedTile = Level.GetTile(targetX, targetY);
        }
    }
}