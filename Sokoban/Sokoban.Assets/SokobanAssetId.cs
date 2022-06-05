using System;
using Geisha.Engine.Core.Assets;

namespace Sokoban.Assets
{
    public static class SokobanAssetId
    {
        public static class Sprites
        {
            public static class Ground
            {
                public static AssetId Gray { get; } = new AssetId(new Guid("FFA50CAC-8C5B-4BB2-9E4A-C5D4FBBD8641"));
            }

            public static class Wall
            {
                public static AssetId RedGray { get; } = new AssetId(new Guid("0f721725-a418-4fd8-8243-0ae80db34b34"));
            }
        }
    }
}