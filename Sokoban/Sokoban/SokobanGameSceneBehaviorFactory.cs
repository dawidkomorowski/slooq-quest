using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;
using Sokoban.Core.LevelModel;

namespace Sokoban
{
    internal sealed class SokobanGameSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "SokobanGame";
        private readonly EntityFactory _entityFactory;

        public SokobanGameSceneBehaviorFactory(EntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public string BehaviorName => SceneBehaviorName;

        public SceneBehavior Create(Scene scene) => new SokobanGameSceneBehavior(scene, _entityFactory);

        private sealed class SokobanGameSceneBehavior : SceneBehavior
        {
            private readonly EntityFactory _entityFactory;

            public override string Name => SceneBehaviorName;

            public SokobanGameSceneBehavior(Scene scene, EntityFactory entityFactory) : base(scene)
            {
                _entityFactory = entityFactory;
            }

            protected override void OnLoaded()
            {
                var level = Level.CreateTestLevel();

                _entityFactory.CreateCamera(Scene);
                _entityFactory.CreateBackground(Scene);

                for (var x = 0; x < level.Width; x++)
                {
                    for (var y = 0; y < level.Height; y++)
                    {
                        var tile = level.GetTile(x, y);
                        _entityFactory.CreateGround(Scene, tile);

                        if (tile.TileObject is Wall wall)
                        {
                            _entityFactory.CreateWall(Scene, wall);
                        }

                        if (tile.TileObject is Player player)
                        {
                            _entityFactory.CreatePlayer(Scene, player);
                        }
                    }
                }
            }
        }
    }
}