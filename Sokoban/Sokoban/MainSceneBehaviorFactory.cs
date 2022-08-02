using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;
using Sokoban.Core.SceneLoading;
using Sokoban.VisualEffects;

namespace Sokoban
{
    internal sealed class MainSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "Main";

        private readonly CoreEntityFactory _coreEntityFactory;

        public MainSceneBehaviorFactory(CoreEntityFactory coreEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
        }

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new MainSceneBehavior(scene, _coreEntityFactory);

        private sealed class MainSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;


            public MainSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                Application.OpenForms[0].Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

                _coreEntityFactory.CreateCamera(Scene);

                var fadeInOutEntity = Scene.CreateEntity();
                var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                fadeInOutComponent.Duration = TimeSpan.FromSeconds(1000);

                var entity = Scene.CreateEntity();
                var loadSceneComponent = entity.CreateComponent<LoadSceneComponent>();
                loadSceneComponent.SceneBehaviorName = "MainMenu";
            }
        }
    }
}