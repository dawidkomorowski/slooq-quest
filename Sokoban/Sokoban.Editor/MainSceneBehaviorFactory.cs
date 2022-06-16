using System;
using System.IO;
using System.Windows.Forms;
using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;
using Sokoban.Core.EditorLogic;
using Sokoban.Core.LevelModel;
using Sokoban.Editor.UserInterface;

namespace Sokoban.Editor
{
    internal sealed class MainSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "Main";
        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly UserInterfaceEntityFactory _userInterfaceEntityFactory;

        public MainSceneBehaviorFactory(CoreEntityFactory coreEntityFactory, UserInterfaceEntityFactory userInterfaceEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
            _userInterfaceEntityFactory = userInterfaceEntityFactory;
        }

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new MainSceneBehavior(scene, _coreEntityFactory, _userInterfaceEntityFactory);

        private sealed class MainSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;
            private readonly UserInterfaceEntityFactory _userInterfaceEntityFactory;
            private EditMode _editMode = null!;

            public MainSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory, UserInterfaceEntityFactory userInterfaceEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _userInterfaceEntityFactory = userInterfaceEntityFactory;
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                Application.OpenForms[0].KeyDown += OnKeyDown;

                var cameraEntity = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = cameraEntity;

                _editMode = new EditMode(LoadDefaultLevel());
                _editMode.LevelModified += EditModeOnLevelModified;

                var levelEntity = _coreEntityFactory.CreateLevel(Scene, _editMode.Level);
                levelEntity.Parent = cameraEntity;

                var cursor = _userInterfaceEntityFactory.CreateCursor(Scene, _editMode);
                cursor.Parent = levelEntity;

                var help = _userInterfaceEntityFactory.CreateHelp(Scene);
                help.Parent = cameraEntity;
            }

            private Level LoadDefaultLevel()
            {
                var defaultLevelPath = Path.Join("Levels", "NewLevel.sokoban-level");
                if (File.Exists(defaultLevelPath))
                {
                    var serializedLevel = File.ReadAllText(defaultLevelPath);
                    return Level.Deserialize(serializedLevel);
                }
                else
                {
                    return new Level();
                }
            }

            private void EditModeOnLevelModified(object? sender, EventArgs e)
            {
                File.WriteAllText(Path.Join("Levels", "NewLevel.sokoban-level"), _editMode.Level.Serialize());
            }
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F10)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}