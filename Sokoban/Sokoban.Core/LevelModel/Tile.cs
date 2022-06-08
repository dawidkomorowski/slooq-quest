namespace Sokoban.Core.LevelModel
{
    public sealed class Tile
    {
        private TileObject? _tileObject;
        private CrateSpot? _crateSpot;

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
                if (value != null)
                {
                    value.Tile.TileObject = null;
                    value.Tile = this;
                }

                _tileObject = value;
            }
        }

        public CrateSpot? CrateSpot
        {
            get => _crateSpot;
            internal set
            {
                if (value != null)
                {
                    value.Tile = this;
                }

                _crateSpot = value;
            }
        }
    }
}