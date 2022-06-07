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

                _coreEntityFactory.CreateCamera(Scene);
                _coreEntityFactory.CreateBackground(Scene);

                for (var x = 0; x < level.Width; x++)
                {
                    for (var y = 0; y < level.Height; y++)
                    {
                        var tile = level.GetTile(x, y);
                        _coreEntityFactory.CreateGround(Scene, tile);

                        if (tile.TileObject is Wall wall)
                        {
                            _coreEntityFactory.CreateWall(Scene, wall);
                        }

                        if (tile.TileObject is Player player)
                        {
                            _coreEntityFactory.CreatePlayer(Scene, player);
                        }
                    }
                }

                _gameEntityFactory.CreatePlayerController(Scene, gameMode);
            }
        }
    }
}