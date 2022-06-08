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

        #region MoveUp

        [Test]
        public void MoveUp_ShouldMovePlayerUp_WhenTargetTileIsEmpty()
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
        public void MoveUp_ShouldNotMovePlayer_WhenTargetTileIsOutsideOfLevel()
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
        public void MoveUp_ShouldNotMovePlayer_WhenTargetTileHasWall()
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
        public void MoveUp_ShouldMovePlayerAndCrateUp_WhenTargetTileHasCrate_AndNextTileIsEmpty()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(5, 6).TileObject = crate;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveUp();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.Null);
            Assert.That(level.GetTile(5, 6).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(5, 7).TileObject, Is.EqualTo(crate));
        }

        [Test]
        public void MoveUp_ShouldNotMovePlayerAndCrate_WhenTargetTileHasCrate_AndNextTileIsOutsideOfLevel()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(5, 8).TileObject = player;
            level.GetTile(5, 9).TileObject = crate;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveUp();

            // Assert
            Assert.That(level.GetTile(5, 8).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(5, 9).TileObject, Is.EqualTo(crate));
        }

        [Test]
        public void MoveUp_ShouldNotMovePlayerAndCrate_WhenTargetTileHasCrate_AndNextTileIsNotEmpty()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(5, 6).TileObject = crate;
            level.GetTile(5, 7).TileObject = new Wall();

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveUp();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(5, 6).TileObject, Is.EqualTo(crate));
        }

        #endregion

        #region MoveDown

        [Test]
        public void MoveDown_ShouldMovePlayerDown_WhenTargetTileIsEmpty()
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
        public void MoveDown_ShouldNotMovePlayer_WhenTargetTileIsOutsideOfLevel()
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
        public void MoveDown_ShouldNotMovePlayer_WhenTargetTileHasWall()
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
        public void MoveDown_ShouldMovePlayerAndCrateDown_WhenTargetTileHasCrate_AndNextTileIsEmpty()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(5, 4).TileObject = crate;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveDown();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.Null);
            Assert.That(level.GetTile(5, 4).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(5, 3).TileObject, Is.EqualTo(crate));
        }

        [Test]
        public void MoveDown_ShouldNotMovePlayerAndCrate_WhenTargetTileHasCrate_AndNextTileIsOutsideOfLevel()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(5, 1).TileObject = player;
            level.GetTile(5, 0).TileObject = crate;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveDown();

            // Assert
            Assert.That(level.GetTile(5, 1).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(5, 0).TileObject, Is.EqualTo(crate));
        }

        [Test]
        public void MoveDown_ShouldNotMovePlayerAndCrate_WhenTargetTileHasCrate_AndNextTileIsNotEmpty()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(5, 4).TileObject = crate;
            level.GetTile(5, 3).TileObject = new Wall();

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveDown();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(5, 4).TileObject, Is.EqualTo(crate));
        }

        #endregion

        #region MoveLeft

        [Test]
        public void MoveLeft_ShouldMovePlayerLeft_WhenTargetTileIsEmpty()
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
        public void MoveLeft_ShouldNotMovePlayer_WhenTargetTileIsOutsideOfLevel()
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
        public void MoveLeft_ShouldNotMovePlayer_WhenTargetTileHasWall()
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
        public void MoveLeft_ShouldMovePlayerAndCrateLeft_WhenTargetTileHasCrate_AndNextTileIsEmpty()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(4, 5).TileObject = crate;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveLeft();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.Null);
            Assert.That(level.GetTile(4, 5).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(3, 5).TileObject, Is.EqualTo(crate));
        }

        [Test]
        public void MoveLeft_ShouldNotMovePlayerAndCrate_WhenTargetTileHasCrate_AndNextTileIsOutsideOfLevel()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(1, 5).TileObject = player;
            level.GetTile(0, 5).TileObject = crate;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveLeft();

            // Assert
            Assert.That(level.GetTile(1, 5).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(0, 5).TileObject, Is.EqualTo(crate));
        }

        [Test]
        public void MoveLeft_ShouldNotMovePlayerAndCrate_WhenTargetTileHasCrate_AndNextTileIsNotEmpty()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(4, 5).TileObject = crate;
            level.GetTile(3, 5).TileObject = new Wall();

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveLeft();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(4, 5).TileObject, Is.EqualTo(crate));
        }

        #endregion

        #region MoveRight

        [Test]
        public void MoveRight_ShouldMovePlayerRight_WhenTargetTileIsEmpty()
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
        public void MoveRight_ShouldNotMovePlayer_WhenTargetTileIsOutsideOfLevel()
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
        public void MoveRight_ShouldNotMovePlayer_WhenTargetTileHasWall()
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

        [Test]
        public void MoveRight_ShouldMovePlayerAndCrateRight_WhenTargetTileHasCrate_AndNextTileIsEmpty()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(6, 5).TileObject = crate;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveRight();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.Null);
            Assert.That(level.GetTile(6, 5).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(7, 5).TileObject, Is.EqualTo(crate));
        }

        [Test]
        public void MoveRight_ShouldNotMovePlayerAndCrate_WhenTargetTileHasCrate_AndNextTileIsOutsideOfLevel()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(8, 5).TileObject = player;
            level.GetTile(9, 5).TileObject = crate;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveRight();

            // Assert
            Assert.That(level.GetTile(8, 5).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(9, 5).TileObject, Is.EqualTo(crate));
        }

        [Test]
        public void MoveRight_ShouldNotMovePlayerAndCrate_WhenTargetTileHasCrate_AndNextTileIsNotEmpty()
        {
            // Arrange
            var player = new Player();
            var crate = new Crate();

            var level = new Level();
            level.GetTile(5, 5).TileObject = player;
            level.GetTile(6, 5).TileObject = crate;
            level.GetTile(7, 5).TileObject = new Wall();

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveRight();

            // Assert
            Assert.That(level.GetTile(5, 5).TileObject, Is.EqualTo(player));
            Assert.That(level.GetTile(6, 5).TileObject, Is.EqualTo(crate));
        }

        #endregion
    }
}