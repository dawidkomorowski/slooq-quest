using System;
using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Sokoban.Core.Components;
using Sokoban.Core.GameLogic;
using Sokoban.Core.LevelModel;
using Sokoban.Core.SceneLoading;
using Sokoban.VisualEffects;

namespace Sokoban.CutScenes.IntroToFinal
{
    internal sealed class IntroToFinalCutSceneComponent : BehaviorComponent
    {
        private int _stage = 0;
        private TileObjectPositionComponent _playerTileObjectPositionComponent = null!;
        private SpeechBalloonComponent _speechBalloonComponent = null!;
        private Wait _wait = new Wait(TimeSpan.Zero);

        public IntroToFinalCutSceneComponent(Entity entity) : base(entity)
        {
        }

        public GameMode? GameMode { get; set; }

        public override void OnStart()
        {
            _playerTileObjectPositionComponent = Entity.Scene.AllEntities
                .Single(e =>
                    e.HasComponent<TileObjectPositionComponent>() &&
                    e.GetComponent<TileObjectPositionComponent>().TileObject is Player)
                .GetComponent<TileObjectPositionComponent>();

            var speechBalloonEntity = Scene.CreateEntity();
            _speechBalloonComponent = speechBalloonEntity.CreateComponent<SpeechBalloonComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (GameMode is null)
            {
                return;
            }

            switch (_stage)
            {
                case 0:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 1;
                    break;
                case 1:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 2;
                    }

                    break;
                case 2:
                    GameMode.MoveUp();
                    _stage = 3;
                    break;
                case 3:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 4;
                    }

                    break;
                case 4:
                    GameMode.MoveUp();
                    _stage = 5;
                    break;
                case 5:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 6;
                    }

                    break;
                case 6:
                    GameMode.MoveUp();
                    _stage = 7;
                    break;
                case 7:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 8;
                    }

                    break;
                case 8:
                    GameMode.MoveUp();
                    _stage = 9;
                    break;
                case 9:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 10;
                    }

                    break;
                case 10:
                    GameMode.MoveUp();
                    _stage = 11;
                    break;
                case 11:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 12;
                    }

                    break;
                case 12:
                    GameMode.MoveUp();
                    _stage = 13;
                    break;
                case 13:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 14;
                    }

                    break;
                case 14:
                    GameMode.MoveUp();
                    _stage = 15;
                    break;
                case 15:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 16;
                    }

                    break;
                case 16:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 17;
                    break;
                case 17:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 18;
                    }

                    break;
                case 18:
                    _speechBalloonComponent.SetPosition(-140, 160);
                    _speechBalloonComponent.SetDimensions(100, 50);
                    _speechBalloonComponent.SetTextLine1("!!!", -25, 17);
                    _speechBalloonComponent.Show();
                    _stage = 19;
                    break;
                case 19:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 20;
                    }

                    break;
                case 20:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 21;
                    break;
                case 21:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 22;
                    }

                    break;
                case 22:
                    _speechBalloonComponent.Hide();
                    _stage = 23;
                    break;
                case 23:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 24;
                    }

                    break;
                case 24:
                    GameMode.MoveRight();
                    _stage = 25;
                    break;
                case 25:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 26;
                    }

                    break;
                case 26:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 27;
                    break;
                case 27:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 28;
                    }

                    break;
                case 28:
                    _speechBalloonComponent.SetPosition(40, 160);
                    _speechBalloonComponent.SetDimensions(340, 100);
                    _speechBalloonComponent.SetTextLine1("Slooq! I have", -150, 40);
                    _speechBalloonComponent.SetTextLine2("finally found you.", -150, 15);
                    _speechBalloonComponent.Show();
                    _stage = 29;
                    break;
                case 29:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 30;
                    }

                    break;
                case 30:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 31;
                    break;
                case 31:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 32;
                    }

                    break;
                case 32:
                    _speechBalloonComponent.Hide();
                    _stage = 33;
                    break;
                case 33:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 34;
                    }

                    break;
                case 34:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 35;
                    break;
                case 35:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 36;
                    }

                    break;
                case 36:
                    _speechBalloonComponent.SetPosition(40, 160);
                    _speechBalloonComponent.SetDimensions(340, 100);
                    _speechBalloonComponent.SetTextLine1("I don't want to play with", -150, 40);
                    _speechBalloonComponent.SetTextLine2("you anymore. Let me out.", -150, 15);
                    _speechBalloonComponent.Show();
                    _stage = 37;
                    break;
                case 37:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 38;
                    }

                    break;
                case 38:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 39;
                    break;
                case 39:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 40;
                    }

                    break;
                case 40:
                    _speechBalloonComponent.Hide();
                    _stage = 41;
                    break;
                case 41:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 42;
                    }

                    break;
                case 42:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 43;
                    break;
                case 43:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 44;
                    }

                    break;
                case 44:
                    _stage = 999;
                    break;
                case 999:
                    var fadeInOutEntity = Scene.CreateEntity();
                    var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                    fadeInOutComponent.Duration = TimeSpan.FromSeconds(1);
                    fadeInOutComponent.Mode = FadeInOutComponent.FadeMode.FadeOut;
                    fadeInOutComponent.Action = () =>
                    {
                        var e = Scene.CreateEntity();
                        var loadSceneComponent = e.CreateComponent<LoadSceneComponent>();
                        loadSceneComponent.SceneBehaviorName = "MainMenu";
                    };
                    _stage = 1000;
                    break;
                case 1000:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_stage), _stage, "Invalid stage value.");
            }
        }
    }

    internal sealed class IntroToFinalCutSceneComponentFactory : ComponentFactory<IntroToFinalCutSceneComponent>
    {
        protected override IntroToFinalCutSceneComponent CreateComponent(Entity entity) => new IntroToFinalCutSceneComponent(entity);
    }
}