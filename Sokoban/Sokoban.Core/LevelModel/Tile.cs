namespace Sokoban.Core.LevelModel
{
    public sealed class Tile
    {
        private TileObject? _tileObject;

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
        public Ground Ground { get; set; } = Ground.Gray;

        public TileObject? TileObject
        {
            get => _tileObject;
            internal set
            {
                _tileObject = value;

                if (_tileObject != null)
                {
                    _tileObject.Tile = this;
                }
            }
        }
    }
}