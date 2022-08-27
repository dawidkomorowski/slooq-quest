using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;
using Sokoban.VisualEffects;
using System;

namespace Sokoban.Credits
{
    internal sealed class CreditsSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "Credits";
        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly CreditsEntityFactory _creditsEntityFactory;

        public CreditsSceneBehaviorFactory(CoreEntityFactory coreEntityFactory, CreditsEntityFactory creditsEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
            _creditsEntityFactory = creditsEntityFactory;
        }

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new CreditsSceneBehavior(scene, _coreEntityFactory, _creditsEntityFactory);

        private sealed class CreditsSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;
            private readonly CreditsEntityFactory _creditsEntityFactory;

            public CreditsSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory, CreditsEntityFactory creditsEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _creditsEntityFactory = creditsEntityFactory;
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                var cameraEntity = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = cameraEntity;

                var fadeInOutEntity = Scene.CreateEntity();
                var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                fadeInOutComponent.Duration = TimeSpan.FromMilliseconds(250);

                _creditsEntityFactory.CreateExitCreditsEntity(Scene);
            }
        }
    }
}