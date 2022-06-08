namespace Sokoban.Core.LevelModel
{
    public sealed class CrateSpot
    {
        public Tile Tile { get; internal set; } = new Tile(-1, -1);
        public CrateSpotType Type { get; set; } = CrateSpotType.Brown;
    }
}