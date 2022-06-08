namespace Sokoban.Core.LevelModel
{
    public sealed class Crate : TileObject
    {
        public CrateType Type { get; set; } = CrateType.Brown;
    }
}