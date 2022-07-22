using System;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Input.Mapping;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using Sokoban.Assets;
using Sokoban.Core.SceneLoading;
using Sokoban.VisualEffects;

namespace Sokoban.MainMenu
{
    internal sealed class MainMenuEntityFactory
    {
        private readonly IAssetStore _assetStore;
        private readonly IEngineManager _engineManager;

        public MainMenuEntityFactory(IAssetStore assetStore, IEngineManager engineManager)
        {
            _assetStore = assetStore;
            _engineManager = engineManager;
        }

        public Entity CreateMainMenu(Scene scene)
        {
            var entity = scene.CreateEntity();

            entity.CreateComponent<Transform2DComponent>();

            var inputComponent = entity.CreateComponent<InputComponent>();
            inputComponent.InputMapping = new InputMapping
            {
                ActionMappings =
                {
                    new ActionMapping
                    {
                        ActionName = "OptionUp",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Up) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "OptionDown",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Down) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "SelectOption",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Enter) } }
                    }
                }
            };

            var mainMenuComponent = entity.CreateComponent<MainMenuComponent>();

            var options = new[]
            {
                new MainMenuOption { Index = 0, Text = "Continue", IsSelected = true },
                new MainMenuOption
                {
                    Index = 1, Text = "New Game", IsSelected = false, Action = () =>
                    {
                        inputComponent.InputMapping = null;

                        var fadeInOutEntity = scene.CreateEntity();
                        var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                        fadeInOutComponent.Duration = TimeSpan.FromMilliseconds(250);
                        fadeInOutComponent.Mode = FadeInOutComponent.FadeMode.FadeOut;
                        fadeInOutComponent.Action = () =>
                        {
                            var e = scene.CreateEntity();
                            var loadSceneComponent = e.CreateComponent<LoadSceneComponent>();
                            loadSceneComponent.SceneBehaviorName = "SokobanGame";
                        };
                    }
                },
                new MainMenuOption { Index = 2, Text = "Credits", IsSelected = false },
                new MainMenuOption { Index = 3, Text = "Exit", IsSelected = false, Action = () => _engineManager.ScheduleEngineShutdown() }
            };

            mainMenuComponent.MainMenuModel = new MainMenuModel(options);

            return entity;
        }

        public Entity CreateTitle(Scene scene)
        {
            var entity = scene.CreateEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(0, 200);
            transform2DComponent.Scale = new Vector2(0.6, 0.6);

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.MainMenu.Title);
            spriteRendererComponent.SortingLayerName = "UI";

            return entity;
        }
    }
}