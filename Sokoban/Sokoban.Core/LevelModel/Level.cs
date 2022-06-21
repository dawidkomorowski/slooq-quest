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
                                { "CrateSpotType", crate.CrateSpotType.ToString() }
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
                { "Version", Assembly.GetAssembly(typeof(Level))?.GetName().Version?.ToString(2) },
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
                                var type = GetEnumPropertyValue<CrateType>(tileObjectJsonObject, "CrateSpotType");
                                var crateSpotType = GetEnumPropertyValue<CrateSpotType>(tileObjectJsonObject, "CrateSpotType");
                                tileObject = new Crate { Type = type, CrateSpotType = crateSpotType };
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

        public static Level CreateEmptyLevelValidForGameMode()
        {
            var level = new Level();
            level.GetTile(0, 0).TileObject = new Player();
            return level;
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

            level.GetTile(6, 7).TileObject = new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown };
            level.GetTile(2, 4).TileObject = new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown };
            level.GetTile(3, 6).TileObject = new Crate { Type = CrateType.Red, CrateSpotType = CrateSpotType.Red };

            level.GetTile(1, 1).CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
            level.GetTile(8, 8).CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
            level.GetTile(8, 1).CrateSpot = new CrateSpot { Type = CrateSpotType.Red };

            level.GetTile(3, 3).TileObject = new Player();

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