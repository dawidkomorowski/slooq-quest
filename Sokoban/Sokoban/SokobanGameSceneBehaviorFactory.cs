using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;

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
                _entityFactory.CreateCamera(Scene);
                _entityFactory.CreateGround(Scene);
            }
        }
    }
}