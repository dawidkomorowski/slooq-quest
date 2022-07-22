using System;
using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;
using Sokoban.VisualEffects;

namespace Sokoban.LevelSelectionMenu
{
    internal sealed class LevelSelectionMenuSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "LevelSelectionMenu";

        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly LevelSelectionMenuEntityFactory _levelSelectionMenuEntityFactory;

        public LevelSelectionMenuSceneBehaviorFactory(CoreEntityFactory coreEntityFactory, LevelSelectionMenuEntityFactory levelSelectionMenuEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
            _levelSelectionMenuEntityFactory = levelSelectionMenuEntityFactory;
        }

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new LevelSelectionSceneBehavior(scene, _coreEntityFactory, _levelSelectionMenuEntityFactory);

        private sealed class LevelSelectionSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;
            private readonly LevelSelectionMenuEntityFactory _levelSelectionMenuEntityFactory;

            public LevelSelectionSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory,
                LevelSelectionMenuEntityFactory levelSelectionMenuEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _levelSelectionMenuEntityFactory = levelSelectionMenuEntityFactory;
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
            }
        }
    }
}