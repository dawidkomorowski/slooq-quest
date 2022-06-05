namespace Sokoban.Core.LevelModel
{
    public sealed class Level
    {
        private readonly Tile[] _tiles;

        public Level()
        {
            _tiles = new Tile[Width * Height];

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    SetTile(x, y, new Tile(x, y));
                }
            }

            GetTile(0, 0).TileObject = new Wall();
            GetTile(4, 5).TileObject = new Wall();
        }

        public int Width { get; } = 10;
        public int Height { get; } = 10;

        public Tile GetTile(int x, int y)
        {
            return _tiles[y * Width + x];
        }

        private void SetTile(int x, int y, Tile tile)
        {
            _tiles[y * Width + x] = tile;
        }
    }
}