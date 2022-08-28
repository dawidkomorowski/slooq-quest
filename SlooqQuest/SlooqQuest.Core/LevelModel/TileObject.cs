namespace SlooqQuest.Core.LevelModel
{
    public abstract class TileObject
    {
        public Tile Tile { get; internal set; } = new Tile(-1, -1);
    }
}