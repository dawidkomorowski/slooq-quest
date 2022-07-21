using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;

namespace Sokoban.MainMenu
{
    internal sealed class MainMenuSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "MainMenu";

        private readonly CoreEntityFactory _coreEntityFactory;

        public MainMenuSceneBehaviorFactory(CoreEntityFactory coreEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
        }

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new MainMenuSceneBehavior(scene, _coreEntityFactory);

        private sealed class MainMenuSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;

            public MainMenuSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                var cameraEntity = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = cameraEntity;
            }
        }
    }
}