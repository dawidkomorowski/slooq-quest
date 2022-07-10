using System;
using System.IO;
using NUnit.Framework;
using Sokoban.Core.LevelModel;

namespace Sokoban.Core.Tests.LevelModel
{
    [TestFixture]
    public class LevelTests
    {
        [Test]
        public void Serialize_And_Deserialize_Level()
        {
            // Arrange
            var level = new Level();

            level.GetTile(0, 0).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(1, 0).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(2, 0).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(3, 0).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(4, 0).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(5, 0).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(6, 0).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(7, 0).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(8, 0).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(9, 0).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(9, 1).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(9, 2).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(9, 3).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(9, 4).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(9, 5).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(9, 6).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(9, 7).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(9, 8).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(9, 9).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(8, 9).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(7, 9).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(6, 9).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(5, 9).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(4, 9).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(3, 9).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(2, 9).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(1, 9).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(0, 9).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(0, 8).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(0, 7).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(0, 6).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(0, 5).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(0, 4).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(0, 3).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(0, 2).TileObject = new Wall { Type = WallType.RedGray };
            level.GetTile(0, 1).TileObject = new Wall { Type = WallType.RedGray };

            level.GetTile(1, 1).CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
            level.GetTile(8, 8).CrateSpot = new CrateSpot { Type = CrateSpotType.Red };

            level.GetTile(2, 2).TileObject = new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown };
            level.GetTile(7, 7).TileObject = new Crate { Type = CrateType.Red, CrateSpotType = CrateSpotType.Red };
            level.GetTile(4, 4).TileObject = new Crate { Type = CrateType.Blue, CrateSpotType = CrateSpotType.Blue, Counter = 2 };

            level.GetTile(5, 5).TileObject = new Player();

            // Act
            var serializedLevel = level.Serialize();
            var deserializedLevel = Level.Deserialize(serializedLevel);

            // Assert
            Assert.That(deserializedLevel.Width, Is.EqualTo(level.Width));
            Assert.That(deserializedLevel.Height, Is.EqualTo(level.Height));

            for (var x = 0; x < level.Width; x++)
            {
                for (var y = 0; y < level.Height; y++)
                {
                    var actual = deserializedLevel.GetTile(x, y);
                    var expected = level.GetTile(x, y);
                    AssertThatTilesAreEqual(actual, expected);
                }
            }
        }

        private static void AssertThatTilesAreEqual(Tile actual, Tile expected)
        {
            Assert.That(actual.Ground, Is.EqualTo(expected.Ground), actual.ToString);

            if (expected.CrateSpot is null)
            {
                Assert.That(actual.CrateSpot, Is.Null, actual.ToString);
            }
            else
            {
                Assert.That(actual.CrateSpot?.Type, Is.EqualTo(expected.CrateSpot.Type), actual.ToString);
            }

            switch (expected.TileObject)
            {
                case null:
                    Assert.That(actual.TileObject, Is.Null, actual.ToString);
                    break;
                case Player _:
                    Assert.That(actual.TileObject, Is.Not.Null, actual.ToString);
                    Assert.That(actual.TileObject, Is.TypeOf<Player>(), actual.ToString);
                    break;
                case Wall expectedWall:
                    Assert.That(actual.TileObject, Is.Not.Null, actual.ToString);
                    Assert.That(actual.TileObject, Is.TypeOf<Wall>(), actual.ToString);
                    var actualWall = (Wall)actual.TileObject!;
                    Assert.That(actualWall.Type, Is.EqualTo(expectedWall.Type), actual.ToString);
                    break;
                case Crate expectedCrate:
                    Assert.That(actual.TileObject, Is.Not.Null, actual.ToString);
                    Assert.That(actual.TileObject, Is.TypeOf<Crate>(), actual.ToString);
                    var actualCrate = (Crate)actual.TileObject!;
                    Assert.That(actualCrate.Type, Is.EqualTo(expectedCrate.Type), actual.ToString);
                    Assert.That(actualCrate.CrateSpotType, Is.EqualTo(expectedCrate.CrateSpotType), actual.ToString);
                    Assert.That(actualCrate.Counter, Is.EqualTo(expectedCrate.Counter), actual.ToString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unsupported {nameof(Tile.TileObject)} type: {expected.TileObject.GetType()}");
            }
        }

        [Test]
        public void Deserialize_ShouldBeSuccessful_ForAllCreatedLevels()
        {
            // Arrange
            var levelFilePaths = Directory.GetFiles("Levels");

            // Act
            // Assert
            foreach (var levelFilePath in levelFilePaths)
            {
                var levelData = File.ReadAllText(levelFilePath);
                var level = Level.Deserialize(levelData);

                Assert.That(level, Is.Not.Null);
            }
        }
    }
}