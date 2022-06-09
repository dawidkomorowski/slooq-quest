using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;
using Sokoban.Core.GameLogic;
using Sokoban.Core.LevelModel;

namespace Sokoban
{
    internal sealed class SokobanGameSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "SokobanGame";
        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly GameEntityFactory _gameEntityFactory;

        public SokobanGameSceneBehaviorFactory(CoreEntityFactory coreEntityFactory, GameEntityFactory gameEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
            _gameEntityFactory = gameEntityFactory;
        }

        public string BehaviorName => SceneBehaviorName;

        public SceneBehavior Create(Scene scene) => new SokobanGameSceneBehavior(scene, _coreEntityFactory, _gameEntityFactory);

        private sealed class SokobanGameSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;
            private readonly GameEntityFactory _gameEntityFactory;
            private Entity _cameraEntity = null!;
            private Entity? _levelEntity;
            private Entity? _playerControllerEntity;

            public override string Name => SceneBehaviorName;

            public SokobanGameSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory, GameEntityFactory gameEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _gameEntityFactory = gameEntityFactory;
            }

            protected override void OnLoaded()
            {
                _cameraEntity = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = _cameraEntity;

                var inGameMenu = _gameEntityFactory.CreateInGameMenu(Scene);
                inGameMenu.Parent = _cameraEntity;

                _gameEntityFactory.CreateRestartLevelEntity(Scene);
            }
        }
    }
}