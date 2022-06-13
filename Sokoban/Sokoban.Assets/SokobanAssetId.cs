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

            public static class Crate
            {
                public static AssetId Brown { get; } = new AssetId(new Guid("38b93af1-0689-4006-9821-6339a8a2f7b4"));
                public static AssetId Red { get; } = new AssetId(new Guid("a38a3dbf-9a1d-42bb-9312-813b7b5c38ad"));
            }

            public static class CrateSpot
            {
                public static AssetId Brown { get; } = new AssetId(new Guid("46f723ec-3f77-41b2-8503-43c6290181d1"));
                public static AssetId Red { get; } = new AssetId(new Guid("553b77b6-d83e-4e05-8c35-e8b8229016d5"));
            }

            public static class Player
            {
                public static AssetId Default { get; } = new AssetId(new Guid("a98d0e5d-1046-4aff-a958-017fcd307352"));
            }
        }

        public static class Animations
        {
            public static class Player
            {
                public static AssetId StandUp { get; } = new AssetId(new Guid("dc85804b-db7f-4a6f-b251-609451008ce0"));
                public static AssetId StandDown { get; } = new AssetId(new Guid("a362b8ac-7c5f-43e7-9ca5-71ff6dd9d88c"));
                public static AssetId StandLeft { get; } = new AssetId(new Guid("602874fe-b064-4ae6-a48f-7181b360d9cb"));
                public static AssetId StandRight { get; } = new AssetId(new Guid("2725de37-3d29-48b0-b8e9-48cf0bf0bcb4"));
                public static AssetId MoveUp { get; } = new AssetId(new Guid("917002df-1c6d-48e2-a2ae-878347e2a78f"));
                public static AssetId MoveDown { get; } = new AssetId(new Guid("39b19ca7-3db3-418f-8344-329fb9d00184"));
                public static AssetId MoveLeft { get; } = new AssetId(new Guid("b58c800d-f76f-4507-ac5a-59fcdc1e44c1"));
                public static AssetId MoveRight { get; } = new AssetId(new Guid("8fa57684-48a0-4d4b-855e-106596dbba08"));
            }
        }

        public static class InputMapping
        {
            public static AssetId Default { get; } = new AssetId(new Guid("e38ecd7d-cf5c-4399-8ef7-ef2d3f497e22"));
        }
    }
}