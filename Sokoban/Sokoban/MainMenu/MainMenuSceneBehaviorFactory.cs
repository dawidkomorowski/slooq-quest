using System;
using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;
using Sokoban.VisualEffects;

namespace Sokoban.MainMenu
{
    internal sealed class MainMenuSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "MainMenu";

        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly MainMenuEntityFactory _mainMenuEntityFactory;

        public MainMenuSceneBehaviorFactory(CoreEntityFactory coreEntityFactory, MainMenuEntityFactory mainMenuEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
            _mainMenuEntityFactory = mainMenuEntityFactory;
        }

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new MainMenuSceneBehavior(scene, _coreEntityFactory, _mainMenuEntityFactory);

        private sealed class MainMenuSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;
            private readonly MainMenuEntityFactory _mainMenuEntityFactory;

            public MainMenuSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory, MainMenuEntityFactory mainMenuEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _mainMenuEntityFactory = mainMenuEntityFactory;
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                var cameraEntity = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = cameraEntity;

                var title = _mainMenuEntityFactory.CreateTitle(Scene);
                title.Parent = cameraEntity;

                var fadeInOutEntity = Scene.CreateEntity();
                var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                fadeInOutComponent.Duration = TimeSpan.FromSeconds(1);
            }
        }
    }
}