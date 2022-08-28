using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Geisha.Engine.Animation;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.SceneModel;
using SlooqQuest.Assets;
using SlooqQuest.Core;
using SlooqQuest.Core.SceneLoading;
using SlooqQuest.VisualEffects;

namespace SlooqQuest
{
    internal sealed class MainSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "Main";

        private readonly IAssetStore _assetStore;
        private readonly CoreEntityFactory _coreEntityFactory;

        public MainSceneBehaviorFactory(CoreEntityFactory coreEntityFactory, IAssetStore assetStore)
        {
            _coreEntityFactory = coreEntityFactory;
            _assetStore = assetStore;
        }

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new MainSceneBehavior(scene, _coreEntityFactory, _assetStore);

        private sealed class MainSceneBehavior : SceneBehavior
        {
            private readonly IAssetStore _assetStore;
            private readonly CoreEntityFactory _coreEntityFactory;


            public MainSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory, IAssetStore assetStore) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _assetStore = assetStore;
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                Application.OpenForms[0].Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

                PreloadHeavyAssets();

                _coreEntityFactory.CreateCamera(Scene);

                var fadeInOutEntity = Scene.CreateEntity();
                var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                fadeInOutComponent.Duration = TimeSpan.FromSeconds(1000);

                var entity = Scene.CreateEntity();
                var loadSceneComponent = entity.CreateComponent<LoadSceneComponent>();
                loadSceneComponent.SceneBehaviorName = "MainMenu";
            }

            private void PreloadHeavyAssets()
            {
                _assetStore.GetAsset<SpriteAnimation>(SokobanAssetId.Animations.Smoke.Default);
            }
        }
    }
}