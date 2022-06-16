using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;
using Sokoban.InGameMenu;
using Sokoban.LevelComplete;
using Sokoban.RestartLevel;

namespace Sokoban
{
    internal sealed class SokobanGameSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "SokobanGame";
        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly InGameMenuEntityFactory _inGameMenuEntityFactory;
        private readonly RestartLevelEntityFactory _restartLevelEntityFactory;
        private readonly LevelCompleteEntityFactory _levelCompleteEntityFactory;

        public SokobanGameSceneBehaviorFactory(CoreEntityFactory coreEntityFactory, InGameMenuEntityFactory inGameMenuEntityFactory,
            RestartLevelEntityFactory restartLevelEntityFactory, LevelCompleteEntityFactory levelCompleteEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
            _inGameMenuEntityFactory = inGameMenuEntityFactory;
            _restartLevelEntityFactory = restartLevelEntityFactory;
            _levelCompleteEntityFactory = levelCompleteEntityFactory;
        }

        public string BehaviorName => SceneBehaviorName;

        public SceneBehavior Create(Scene scene) =>
            new SokobanGameSceneBehavior(
                scene,
                _coreEntityFactory,
                _inGameMenuEntityFactory,
                _restartLevelEntityFactory,
                _levelCompleteEntityFactory
            );

        private sealed class SokobanGameSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;
            private readonly InGameMenuEntityFactory _inGameMenuEntityFactory;
            private readonly RestartLevelEntityFactory _restartLevelEntityFactory;
            private readonly LevelCompleteEntityFactory _levelCompleteEntityFactory;

            public override string Name => SceneBehaviorName;

            public SokobanGameSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory, InGameMenuEntityFactory inGameMenuEntityFactory,
                RestartLevelEntityFactory restartLevelEntityFactory, LevelCompleteEntityFactory levelCompleteEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _inGameMenuEntityFactory = inGameMenuEntityFactory;
                _restartLevelEntityFactory = restartLevelEntityFactory;
                _levelCompleteEntityFactory = levelCompleteEntityFactory;
            }

            protected override void OnLoaded()
            {
                var cameraEntity = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = cameraEntity;

                var inGameMenu = _inGameMenuEntityFactory.CreateInGameMenu(Scene);
                inGameMenu.Parent = cameraEntity;

                var levelCompleteEntity = _levelCompleteEntityFactory.CreateLevelCompleteEntity(Scene);
                levelCompleteEntity.Parent = cameraEntity;

                _restartLevelEntityFactory.CreateRestartLevelEntity(Scene);
            }
        }
    }
}