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

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(0), 45, "Powered by");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(0), 60, "Geisha Engine");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(2), 45, "Story");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(2), 60, "Dominik Wiech");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(3), 45, "Game Design");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(3), 60, "Dawid Komorowski");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(4), 45, "Level Design");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(4), 60, "Dominik Wiech");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(5), 45, "Game Programming");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(5), 60, "Dawid Komorowski");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(6), 45, "Quality Assurance");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(6), 60, "Dominik Wiech");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(7), 45, "Engine Programming");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(7), 60, "Dawid Komorowski");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(8) - 100, 45, "Third party assets");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(9), 45, "Graphics: Sokoban (pack)");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(9), 60, "by Kenney Vleugels (Kenney.nl)");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(10), 45, "Graphics: Animal pack");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(10), 60, "by Kenney Vleugels (Kenney.nl)");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(11), 45, "Graphics: Tiny Dungeon (1.0)");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(11), 60, "by Kenney Vleugels (Kenney.nl)");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(12), 45, "Graphics: Smoke Aura");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(12), 35, "by Beast (https://opengameart.org/content/smoke-aura)");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(13), 45, "Music: Menu Music");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(13), 35, "by mrpoly (https://opengameart.org/content/menu-music)");

                _creditsEntityFactory.CreateCreditsText(Scene, -550, HeaderPosition(14), 45, "Font: Burnstown Dam");
                _creditsEntityFactory.CreateCreditsText(Scene, -550, ContentPosition(14), 60, "by Typodermic Fonts", true);
            }

            private static double HeaderPosition(int i)
            {
                return -500 - 200 * i;
            }

            private static double ContentPosition(int i)
            {
                return -500 - 200 * i - 60;
            }
        }
    }
}