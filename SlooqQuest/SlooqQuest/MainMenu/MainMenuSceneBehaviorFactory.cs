using System;
using Geisha.Engine.Core.SceneModel;
using SlooqQuest.Core;
using SlooqQuest.VisualEffects;

namespace SlooqQuest.MainMenu
{
    internal sealed class MainMenuSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "MainMenu";

        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly MainMenuEntityFactory _mainMenuEntityFactory;
        private readonly GameAudio _gameAudio;

        public MainMenuSceneBehaviorFactory(CoreEntityFactory coreEntityFactory, MainMenuEntityFactory mainMenuEntityFactory, GameAudio gameAudio)
        {
            _coreEntityFactory = coreEntityFactory;
            _mainMenuEntityFactory = mainMenuEntityFactory;
            _gameAudio = gameAudio;
        }

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new MainMenuSceneBehavior(scene, _coreEntityFactory, _mainMenuEntityFactory, _gameAudio);

        private sealed class MainMenuSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;
            private readonly MainMenuEntityFactory _mainMenuEntityFactory;
            private readonly GameAudio _gameAudio;

            public MainMenuSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory, MainMenuEntityFactory mainMenuEntityFactory,
                GameAudio gameAudio) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _mainMenuEntityFactory = mainMenuEntityFactory;
                _gameAudio = gameAudio;
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                var cameraEntity = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = cameraEntity;

                var title = _mainMenuEntityFactory.CreateTitle(Scene);
                title.Parent = cameraEntity;

                var mainMenu = _mainMenuEntityFactory.CreateMainMenu(Scene);
                mainMenu.Parent = cameraEntity;

                var fadeInOutEntity = Scene.CreateEntity();
                var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                fadeInOutComponent.Duration = TimeSpan.FromSeconds(1);

                _gameAudio.PlayMainMenuMusic();
            }
        }
    }
}