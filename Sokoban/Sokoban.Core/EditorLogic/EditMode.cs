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