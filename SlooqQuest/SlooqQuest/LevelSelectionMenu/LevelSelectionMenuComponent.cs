﻿using System;
using System.Collections.Generic;
using System.Linq;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using SlooqQuest.Core.SceneLoading;
using SlooqQuest.VisualEffects;

namespace SlooqQuest.LevelSelectionMenu
{
    internal sealed class LevelSelectionMenuComponent : BehaviorComponent
    {
        private const int Middle = 4;
        private readonly GameState _gameState;
        private readonly List<TextRendererComponent> _labels = new List<TextRendererComponent>();
        private InputComponent _inputComponent = null!;

        public LevelSelectionMenuComponent(Entity entity, GameState gameState) : base(entity)
        {
            _gameState = gameState;
        }

        public LevelSelectionModel? LevelSelectionModel { get; set; }

        public override void OnStart()
        {
            _inputComponent = Entity.GetComponent<InputComponent>();

            if (LevelSelectionModel is null)
            {
                return;
            }

            _inputComponent.BindAction("SelectPreviousLevel", LevelSelectionModel.SelectPreviousLevel);
            _inputComponent.BindAction("SelectNextLevel", LevelSelectionModel.SelectNextLevel);
            _inputComponent.BindAction("SelectLevel", () =>
            {
                if (!LevelSelectionModel.SelectedLevel.IsUnlocked)
                {
                    return;
                }

                _inputComponent.InputMapping = null;

                _gameState.CurrentLevel = LevelSelectionModel.SelectedLevel;

                var fadeInOutEntity = Scene.CreateEntity();
                var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                fadeInOutComponent.Duration = TimeSpan.FromMilliseconds(250);
                fadeInOutComponent.Mode = FadeInOutComponent.FadeMode.FadeOut;
                fadeInOutComponent.Action = () =>
                {
                    var e = Scene.CreateEntity();
                    var loadSceneComponent = e.CreateComponent<LoadSceneComponent>();

                    if (_gameState.CurrentLevel == _gameState.Levels.Last())
                    {
                        loadSceneComponent.SceneBehaviorName = "IntroToFinalCutScene";
                    }
                    else
                    {
                        loadSceneComponent.SceneBehaviorName = "SlooqQuestGame";

                    }
                };
            });
            _inputComponent.BindAction("Back", () =>
            {
                _inputComponent.InputMapping = null;

                var fadeInOutEntity = Scene.CreateEntity();
                var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                fadeInOutComponent.Duration = TimeSpan.FromMilliseconds(250);
                fadeInOutComponent.Mode = FadeInOutComponent.FadeMode.FadeOut;
                fadeInOutComponent.Action = () =>
                {
                    var e = Scene.CreateEntity();
                    var loadSceneComponent = e.CreateComponent<LoadSceneComponent>();
                    loadSceneComponent.SceneBehaviorName = "MainMenu";
                };
            });

            for (var i = 0; i < Middle; i++)
            {
                _labels.Add(CreateLabel(300 - i * 60, 40));
            }

            _labels.Add(CreateLabel(450 - Middle * 100, 70));

            for (var i = Middle + 1; i < 2 * Middle + 1; i++)
            {
                _labels.Add(CreateLabel(225 - i * 60, 40));
            }
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (LevelSelectionModel is null)
            {
                return;
            }

            foreach (var label in _labels)
            {
                label.Text = string.Empty;
            }

            _labels[Middle].Text = LevelSelectionModel.SelectedLevel.Name;
            _labels[Middle].Color = GetLabelColor(LevelSelectionModel.SelectedLevel.IsUnlocked);

            var i = 0;
            foreach (var levelInfo in LevelSelectionModel.PreviousLevels.Reverse().Take(Middle))
            {
                _labels[Middle - 1 - i].Text = levelInfo.Name;
                _labels[Middle - 1 - i].Color = GetLabelColor(levelInfo.IsUnlocked);
                i++;
            }

            i = Middle + 1;
            foreach (var levelInfo in LevelSelectionModel.NextLevels.Take(Middle))
            {
                _labels[i].Text = levelInfo.Name;
                _labels[i].Color = GetLabelColor(levelInfo.IsUnlocked);
                i++;
            }
        }

        private TextRendererComponent CreateLabel(double y, double fontSize)
        {
            var entity = Entity.CreateChildEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(-600, y);

            var textRendererComponent = entity.CreateComponent<TextRendererComponent>();
            textRendererComponent.Color = Color.FromArgb(255, 255, 255, 255);
            textRendererComponent.FontSize = FontSize.FromDips(fontSize);
            textRendererComponent.SortingLayerName = "UI";

            return textRendererComponent;
        }

        private static Color GetLabelColor(bool isUnlocked)
        {
            return isUnlocked ? Color.FromArgb(255, 255, 255, 255) : Color.FromArgb(255, 150, 150, 150);
        }
    }

    internal sealed class LevelSelectionMenuComponentFactory : ComponentFactory<LevelSelectionMenuComponent>
    {
        private readonly GameState _gameState;

        public LevelSelectionMenuComponentFactory(GameState gameState)
        {
            _gameState = gameState;
        }

        protected override LevelSelectionMenuComponent CreateComponent(Entity entity) => new LevelSelectionMenuComponent(entity, _gameState);
    }
}