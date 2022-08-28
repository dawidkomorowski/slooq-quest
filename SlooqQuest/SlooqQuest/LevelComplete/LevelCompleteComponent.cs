using System;
using System.Linq;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;
using SlooqQuest.Core.Components;
using SlooqQuest.Core.SceneLoading;
using SlooqQuest.InGameMenu;
using SlooqQuest.VisualEffects;

namespace SlooqQuest.LevelComplete
{
    internal sealed class LevelCompleteComponent : BehaviorComponent
    {
        private readonly GameState _gameState;
        private RectangleRendererComponent _background = null!;
        private TextRendererComponent _text = null!;

        private const int TargetBackgroundAlpha = 150;
        private const int TargetTextAlpha = 255;

        private readonly TimeSpan _animationTime = TimeSpan.FromMilliseconds(1000);
        private TimeSpan _animationTimer = TimeSpan.Zero;

        private LevelCompleteState _state = LevelCompleteState.Invisible;

        public LevelCompleteComponent(Entity entity, GameState gameState) : base(entity)
        {
            _gameState = gameState;
        }

        public override void OnStart()
        {
            _background = Entity.GetComponent<RectangleRendererComponent>();
            _text = Entity.Children[0].GetComponent<TextRendererComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            switch (_state)
            {
                case LevelCompleteState.Invisible:
                    if (_gameState.GameMode.IsLevelComplete())
                    {
                        _gameState.OnLevelComplete();
                        ShowLevelComplete();
                    }

                    break;
                case LevelCompleteState.Animating:
                    Animate(gameTime);
                    break;
                case LevelCompleteState.Visible:
                    GoBackToLevelSelection();
                    break;
                case LevelCompleteState.WaitingForExit:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ShowLevelComplete()
        {
            _state = LevelCompleteState.Animating;

            var playerControllerComponent = FindPlayerControllerComponent();
            playerControllerComponent.Enabled = false;

            var inGameMenu = FindInGameMenuComponent();
            inGameMenu.Enabled = false;
        }

        private void Animate(GameTime gameTime)
        {
            _animationTimer += gameTime.DeltaTime;

            var animationRatio = _animationTimer / _animationTime;

            if (animationRatio >= 1.0)
            {
                _animationTimer = TimeSpan.Zero;
                animationRatio = 1.0;
                _state = LevelCompleteState.Visible;
            }

            var backgroundAlpha = animationRatio * (TargetBackgroundAlpha / 255.0);
            var textAlpha = animationRatio * (TargetTextAlpha / 255.0);

            SetAlpha(backgroundAlpha, textAlpha);
        }

        private void GoBackToLevelSelection()
        {
            _state = LevelCompleteState.WaitingForExit;

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
                    loadSceneComponent.SceneBehaviorName = "FinalCutScene";
                }
                else
                {
                    loadSceneComponent.SceneBehaviorName = "LevelSelectionMenu";
                }
            };
        }

        private void SetAlpha(double background, double text)
        {
            _background.Color = Color.FromArgb(background, _background.Color.DoubleR, _background.Color.DoubleG, _background.Color.DoubleB);
            _text.Color = Color.FromArgb(text, _text.Color.DoubleR, _text.Color.DoubleG, _text.Color.DoubleB);
        }

        private PlayerControllerComponent FindPlayerControllerComponent()
        {
            return Scene.AllEntities.Single(e => e.HasComponent<PlayerControllerComponent>()).GetComponent<PlayerControllerComponent>();
        }

        private InGameMenuComponent FindInGameMenuComponent()
        {
            return Entity.Scene.AllEntities.Single(e => e.HasComponent<InGameMenuComponent>())
                .GetComponent<InGameMenuComponent>();
        }

        private enum LevelCompleteState
        {
            Invisible,
            Animating,
            Visible,
            WaitingForExit
        }
    }

    internal sealed class LevelCompleteComponentFactory : ComponentFactory<LevelCompleteComponent>
    {
        private readonly GameState _gameState;

        public LevelCompleteComponentFactory(GameState gameState)
        {
            _gameState = gameState;
        }

        protected override LevelCompleteComponent CreateComponent(Entity entity) => new LevelCompleteComponent(entity, _gameState);
    }
}