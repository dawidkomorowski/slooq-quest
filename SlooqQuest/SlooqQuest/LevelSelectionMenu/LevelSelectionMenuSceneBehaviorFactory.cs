using System;
using Geisha.Engine.Core.SceneModel;
using SlooqQuest.Core;
using SlooqQuest.VisualEffects;

namespace SlooqQuest.LevelSelectionMenu
{
    internal sealed class LevelSelectionMenuSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "LevelSelectionMenu";

        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly LevelSelectionMenuEntityFactory _levelSelectionMenuEntityFactory;
        private readonly GameAudio _gameAudio;


        public LevelSelectionMenuSceneBehaviorFactory(CoreEntityFactory coreEntityFactory, LevelSelectionMenuEntityFactory levelSelectionMenuEntityFactory,
            GameAudio gameAudio)
        {
            _coreEntityFactory = coreEntityFactory;
            _levelSelectionMenuEntityFactory = levelSelectionMenuEntityFactory;
            _gameAudio = gameAudio;
        }

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new LevelSelectionSceneBehavior(scene, _coreEntityFactory, _levelSelectionMenuEntityFactory, _gameAudio);

        private sealed class LevelSelectionSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;
            private readonly LevelSelectionMenuEntityFactory _levelSelectionMenuEntityFactory;
            private readonly GameAudio _gameAudio;

            public LevelSelectionSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory,
                LevelSelectionMenuEntityFactory levelSelectionMenuEntityFactory, GameAudio gameAudio) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _levelSelectionMenuEntityFactory = levelSelectionMenuEntityFactory;
                _gameAudio = gameAudio;
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                var cameraEntity = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = cameraEntity;

                var levelSelectionMenu = _levelSelectionMenuEntityFactory.CreateLevelSelectionMenu(Scene);
                levelSelectionMenu.Parent = cameraEntity;

                var fadeInOutEntity = Scene.CreateEntity();
                var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                fadeInOutComponent.Duration = TimeSpan.FromMilliseconds(250);

                _gameAudio.PlayMainMenuMusic();
            }
        }
    }
}