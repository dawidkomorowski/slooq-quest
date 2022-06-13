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
    }
}