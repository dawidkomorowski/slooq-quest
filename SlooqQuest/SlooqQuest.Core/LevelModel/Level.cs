using System;
using System.Reflection;
using System.Text.Json.Nodes;

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
                    var tile = new Tile(x, y);
                    _tiles[y * Width + x] = tile;
                }
            }
        }

        private static Version CurrentVersion => Version.Parse(Assembly.GetAssembly(typeof(Level))?.GetName().Version?.ToString(2) ??
                                                               throw new InvalidOperationException("Cannot get current version."));

        public int Width { get; } = 10;
        public int Height { get; } = 10;

        public Tile GetTile(int x, int y)
        {
            return _tiles[y * Width + x];
        }

        public bool IsOutsideOfLevel(int x, int y)
        {
            return x < 0 || y < 0 || x >= Width || y >= Height;
        }

        public string Serialize()
        {
            var tilesJsonArray = new JsonArray();
            foreach (var tile in _tiles)
            {
                var tileJsonObject = new JsonObject
                {
                    { "X", tile.X },
                    { "Y", tile.Y },
                    { "Ground", tile.Ground.ToString() },
                    { "CrateSpot", tile.CrateSpot?.Type.ToString() },
                    {
                        "TileObject", tile.TileObject switch
                        {
                            null => null,
                            Crate crate => new JsonObject
                            {
                                { "ObjectType", "Crate" },
                                { "Type", crate.Type.ToString() },
                                { "CrateSpotType", crate.CrateSpotType.ToString() },
                                { "Counter", crate.Counter },
                                { "IsHidden", crate.IsHidden }
                            },
                            Player _ => new JsonObject
                            {
                                { "ObjectType", "Player" }
                            },
                            Wall wall => new JsonObject
                            {
                                { "ObjectType", "Wall" },
                                { "Type", wall.Type.ToString() }
                            },
                            _ => throw new ArgumentOutOfRangeException($"Unsupported {nameof(Tile.TileObject)} type: {tile.TileObject.GetType()}")
                        }
                    }
                };
                tilesJsonArray.Add(tileJsonObject);
            }

            var levelJsonObject = new JsonObject
            {
                { "Version", CurrentVersion.ToString(2) },
                { "Width", Width },
                { "Height", Height },
                { "Tiles", tilesJsonArray }
            };

            return levelJsonObject.ToJsonString();
        }

        public static Level Deserialize(string serializedLevel)
        {
            try
            {
                var levelJsonNode = JsonNode.Parse(serializedLevel);
                if (levelJsonNode is null)
                {
                    throw new LevelDataCorruptedException();
                }

                Upgrade(levelJsonNode);

                var levelJsonObject = levelJsonNode.AsObject();
                var level = new Level();

                var tilesJsonNode = GetNotNullPropertyValue(levelJsonObject, "Tiles");

                foreach (var tileJsonNode in tilesJsonNode.AsArray())
                {
                    if (tileJsonNode is null)
                    {
                        throw new LevelDataCorruptedException();
                    }

                    var tileJsonObject = tileJsonNode.AsObject();

                    var x = GetNotNullPropertyValue(tileJsonObject, "X").GetValue<int>();
                    var y = GetNotNullPropertyValue(tileJsonObject, "Y").GetValue<int>();
                    var ground = Enum.Parse<Ground>(GetNotNullPropertyValue(tileJsonObject, "Ground").GetValue<string>());

                    var crateSpotJsonNode = GetPropertyValue(tileJsonObject, "CrateSpot");
                    CrateSpot? crateSpot;
                    if (crateSpotJsonNode is null)
                    {
                        crateSpot = null;
                    }
                    else
                    {
                        var crateSpotType = Enum.Parse<CrateSpotType>(crateSpotJsonNode.GetValue<string>());
                        crateSpot = new CrateSpot { Type = crateSpotType };
                    }


                    var tileObjectJsonNode = GetPropertyValue(tileJsonObject, "TileObject");
                    TileObject? tileObject;
                    if (tileObjectJsonNode is null)
                    {
                        tileObject = null;
                    }
                    else
                    {
                        var tileObjectJsonObject = tileObjectJsonNode.AsObject();
                        var objectType = GetNotNullPropertyValue(tileObjectJsonObject, "ObjectType").GetValue<string>();

                        switch (objectType)
                        {
                            case "Wall":
                            {
                                var type = GetEnumPropertyValue<WallType>(tileObjectJsonObject, "Type");
                                tileObject = new Wall { Type = type };
                                break;
                            }
                            case "Crate":
                            {
                                var type = GetEnumPropertyValue<CrateType>(tileObjectJsonObject, "Type");
                                var crateSpotType = GetEnumPropertyValue<CrateSpotType>(tileObjectJsonObject, "CrateSpotType");
                                var counter = GetNotNullPropertyValue(tileObjectJsonObject, "Counter").GetValue<int>();
                                var isHidden = GetNotNullPropertyValue(tileObjectJsonObject, "IsHidden").GetValue<bool>();
                                tileObject = new Crate { Type = type, CrateSpotType = crateSpotType, Counter = counter, IsHidden = isHidden };
                                break;
                            }
                            case "Player":
                                tileObject = new Player();
                                break;
                            default:
                                throw new LevelDataCorruptedException();
                        }
                    }

                    var tile = level.GetTile(x, y);
                    tile.Ground = ground;
                    tile.CrateSpot = crateSpot;
                    tile.TileObject = tileObject;
                }

                return level;
            }
            catch (LevelDataCorruptedException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new LevelDataCorruptedException(exception);
            }
        }

        private static void Upgrade(JsonNode levelJsonNode)
        {
            var levelJsonObject = levelJsonNode.AsObject();
            var versionString = GetNotNullPropertyValue(levelJsonObject, "Version").GetValue<string>();
            var version = Version.Parse(versionString);

            // Upgrade to v0.8
            if (version < new Version(0, 8))
            {
                var tilesJsonNode = GetNotNullPropertyValue(levelJsonObject, "Tiles");

                foreach (var tileJsonNode in tilesJsonNode.AsArray())
                {
                    if (tileJsonNode is null)
                    {
                        throw new LevelDataCorruptedException();
                    }

                    var tileJsonObject = tileJsonNode.AsObject();
                    var tileObjectJsonNode = GetPropertyValue(tileJsonObject, "TileObject");
                    if (tileObjectJsonNode != null)
                    {
                        var tileObjectJsonObject = tileObjectJsonNode.AsObject();
                        var objectType = GetNotNullPropertyValue(tileObjectJsonObject, "ObjectType").GetValue<string>();

                        if (objectType == "Crate")
                        {
                            tileObjectJsonObject.Add("Counter", 5);
                        }
                    }
                }

                version = new Version(0, 8);
            }

            // Upgrade to v0.9
            if (version < new Version(0, 9))
            {
                var tilesJsonNode = GetNotNullPropertyValue(levelJsonObject, "Tiles");

                foreach (var tileJsonNode in tilesJsonNode.AsArray())
                {
                    if (tileJsonNode is null)
                    {
                        throw new LevelDataCorruptedException();
                    }

                    var tileJsonObject = tileJsonNode.AsObject();
                    var tileObjectJsonNode = GetPropertyValue(tileJsonObject, "TileObject");
                    if (tileObjectJsonNode != null)
                    {
                        var tileObjectJsonObject = tileObjectJsonNode.AsObject();
                        var objectType = GetNotNullPropertyValue(tileObjectJsonObject, "ObjectType").GetValue<string>();

                        if (objectType == "Crate")
                        {
                            tileObjectJsonObject.Add("IsHidden", false);
                        }
                    }
                }

                version = new Version(0, 9);
            }
        }

        public static Level CreateEmptyLevelValidForGameMode()
        {
            var level = new Level();
            level.GetTile(0, 0).TileObject = new Player();
            level.GetTile(1, 0).CrateSpot = new CrateSpot();
            return level;
        }

        private static JsonNode? GetPropertyValue(JsonObject jsonObject, string propertyName)
        {
            if (!jsonObject.TryGetPropertyValue(propertyName, out var jsonNode))
            {
                throw new LevelDataCorruptedException();
            }

            return jsonNode;
        }

        private static JsonNode GetNotNullPropertyValue(JsonObject jsonObject, string propertyName)
        {
            if (!jsonObject.TryGetPropertyValue(propertyName, out var jsonNode))
            {
                throw new LevelDataCorruptedException();
            }

            if (jsonNode is null)
            {
                throw new LevelDataCorruptedException();
            }

            return jsonNode;
        }

        private static TEnum GetEnumPropertyValue<TEnum>(JsonObject jsonObject, string propertyName) where TEnum : struct
        {
            return Enum.Parse<TEnum>(GetNotNullPropertyValue(jsonObject, propertyName).GetValue<string>());
        }
    }

    public sealed class LevelDataCorruptedException : Exception
    {
        private const string ExceptionMessage = "Level data is corrupted.";

        public LevelDataCorruptedException() : base(ExceptionMessage)
        {
        }

        public LevelDataCorruptedException(Exception innerException) : base(ExceptionMessage, innerException)
        {
        }
    }
}