using System;
using System.Collections.Generic;
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
            new TestCaseData(new Action<EditMode>(e => e.SetGroundToNone()), Ground.None),
            new TestCaseData(new Action<EditMode>(e => e.SetGroundToBrown()), Ground.Brown),
            new TestCaseData(new Action<EditMode>(e => e.SetGroundToGreen()), Ground.Green),
            new TestCaseData(new Action<EditMode>(e => e.SetGroundToGray()), Ground.Gray)
        };

        [TestCaseSource(nameof(SetGroundTestCases))]
        public void SetGroundXYZ_ShouldSetGroundXYZOnSelectedTile_AndShouldInvokeLevelModifiedEvent(Action<EditMode> act, Ground expectedGround)
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
            act(editMode);

            // Assert
            Assert.That(level.GetTile(0, 0).Ground, Is.EqualTo(expectedGround));
            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        public static IEnumerable<TestCaseData> CreateWallTestCases => new[]
        {
            new TestCaseData(new Action<EditMode>(e => e.CreateRedWall()), WallType.Red),
            new TestCaseData(new Action<EditMode>(e => e.CreateRedGrayWall()), WallType.RedGray),
            new TestCaseData(new Action<EditMode>(e => e.CreateGrayWall()), WallType.Gray),
            new TestCaseData(new Action<EditMode>(e => e.CreateBrownWall()), WallType.Brown),
            new TestCaseData(new Action<EditMode>(e => e.CreateRedWallTop()), WallType.TopRed),
            new TestCaseData(new Action<EditMode>(e => e.CreateRedGrayWallTop()), WallType.TopRedGray),
            new TestCaseData(new Action<EditMode>(e => e.CreateGrayWallTop()), WallType.TopGray),
            new TestCaseData(new Action<EditMode>(e => e.CreateBrownWallTop()), WallType.TopBrown)
        };

        [TestCaseSource(nameof(CreateWallTestCases))]
        public void CreateXYZWall_ShouldCreateXYZWallOnSelectedTile_AndShouldInvokeLevelModifiedEvent(Action<EditMode> act, WallType expectedWallType)
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
            act(editMode);

            // Assert
            Assert.That(level.GetTile(0, 0).TileObject, Is.Not.Null);
            Assert.That(level.GetTile(0, 0).TileObject, Is.TypeOf<Wall>());
            var wall = (Wall)level.GetTile(0, 0).TileObject!;
            Assert.That(wall.Type, Is.EqualTo(expectedWallType));

            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        [Test]
        public void CreateBrownCrate_ShouldCreateBrownCrateOnSelectedTile()
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            // Act
            editMode.CreateBrownCrate();

            // Assert
            Assert.That(level.GetTile(0, 0).TileObject, Is.Not.Null);
            Assert.That(level.GetTile(0, 0).TileObject, Is.TypeOf<Crate>());
            var crate = (Crate)level.GetTile(0, 0).TileObject!;
            Assert.That(crate.Type, Is.EqualTo(CrateType.Brown));
            Assert.That(crate.CrateSpotType, Is.EqualTo(CrateSpotType.Brown));
        }

        [Test]
        public void CreateBrownCrate_ShouldInvokeLevelModifiedEvent()
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
            editMode.CreateBrownCrate();

            // Assert
            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        [Test]
        public void CreateRedCrate_ShouldCreateRedCrateOnSelectedTile()
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            // Act
            editMode.CreateRedCrate();

            // Assert
            Assert.That(level.GetTile(0, 0).TileObject, Is.Not.Null);
            Assert.That(level.GetTile(0, 0).TileObject, Is.TypeOf<Crate>());
            var crate = (Crate)level.GetTile(0, 0).TileObject!;
            Assert.That(crate.Type, Is.EqualTo(CrateType.Red));
            Assert.That(crate.CrateSpotType, Is.EqualTo(CrateSpotType.Red));
        }

        [Test]
        public void CreateRedCrate_ShouldInvokeLevelModifiedEvent()
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
            editMode.CreateRedCrate();

            // Assert
            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        [Test]
        public void CreateBrownCrateSpot_ShouldCreateBrownCrateSpotOnSelectedTile()
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            // Act
            editMode.CreateBrownCrateSpot();

            // Assert
            Assert.That(level.GetTile(0, 0).CrateSpot, Is.Not.Null);
            Assert.That(level.GetTile(0, 0).CrateSpot!.Type, Is.EqualTo(CrateSpotType.Brown));
        }

        [Test]
        public void CreateBrownCrateSpot_ShouldInvokeLevelModifiedEvent()
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
            editMode.CreateBrownCrateSpot();

            // Assert
            Assert.That(actualSender, Is.EqualTo(editMode));
            Assert.That(actualArgs, Is.EqualTo(EventArgs.Empty));
        }

        [Test]
        public void CreateRedCrateSpot_ShouldCreateRedCrateSpotOnSelectedTile()
        {
            // Arrange
            var level = new Level();
            var editMode = new EditMode(level);

            // Act
            editMode.CreateRedCrateSpot();

            // Assert
            Assert.That(level.GetTile(0, 0).CrateSpot, Is.Not.Null);
            Assert.That(level.GetTile(0, 0).CrateSpot!.Type, Is.EqualTo(CrateSpotType.Red));
        }

        [Test]
        public void CreateRedCrateSpot_ShouldInvokeLevelModifiedEvent()
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
            editMode.CreateRedCrateSpot();

            // Assert
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
    }
}