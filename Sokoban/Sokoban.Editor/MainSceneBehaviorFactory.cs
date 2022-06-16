using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;
using Sokoban.Core.EditorLogic;
using Sokoban.Core.LevelModel;
using Sokoban.Editor.UserInterface;

namespace Sokoban
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

            public MainSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory, UserInterfaceEntityFactory userInterfaceEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _userInterfaceEntityFactory = userInterfaceEntityFactory;
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                var cameraEntity = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = cameraEntity;

                var level = Level.CreateTestLevel();
                var editMode = new EditMode(level);

                var levelEntity = _coreEntityFactory.CreateLevel(Scene, editMode.Level);
                levelEntity.Parent = cameraEntity;

                var cursor = _userInterfaceEntityFactory.CreateCursor(Scene, editMode);
                cursor.Parent = levelEntity;
            }
        }
    }
}