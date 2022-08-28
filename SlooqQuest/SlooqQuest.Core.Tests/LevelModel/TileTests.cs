using NUnit.Framework;
using SlooqQuest.Core.LevelModel;

namespace SlooqQuest.Core.Tests.LevelModel
{
    [TestFixture]
    public class TileTests
    {
        [Test]
        public void Constructor_ShouldCreateTileWithNullTileObject()
        {
            // Arrange
            // Act
            var tile = new Tile(0, 0);

            // Assert
            Assert.That(tile.TileObject, Is.Null);
        }

        [Test]
        public void Constructor_ShouldCreateTileWithNullCrateSpot()
        {
            // Arrange
            // Act
            var tile = new Tile(0, 0);

            // Assert
            Assert.That(tile.CrateSpot, Is.Null);
        }

        [Test]
        public void TileObject_Set_ShouldSetTileOfTileObjectToCurrentTile()
        {
            // Arrange
            var tile = new Tile(0, 0);
            var tileObject = new Player();

            // Act
            tile.TileObject = tileObject;

            // Assert
            Assert.That(tile.TileObject, Is.EqualTo(tileObject));
            Assert.That(tileObject.Tile, Is.EqualTo(tile));
        }

        [Test]
        public void TileObject_Set_ShouldSetTileObjectOfOtherTileToNull()
        {
            // Arrange
            var tile = new Tile(0, 0);
            var otherTile = new Tile(1, 1);
            var tileObject = new Player();

            otherTile.TileObject = tileObject;

            // Act
            tile.TileObject = tileObject;

            // Assert
            Assert.That(tile.TileObject, Is.EqualTo(tileObject));
            Assert.That(tileObject.Tile, Is.EqualTo(tile));
            Assert.That(otherTile.TileObject, Is.Null);
        }

        [Test]
        public void CrateSpot_Set_ShouldSetTileOfCrateSpotToCurrentTile()
        {
            // Arrange
            var tile = new Tile(0, 0);
            var crateSpot = new CrateSpot();

            // Act
            tile.CrateSpot = crateSpot;

            // Assert
            Assert.That(tile.CrateSpot, Is.EqualTo(crateSpot));
            Assert.That(crateSpot.Tile, Is.EqualTo(tile));
        }
    }
}