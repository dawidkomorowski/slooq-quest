using Geisha.Engine.Core.SceneModel;
using Sokoban.Core.SceneLoading;

namespace Sokoban
{
    internal sealed class MainSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "Main";

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new MainSceneBehavior(scene);

        private sealed class MainSceneBehavior : SceneBehavior
        {
            public MainSceneBehavior(Scene scene) : base(scene)
            {
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                var entity = Scene.CreateEntity();
                var loadSceneComponent = entity.CreateComponent<LoadSceneComponent>();
                loadSceneComponent.SceneBehaviorName = "MainMenu";
            }
        }
    }
}