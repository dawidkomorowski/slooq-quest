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

            for (var x = 0; x < level.Width; x++)
            {
                for (var y = 0; y < level.Height; y++)
                {
                    if (x == 0 || y == 0 || x == level.Width - 1 || y == level.Height - 1)
                    {
                        level.GetTile(x, y).TileObject = new Wall();
                    }
                }
            }

            level.GetTile(4, 5).TileObject = new Wall();
            level.GetTile(5, 5).TileObject = new Wall();
            level.GetTile(6, 5).TileObject = new Wall();

            level.GetTile(6, 7).TileObject = new Crate();
            level.GetTile(2, 4).TileObject = new Crate();

            level.GetTile(1, 1).CrateSpot = new CrateSpot();
            level.GetTile(8, 8).CrateSpot = new CrateSpot();

            level.GetTile(3, 3).TileObject = new Player();

            return level;
        }

        private void SetTile(int x, int y, Tile tile)
        {
            _tiles[y * Width + x] = tile;
        }
    }
}