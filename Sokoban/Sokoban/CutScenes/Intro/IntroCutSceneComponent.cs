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

namespace Sokoban.CutScenes.Intro
{
    internal sealed class IntroCutSceneComponent : BehaviorComponent
    {
        private int _stage = 0;
        private TileObjectPositionComponent _playerTileObjectPositionComponent = null!;
        private SpeechBalloonComponent _speechBalloonComponent = null!;
        private Wait _wait = new Wait(TimeSpan.Zero);

        public IntroCutSceneComponent(Entity entity) : base(entity)
        {
        }

        public GameMode? GameMode { get; set; }

        public override void OnStart()
        {
            _playerTileObjectPositionComponent = Entity.Scene.AllEntities
                .Single(e => e.HasComponent<TileObjectPositionComponent>() && e.GetComponent<TileObjectPositionComponent>().TileObject is Player)
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
                    _speechBalloonComponent.SetPosition(0, -200);
                    _speechBalloonComponent.SetDimensions(100, 50);
                    _speechBalloonComponent.SetTextLine1("???", -25, 17);
                    _speechBalloonComponent.Show();
                    _stage = 3;
                    break;
                case 3:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 4;
                    }

                    break;
                case 4:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 5;
                    break;
                case 5:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 6;
                    }

                    break;
                case 6:
                    _speechBalloonComponent.Hide();
                    _stage = 7;
                    break;
                case 7:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 8;
                    }

                    break;
                case 8:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 9;
                    break;
                case 9:
                    if (_wait.Update(gameTime.DeltaTime))
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
                    GameMode.MoveUp();
                    _stage = 17;
                    break;
                case 17:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 18;
                    }

                    break;
                case 18:
                    GameMode.MoveRight();
                    _stage = 19;
                    break;
                case 19:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 20;
                    }

                    break;
                case 20:
                    GameMode.MoveRight();
                    _stage = 21;
                    break;
                case 21:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 22;
                    }

                    break;
                case 22:
                    GameMode.MoveDown();
                    _stage = 23;
                    break;
                case 23:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 24;
                    }

                    break;
                case 24:
                    GameMode.MoveDown();
                    _stage = 25;
                    break;
                case 25:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 26;
                    }

                    break;
                case 26:
                    GameMode.MoveDown();
                    _stage = 27;
                    break;
                case 27:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 28;
                    }

                    break;
                case 28:
                    GameMode.MoveDown();
                    _stage = 29;
                    break;
                case 29:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 30;
                    }

                    break;
                case 30:
                    GameMode.MoveLeft();
                    _stage = 31;
                    break;
                case 31:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 32;
                    }

                    break;
                case 32:
                    GameMode.MoveLeft();
                    _stage = 33;
                    break;
                case 33:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 34;
                    }

                    break;
                case 34:
                    GameMode.MoveUp();
                    _stage = 35;
                    break;
                case 35:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 36;
                    }

                    break;
                case 36:
                    GameMode.MoveDown();
                    _stage = 37;
                    break;
                case 37:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 38;
                    }

                    break;
                case 38:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 39;
                    break;
                case 39:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 40;
                    }

                    break;
                case 40:
                    _speechBalloonComponent.SetPosition(60, -200);
                    _speechBalloonComponent.SetDimensions(260, 80);
                    _speechBalloonComponent.SetTextLine1("What happened?", -112, 30);
                    _speechBalloonComponent.SetTextLine2("Where am I?", -112, 5);
                    _speechBalloonComponent.Show();
                    _stage = 41;
                    break;
                case 41:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 42;
                    }

                    break;
                case 42:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 43;
                    break;
                case 43:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 44;
                    }

                    break;
                case 44:
                    _speechBalloonComponent.Hide();
                    _stage = 45;
                    break;
                case 45:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 46;
                    }

                    break;
                case 46:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 47;
                    break;
                case 47:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 180;
                    }

                    break;
                case 180:
                    _stage = 999;
                    break;
                case 999:
                    var fadeInOutEntity = Scene.CreateEntity();
                    var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                    fadeInOutComponent.Duration = TimeSpan.FromMilliseconds(250);
                    fadeInOutComponent.Mode = FadeInOutComponent.FadeMode.FadeOut;
                    fadeInOutComponent.Action = () =>
                    {
                        var e = Scene.CreateEntity();
                        var loadSceneComponent = e.CreateComponent<LoadSceneComponent>();
                        loadSceneComponent.SceneBehaviorName = "LevelSelectionMenu";
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

    internal sealed class IntroCutSceneComponentFactory : ComponentFactory<IntroCutSceneComponent>
    {
        protected override IntroCutSceneComponent CreateComponent(Entity entity) => new IntroCutSceneComponent(entity);
    }
}