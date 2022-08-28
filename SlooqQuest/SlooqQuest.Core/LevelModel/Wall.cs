namespace SlooqQuest.Core.LevelModel
{
    public sealed class Wall : TileObject
    {
        public WallType Type { get; set; } = WallType.RedGray;
    }
}