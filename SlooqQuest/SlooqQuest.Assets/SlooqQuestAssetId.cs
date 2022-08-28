using System;
using Geisha.Engine.Core.Assets;

namespace SlooqQuest.Assets
{
    public static class SlooqQuestAssetId
    {
        public static class Sprites
        {
            public static class Ground
            {
                public static AssetId Brown { get; } = new AssetId(new Guid("cd5f29dc-091d-4788-ace4-327786fd6141"));
                public static AssetId Green { get; } = new AssetId(new Guid("10fc274a-ce77-4897-a934-e6d9d9c0992a"));
                public static AssetId Gray { get; } = new AssetId(new Guid("FFA50CAC-8C5B-4BB2-9E4A-C5D4FBBD8641"));
            }

            public static class Wall
            {
                public static AssetId Red { get; } = new AssetId(new Guid("5231822a-ecdd-419d-a20a-c382850a73a0"));
                public static AssetId RedGray { get; } = new AssetId(new Guid("0f721725-a418-4fd8-8243-0ae80db34b34"));
                public static AssetId Gray { get; } = new AssetId(new Guid("4f414142-3100-42f6-aad3-4b3b7063f704"));
                public static AssetId Brown { get; } = new AssetId(new Guid("cb6695fa-3d6e-443c-b221-0d6395d69bd6"));
                public static AssetId TopRed { get; } = new AssetId(new Guid("9c3cf615-07ac-4c70-88b4-7d5a5c6172c8"));
                public static AssetId TopRedGray { get; } = new AssetId(new Guid("a75af302-713c-4d0b-9e71-c57d2f86fb2d"));
                public static AssetId TopGray { get; } = new AssetId(new Guid("d0175c71-7d34-48a4-adb8-4decb5b937a4"));
                public static AssetId TopBrown { get; } = new AssetId(new Guid("84fb71b4-8ccd-49f2-a2dd-4db092666553"));
            }

            public static class Crate
            {
                public static AssetId Brown { get; } = new AssetId(new Guid("38b93af1-0689-4006-9821-6339a8a2f7b4"));
                public static AssetId Red { get; } = new AssetId(new Guid("a38a3dbf-9a1d-42bb-9312-813b7b5c38ad"));
                public static AssetId RedGrayedOut { get; } = new AssetId(new Guid("f6e42816-6abb-40ce-a19b-3762b91287f1"));
                public static AssetId Blue { get; } = new AssetId(new Guid("5a21aa82-4c12-4202-abdc-19657e477da7"));
                public static AssetId BlueGrayedOut { get; } = new AssetId(new Guid("2f5428e4-1455-460b-a010-1d24c7e68ae1"));
                public static AssetId Green { get; } = new AssetId(new Guid("78eb39ff-bdf3-4090-8342-8cae95d3bc7b"));
                public static AssetId Gray { get; } = new AssetId(new Guid("3800e86b-d36a-45e0-8156-d3fa8f6a9f56"));
                public static AssetId GrayGrayedOut { get; } = new AssetId(new Guid("719823a9-8403-4f62-a0d4-e7a7d4997114"));
            }

            public static class CrateSpot
            {
                public static AssetId Brown { get; } = new AssetId(new Guid("46f723ec-3f77-41b2-8503-43c6290181d1"));
                public static AssetId Red { get; } = new AssetId(new Guid("553b77b6-d83e-4e05-8c35-e8b8229016d5"));
                public static AssetId Blue { get; } = new AssetId(new Guid("d1b38cbe-3f8c-4e95-a0cd-c632f58eb8d4"));
                public static AssetId Green { get; } = new AssetId(new Guid("0f46f1b0-6312-4f14-b3c4-db0d7371fb50"));
            }

            public static class Player
            {
                public static AssetId Default { get; } = new AssetId(new Guid("a98d0e5d-1046-4aff-a958-017fcd307352"));
            }

            public static class Slooq
            {
                public static AssetId Default { get; } = new AssetId(new Guid("f2f04dab-8d22-4773-be1b-766173597de4"));
            }

            public static class MainMenu
            {
                public static AssetId Title { get; } = new AssetId(new Guid("a35428be-a2e0-4c97-aecc-b334bb5cd231"));
            }

            public static class Editor
            {
                public static AssetId Cursor { get; } = new AssetId(new Guid("62393af4-2983-4086-8529-17cde2962cb6"));
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

            public static class Smoke
            {
                public static AssetId Default { get; } = new AssetId(new Guid("65e8b2c8-d0a8-488a-8bc3-a70491c315c1"));
            }
        }

        public static class Music
        {
            public static AssetId MainMenu { get; } = new AssetId(new Guid("4362f25e-0e7a-407c-80f4-dd3418b85639"));
        }

        public static class InputMapping
        {
            public static AssetId Default { get; } = new AssetId(new Guid("e38ecd7d-cf5c-4399-8ef7-ef2d3f497e22"));
        }
    }
}