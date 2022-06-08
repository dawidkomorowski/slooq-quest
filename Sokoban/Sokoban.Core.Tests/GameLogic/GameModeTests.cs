using NUnit.Framework;
using Sokoban.Core.GameLogic;
using Sokoban.Core.LevelModel;

namespace Sokoban.Core.Tests.GameLogic
{
    [TestFixture]
    public class GameModeTests
    {
        [Test]
        public void Constructor_ShouldThrowException_GivenLevelWithNoPlayer()
        {
            // Arrange

            var level = new Level();

            // Act
            // Assert
            Assert.That(() => new GameMode(level), Throws.ArgumentException);
        }

        [Test]
        public void Constructor_ShouldThrowException_GivenLevelWithMultiplePlayers()
        {
            // Arrange

            var level = new Level();
            level.GetTile(5, 5).TileObject = new Player();
            level.GetTile(6, 6).TileObject = new Player();

            // Act
            // Assert
            Assert.That(() => new GameMode(level), Throws.ArgumentException);
        }

        [Test]
        public void Player_ShouldReturnPlayerInTheLevel()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;

            var gameMode = new GameMode(level);

            // Act
            // Assert
            Assert.That(gameMode.Player, Is.EqualTo(player));
        }

        [Test]
        public void MoveUp_ShouldMovePlayerUp_WhenTileIsEmpty()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveUp();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.Null);
            Assert.That(level.GetTile(5, 6).TileObject, Is.EqualTo(player));
        }

        [Test]
        public void MoveUp_ShouldNotMovePlayer_WhenPlayerIsAtTopBorderOfLevel()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(5, 9).TileObject = player;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveUp();

            // Assert
            Assert.That(level.GetTile(5, 9).TileObject, Is.EqualTo(player));
        }

        [Test]
        public void MoveUp_ShouldNotMovePlayer_WhenTileHasWall()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(5, 6).TileObject = new Wall();

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveUp();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.EqualTo(player));
        }

        [Test]
        public void MoveDown_ShouldMovePlayerDown_WhenTileIsEmpty()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveDown();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.Null);
            Assert.That(level.GetTile(5, 4).TileObject, Is.EqualTo(player));
        }

        [Test]
        public void MoveDown_ShouldNotMovePlayer_WhenPlayerIsAtBottomBorderOfLevel()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(5, 0).TileObject = player;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveDown();

            // Assert
            Assert.That(level.GetTile(5, 0).TileObject, Is.EqualTo(player));
        }

        [Test]
        public void MoveDown_ShouldNotMovePlayer_WhenTileHasWall()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(5, 4).TileObject = new Wall();

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveDown();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.EqualTo(player));
        }

        [Test]
        public void MoveLeft_ShouldMovePlayerLeft_WhenTileIsEmpty()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveLeft();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.Null);
            Assert.That(level.GetTile(4, 5).TileObject, Is.EqualTo(player));
        }

        [Test]
        public void MoveLeft_ShouldNotMovePlayer_WhenPlayerIsAtLeftBorderOfLevel()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(0, 5).TileObject = player;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveLeft();

            // Assert
            Assert.That(level.GetTile(0, 5).TileObject, Is.EqualTo(player));
        }

        [Test]
        public void MoveLeft_ShouldNotMovePlayer_WhenTileHasWall()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(4, 5).TileObject = new Wall();

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveLeft();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.EqualTo(player));
        }

        [Test]
        public void MoveRight_ShouldMovePlayerRight_WhenTileIsEmpty()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveRight();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.Null);
            Assert.That(level.GetTile(6, 5).TileObject, Is.EqualTo(player));
        }

        [Test]
        public void MoveRight_ShouldNotMovePlayer_WhenPlayerIsAtRightBorderOfLevel()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(9, 5).TileObject = player;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveRight();

            // Assert
            Assert.That(level.GetTile(9, 5).TileObject, Is.EqualTo(player));
        }

        [Test]
        public void MoveRight_ShouldNotMovePlayer_WhenTileHasWall()
        {
            // Arrange
            var player = new Player();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(6, 5).TileObject = new Wall();

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveRight();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.EqualTo(player));
        }
    }
}