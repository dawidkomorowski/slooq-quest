using Geisha.Common.Math;
using SlooqQuest.Core.LevelModel;

namespace SlooqQuest.Core.Util
{
    public static class TileExtensions
    {
        public static Vector2 GetTranslation(this Tile tile)
        {
            const int tileSize = 64;
            return new Vector2(tile.X * tileSize, tile.Y * tileSize);
        }
    }
}