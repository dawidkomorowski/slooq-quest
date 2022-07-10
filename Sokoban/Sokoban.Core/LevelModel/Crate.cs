namespace Sokoban.Core.LevelModel
{
    public sealed class Crate : TileObject
    {
        public CrateType Type { get; set; } = CrateType.Brown;
        public CrateSpotType CrateSpotType { get; set; } = CrateSpotType.Brown;
        public bool IsLocked { get; private set; } = false;
        public int Counter { get; set; } = 5;

        public void OnMove()
        {
            if (Type is CrateType.Red)
            {
                RedCrateMechanics();
            }

            if (Type is CrateType.Blue)
            {
                BlueCrateMechanics();
            }
        }

        private void RedCrateMechanics()
        {
            IsLocked = CrateSpotType == Tile.CrateSpot?.Type;
        }

        private void BlueCrateMechanics()
        {
            Counter--;
            IsLocked = Counter == 0;
        }
    }
}