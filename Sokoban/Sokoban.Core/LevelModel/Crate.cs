namespace Sokoban.Core.LevelModel
{
    public sealed class Crate : TileObject
    {
        public CrateType Type { get; set; } = CrateType.Brown;
        public CrateSpotType CrateSpotType { get; set; } = CrateSpotType.Brown;
        public bool IsLocked { get; private set; } = false;

        public void OnMove()
        {
            if (Type is CrateType.Red)
            {
                RedCrateMechanics();
            }
        }

        private void RedCrateMechanics()
        {
            IsLocked = CrateSpotType == Tile.CrateSpot?.Type;
        }
    }
}