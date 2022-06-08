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
        }

        public int Width { get; } = 10;
        public int Height { get; } = 10;

        public Tile GetTile(int x, int y)
        {
            return _tiles[y * Width + x];
        }

        public static Level CreateTestLevel()
        {
            var level = new Level();

            level.GetTile(0, 0).TileObject = new Wall();
            level.GetTile(4, 5).TileObject = new Wall();
            level.GetTile(3, 3).TileObject = new Player();
            level.GetTile(6, 7).TileObject = new Crate();

            return level;
        }

        private void SetTile(int x, int y, Tile tile)
        {
            _tiles[y * Width + x] = tile;
        }
    }
}