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

            public override string Name => SceneBehaviorName;

            public SokobanGameSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory, GameEntityFactory gameEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _gameEntityFactory = gameEntityFactory;
            }

            protected override void OnLoaded()
            {
                var level = Level.CreateTestLevel();

                var gameMode = new GameMode(level);

                var camera = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = camera;

                var levelEntity = _coreEntityFactory.CreateLevel(Scene, level);
                levelEntity.Parent = camera;

                _coreEntityFactory.CreatePlayerController(Scene, gameMode);
            }
        }
    }
}