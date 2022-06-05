namespace Sokoban.Core.LevelModel
{
    public sealed class Tile
    {
        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
        public Ground Ground { get; set; } = Ground.Gray;
    }
}