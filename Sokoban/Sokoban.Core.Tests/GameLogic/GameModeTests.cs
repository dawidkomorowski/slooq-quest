using System;
using System.Collections.Generic;
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
        public void Constructor_ShouldNotThrow_GivenEmptyLevelValidForGameMode()
        {
            // Arrange
            var level = Level.CreateEmptyLevelValidForGameMode();

            // Act
            // Assert
            Assert.That(() => new GameMode(level), Throws.Nothing);
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

        #region IsLevelComplete

        public static IEnumerable<TestCaseData> IsLevelComplete_TestCases() => new[]
        {
            new TestCaseData(new Action<Level>(level =>
            {
                level.GetTile(5, 5).TileObject = new Player();

                level.GetTile(1, 1).CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
                level.GetTile(2, 2).TileObject = new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown };
            }), false).SetName("Single brown spot without brown crate."),
            new TestCaseData(new Action<Level>(level =>
            {
                level.GetTile(5, 5).TileObject = new Player();

                level.GetTile(1, 1).CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
                level.GetTile(1, 1).TileObject = new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown };
            }), true).SetName("Single brown spot with brown crate."),
            new TestCaseData(new Action<Level>(level =>
            {
                level.GetTile(5, 5).TileObject = new Player();

                level.GetTile(1, 1).CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
                level.GetTile(1, 1).TileObject = new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown };

                level.GetTile(2, 2).CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
                level.GetTile(3, 3).TileObject = new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown };
            }), false).SetName("One brown spot with brown crate and another brown spot without brown crate."),
            new TestCaseData(new Action<Level>(level =>
            {
                level.GetTile(5, 5).TileObject = new Player();

                level.GetTile(1, 1).CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
                level.GetTile(1, 1).TileObject = new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown };

                level.GetTile(2, 2).CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
                level.GetTile(2, 2).TileObject = new Crate { Type = CrateType.Brown, CrateSpotType = CrateSpotType.Brown };
            }), true).SetName("Two brown spots with brown crates."),
            new TestCaseData(new Action<Level>(level =>
            {
                level.GetTile(5, 5).TileObject = new Player();

                level.GetTile(1, 1).CrateSpot = new CrateSpot { Type = CrateSpotType.Brown };
                level.GetTile(1, 1).TileObject = new Crate { Type = CrateType.Red, CrateSpotType = CrateSpotType.Red };
            }), false).SetName("Single brown spot with red crate."),
        };

        [TestCaseSource(nameof(IsLevelComplete_TestCases))]
        public void IsLevelComplete_ShouldReturnTrue_WhenAllCreateSpots_HaveMatchingCrates(Action<Level> setupAction, bool expectedResult)
        {
            // Arrange
            var level = new Level();

            setupAction(level);

            var gameMode = new GameMode(level);

            // Act
            // Assert
            Assert.That(gameMode.IsLevelComplete, Is.EqualTo(expectedResult));
        }

        #endregion

        #region Red Crate Mechanics

        [Test]
        public void RedCrate_ShouldBecomeLocked_WhenItEntersRedCrateSpot()
        {
            // Arrange
            var crate = new Crate { Type = CrateType.Red, CrateSpotType = CrateSpotType.Red };

            var level = new Level();
            level.GetTile(5, 5).TileObject = new Player();
            level.GetTile(6, 5).TileObject = crate;
            level.GetTile(7, 5).CrateSpot = new CrateSpot { Type = CrateSpotType.Red };

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveRight();

            // Assert
            Assert.That(level.GetTile(7, 5).TileObject, Is.EqualTo(crate));
            Assert.That(crate.IsLocked, Is.True);
        }

        [Test]
        public void RedCrate_ShouldNotBeMoved_WhenItIsLocked()
        {
            // Arrange
            var crate = new Crate { Type = CrateType.Red, CrateSpotType = CrateSpotType.Red };

            var level = new Level();
            level.GetTile(5, 5).TileObject = new Player();
            level.GetTile(6, 5).TileObject = crate;
            level.GetTile(7, 5).CrateSpot = new CrateSpot { Type = CrateSpotType.Red };

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveRight();
            gameMode.MoveRight();

            // Assert
            Assert.That(level.GetTile(7, 5).TileObject, Is.EqualTo(crate));
        }

        #endregion

        #region Blue Crate Mechanics

        [Test]
        public void BlueCrate_ShouldDecrementCounter_WhenItIsMoved()
        {
            // Arrange
            var crate = new Crate { Type = CrateType.Blue, CrateSpotType = CrateSpotType.Blue, Counter = 5 };

            var level = new Level();
            level.GetTile(5, 5).TileObject = new Player();
            level.GetTile(6, 5).TileObject = crate;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveRight();

            // Assert
            Assert.That(level.GetTile(7, 5).TileObject, Is.EqualTo(crate));
            Assert.That(crate.Counter, Is.EqualTo(4));
        }

        [Test]
        public void BlueCrate_ShouldBecomeLocked_WhenItsCounterDropsToZero()
        {
            // Arrange
            var crate = new Crate { Type = CrateType.Blue, CrateSpotType = CrateSpotType.Blue, Counter = 2 };

            var level = new Level();
            level.GetTile(5, 5).TileObject = new Player();
            level.GetTile(6, 5).TileObject = crate;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveRight();
            gameMode.MoveRight();

            // Assert
            Assert.That(level.GetTile(8, 5).TileObject, Is.EqualTo(crate));
            Assert.That(crate.IsLocked, Is.True);
        }

        [Test]
        public void BlueCrate_ShouldNotBeMoved_WhenItIsLocked()
        {
            // Arrange
            var crate = new Crate { Type = CrateType.Blue, CrateSpotType = CrateSpotType.Blue, Counter = 1 };

            var level = new Level();
            level.GetTile(5, 5).TileObject = new Player();
            level.GetTile(6, 5).TileObject = crate;

            var gameMode = new GameMode(level);

            // Act
            gameMode.MoveRight();
            gameMode.MoveRight();

            // Assert
            Assert.That(level.GetTile(7, 5).TileObject, Is.EqualTo(crate));
        }

        #endregion
    }
}