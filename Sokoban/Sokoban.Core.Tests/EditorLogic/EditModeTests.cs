using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;
using Sokoban.Core.EditorLogic;
using Sokoban.Core.LevelModel;

namespace Sokoban.Core.Tests.EditorLogic
{
    [TestFixture]
    public class EditModeTests
    {
        [Test]
        public void Constructor_ShouldCreateEditModeWithSelectedTileAtX0Y0()
        {
            // Arrange
            var level = new Level();

            // Act
            var editMode = new EditMode(level);

            // Assert
            Assert.That(editMode.SelectedTile, Is.EqualTo(level.GetTile(0, 0)));
        }

        [Test]
        public void Move_ShouldSelectAppropriateTile()
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            // Act
            editMode.MoveUp();
            // Assert
            Assert.That(editMode.SelectedTile, Is.EqualTo(level.GetTile(0, 1)));

            // Act
            editMode.MoveRight();
            // Assert
            Assert.That(editMode.SelectedTile, Is.EqualTo(level.GetTile(1, 1)));

            // Act
            editMode.MoveDown();
            // Assert
            Assert.That(editMode.SelectedTile, Is.EqualTo(level.GetTile(1, 0)));

            // Act
            editMode.MoveLeft();
            // Assert
            Assert.That(editMode.SelectedTile, Is.EqualTo(level.GetTile(0, 0)));
        }

        [Test]
        public void Move_ShouldNotChangeSelectedTile_WhenMovingOutsideOfLevel()
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            // Act
            editMode.MoveDown();
            // Assert
            Assert.That(editMode.SelectedTile, Is.EqualTo(level.GetTile(0, 0)));

            // Act
            editMode.MoveLeft();
            // Assert
            Assert.That(editMode.SelectedTile, Is.EqualTo(level.GetTile(0, 0)));

            for (var i = 0; i < 9; i++)
            {
                editMode.MoveRight();
                editMode.MoveUp();
            }

            // Act
            editMode.MoveUp();
            // Assert
            Assert.That(editMode.SelectedTile, Is.EqualTo(level.GetTile(9, 9)));

            // Act
            editMode.MoveRight();
            // Assert
            Assert.That(editMode.SelectedTile, Is.EqualTo(level.GetTile(9, 9)));
        }

        [Test]
        public void Delete_ShouldRemoveTileObjectFromSelectedTile()
        {
            // Arrange
            var level = new Level();
            level.GetTile(0, 0).TileObject = new Wall();
            var editMode = new EditMode(level);

            // Act
            editMode.Delete();

            // Assert
            Assert.That(level.GetTile(0, 0).TileObject, Is.Null);
        }

        [Test]
        public void Delete_ShouldRemoveCrateSpotFromSelectedTile()
        {
            // Arrange
            var level = new Level();
            level.GetTile(0, 0).CrateSpot = new CrateSpot();
            var editMode = new EditMode(level);

            // Act
            editMode.Delete();

            // Assert
            Assert.That(level.GetTile(0, 0).CrateSpot, Is.Null);
        }

        [Test]
        public void Delete_ShouldInvokeLevelModifiedEvent()
        {
            // Arrange
            var level = new Level();
            level.GetTile(0, 0).TileObject = new Wall();
            var editMode = new EditMode(level);

            object? actualSender = null;
            EventArgs? actualArgs = null;
            editMode.LevelModified += (sender, args) =>
            {
                actualSender = sender;
                actualArgs = args;
            };

            // Act
            editMode.Delete();

            // Assert
            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        public static IEnumerable<TestCaseData> SetGroundTestCases => new[]
        {
            new TestCaseData(MakeExpression(e => e.SetGroundToNone()), Ground.None),
            new TestCaseData(MakeExpression(e => e.SetGroundToBrown()), Ground.Brown),
            new TestCaseData(MakeExpression(e => e.SetGroundToGreen()), Ground.Green),
            new TestCaseData(MakeExpression(e => e.SetGroundToGray()), Ground.Gray)
        };

        [TestCaseSource(nameof(SetGroundTestCases))]
        public void SetGroundToXXX_ShouldSetGroundXXXOnSelectedTile_AndShouldInvokeLevelModifiedEvent(Expression<Action<EditMode>> act, Ground expectedGround)
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            object? actualSender = null;
            EventArgs? actualArgs = null;
            editMode.LevelModified += (sender, args) =>
            {
                actualSender = sender;
                actualArgs = args;
            };

            // Act
            act.Compile().Invoke(editMode);

            // Assert
            Assert.That(level.GetTile(0, 0).Ground, Is.EqualTo(expectedGround));
            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        public static IEnumerable<TestCaseData> CreateWallTestCases => new[]
        {
            new TestCaseData(MakeExpression(e => e.CreateRedWall()), WallType.Red),
            new TestCaseData(MakeExpression(e => e.CreateRedGrayWall()), WallType.RedGray),
            new TestCaseData(MakeExpression(e => e.CreateGrayWall()), WallType.Gray),
            new TestCaseData(MakeExpression(e => e.CreateBrownWall()), WallType.Brown),
            new TestCaseData(MakeExpression(e => e.CreateRedWallTop()), WallType.TopRed),
            new TestCaseData(MakeExpression(e => e.CreateRedGrayWallTop()), WallType.TopRedGray),
            new TestCaseData(MakeExpression(e => e.CreateGrayWallTop()), WallType.TopGray),
            new TestCaseData(MakeExpression(e => e.CreateBrownWallTop()), WallType.TopBrown)
        };

        [TestCaseSource(nameof(CreateWallTestCases))]
        public void CreateXXXWall_ShouldCreateXXXWallOnSelectedTile_AndShouldInvokeLevelModifiedEvent(Expression<Action<EditMode>> act,
            WallType expectedWallType)
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            object? actualSender = null;
            EventArgs? actualArgs = null;
            editMode.LevelModified += (sender, args) =>
            {
                actualSender = sender;
                actualArgs = args;
            };

            // Act
            act.Compile().Invoke(editMode);

            // Assert
            Assert.That(level.GetTile(0, 0).TileObject, Is.Not.Null);
            Assert.That(level.GetTile(0, 0).TileObject, Is.TypeOf<Wall>());
            var wall = (Wall)level.GetTile(0, 0).TileObject!;
            Assert.That(wall.Type, Is.EqualTo(expectedWallType));

            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        public static IEnumerable<TestCaseData> CreateCrateTestCases => new[]
        {
            new TestCaseData(MakeExpression(e => e.CreateBrownCrate()), CrateType.Brown, CrateSpotType.Brown),
            new TestCaseData(MakeExpression(e => e.CreateRedCrate()), CrateType.Red, CrateSpotType.Red),
            new TestCaseData(MakeExpression(e => e.CreateBlueCrate()), CrateType.Blue, CrateSpotType.Blue),
            new TestCaseData(MakeExpression(e => e.CreateGreenCrate()), CrateType.Green, CrateSpotType.Green),
            new TestCaseData(MakeExpression(e => e.CreateGrayCrate()), CrateType.Gray, CrateSpotType.Gray)
        };

        [TestCaseSource(nameof(CreateCrateTestCases))]
        public void CreateXXXCrate_ShouldCreateXXXCrateOnSelectedTile_AndShouldInvokeLevelModifiedEvent(Expression<Action<EditMode>> act,
            CrateType expectedCrateType, CrateSpotType expectedCrateSpotType)
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            object? actualSender = null;
            EventArgs? actualArgs = null;
            editMode.LevelModified += (sender, args) =>
            {
                actualSender = sender;
                actualArgs = args;
            };

            // Act
            act.Compile().Invoke(editMode);

            // Assert
            Assert.That(level.GetTile(0, 0).TileObject, Is.Not.Null);
            Assert.That(level.GetTile(0, 0).TileObject, Is.TypeOf<Crate>());
            var crate = (Crate)level.GetTile(0, 0).TileObject!;
            Assert.That(crate.Type, Is.EqualTo(expectedCrateType));
            Assert.That(crate.CrateSpotType, Is.EqualTo(expectedCrateSpotType));

            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        public static IEnumerable<TestCaseData> CreateCrateSpotTestCases => new[]
        {
            new TestCaseData(MakeExpression(e => e.CreateBrownCrateSpot()), CrateSpotType.Brown),
            new TestCaseData(MakeExpression(e => e.CreateRedCrateSpot()), CrateSpotType.Red),
            new TestCaseData(MakeExpression(e => e.CreateBlueCrateSpot()), CrateSpotType.Blue),
            new TestCaseData(MakeExpression(e => e.CreateGreenCrateSpot()), CrateSpotType.Green),
            new TestCaseData(MakeExpression(e => e.CreateGrayCrateSpot()), CrateSpotType.Gray)
        };

        [TestCaseSource(nameof(CreateCrateSpotTestCases))]
        public void CreateXXXCrateSpot_ShouldCreateXXXCrateSpotOnSelectedTile_AndShouldInvokeLevelModifiedEvent(Expression<Action<EditMode>> act,
            CrateSpotType expectedCrateSpotType)
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            object? actualSender = null;
            EventArgs? actualArgs = null;
            editMode.LevelModified += (sender, args) =>
            {
                actualSender = sender;
                actualArgs = args;
            };

            // Act
            act.Compile().Invoke(editMode);

            // Assert
            Assert.That(level.GetTile(0, 0).CrateSpot, Is.Not.Null);
            Assert.That(level.GetTile(0, 0).CrateSpot!.Type, Is.EqualTo(expectedCrateSpotType));

            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        [Test]
        public void PlacePlayer_ShouldPlacePlayerOnSelectedTile_WhenThereIsNoPlayerInLevel()
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            // Act
            editMode.PlacePlayer();

            // Assert
            Assert.That(level.GetTile(0, 0).TileObject, Is.Not.Null);
            Assert.That(level.GetTile(0, 0).TileObject, Is.TypeOf<Player>());
        }

        [Test]
        public void PlacePlayer_ShouldPlacePlayerOnSelectedTile_WhenThereIsPlayerInLevel()
        {
            // Arrange
            var level = new Level();
            level.GetTile(9, 9).TileObject = new Player();
            var editMode = new EditMode(level);

            // Act
            editMode.PlacePlayer();

            // Assert
            Assert.That(level.GetTile(9, 9).TileObject, Is.Null);
            Assert.That(level.GetTile(0, 0).TileObject, Is.Not.Null);
            Assert.That(level.GetTile(0, 0).TileObject, Is.TypeOf<Player>());
        }

        [Test]
        public void PlacePlayer_ShouldInvokeLevelModifiedEvent()
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            object? actualSender = null;
            EventArgs? actualArgs = null;
            editMode.LevelModified += (sender, args) =>
            {
                actualSender = sender;
                actualArgs = args;
            };

            // Act
            editMode.PlacePlayer();

            // Assert
            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        [Test]
        public void SetCrateCounter_ShouldNotThrow_WhenSelectedTileHasNoCrate()
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            // Act
            // Assert
            Assert.That(() => editMode.SetCrateCounter(2), Throws.Nothing);
        }

        [Test]
        public void SetCrateCounter_ShouldSetCounterOfCrate_AndShouldInvokeLevelModifiedEvent()
        {
            // Arrange
            var level = new Level();
            var crate = new Crate { Type = CrateType.Blue, CrateSpotType = CrateSpotType.Blue, Counter = 1 };
            level.GetTile(0, 0).TileObject = crate;
            var editMode = new EditMode(level);

            object? actualSender = null;
            EventArgs? actualArgs = null;
            editMode.LevelModified += (sender, args) =>
            {
                actualSender = sender;
                actualArgs = args;
            };

            // Act
            editMode.SetCrateCounter(2);

            // Assert
            Assert.That(crate.Counter, Is.EqualTo(2));

            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        private static Expression<Action<EditMode>> MakeExpression(Expression<Action<EditMode>> expression) => expression;
    }
}