using System;
using System.Collections.Generic;
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
using SlooqQuest.Assets;
using SlooqQuest.Core.SceneLoading;
using SlooqQuest.VisualEffects;

namespace SlooqQuest.MainMenu
{
    internal sealed class MainMenuEntityFactory
    {
        private readonly IAssetStore _assetStore;
        private readonly IEngineManager _engineManager;
        private readonly GameState _gameState;

        public MainMenuEntityFactory(IAssetStore assetStore, IEngineManager engineManager, GameState gameState)
        {
            _assetStore = assetStore;
            _engineManager = engineManager;
            _gameState = gameState;
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

            var options = new List<MainMenuOption>();
            var index = 0;

            if (_gameState.SavedGame != null)
            {
                options.Add(new MainMenuOption
                {
                    Index = index,
                    Text = "Continue",
                    IsSelected = true,
                    Action = () =>
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
                            loadSceneComponent.SceneBehaviorName = "LevelSelectionMenu";
                        };

                        _gameState.Continue();
                    }
                });
                index++;
            }

            options.Add(new MainMenuOption
            {
                Index = index,
                Text = "New Game",
                IsSelected = index == 0,
                Action = () =>
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
                        loadSceneComponent.SceneBehaviorName = "IntroCutScene";
                    };

                    _gameState.NewGame();
                }
            });
            index++;

            options.Add(new MainMenuOption
            {
                Index = index,
                Text = "Credits",
                IsSelected = false,
                Action = () =>
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
                        loadSceneComponent.SceneBehaviorName = "Credits";
                    };
                }
            });
            index++;

            options.Add(new MainMenuOption { Index = index, Text = "Exit", IsSelected = false, Action = () => _engineManager.ScheduleEngineShutdown() });

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
            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(SlooqQuestAssetId.Sprites.MainMenu.Title);
            spriteRendererComponent.SortingLayerName = "UI";

            return entity;
        }
    }
}