using System;
using System.Linq;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using SlooqQuest.Core.Components;
using SlooqQuest.Core.GameLogic;
using SlooqQuest.Core.LevelModel;
using SlooqQuest.Core.SceneLoading;
using SlooqQuest.VisualEffects;

namespace SlooqQuest.CutScenes.Intro
{
    internal sealed class IntroCutSceneComponent : BehaviorComponent
    {
        private int _stage = 0;
        private TileObjectPositionComponent _playerTileObjectPositionComponent = null!;
        private Transform2DComponent _slooqTransform2DComponent = null!;
        private SpeechBalloonComponent _speechBalloonComponent = null!;
        private Wait _wait = new Wait(TimeSpan.Zero);
        private EaseOutBackAnimation _easeOutBackAnimation = new EaseOutBackAnimation(TimeSpan.Zero);

        public IntroCutSceneComponent(Entity entity) : base(entity)
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

            _slooqTransform2DComponent = Entity.Scene.AllEntities
                .Single(e =>
                    e.HasComponent<TileObjectPositionComponent>() &&
                    e.GetComponent<TileObjectPositionComponent>().TileObject is Crate &&
                    ((Crate)e.GetComponent<TileObjectPositionComponent>().TileObject!).Type is CrateType.Slooq)
                .GetComponent<Transform2DComponent>();

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
                    _slooqTransform2DComponent.Scale = Vector2.Zero;
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 1;
                    break;
                case 1:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 2;
                    }

                    break;
                case 2:
                    _speechBalloonComponent.SetPosition(-30, -200);
                    _speechBalloonComponent.SetDimensions(80, 40);
                    _speechBalloonComponent.SetTextLine1("???", -20, 15);
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
                    _speechBalloonComponent.SetDimensions(240, 80);
                    _speechBalloonComponent.SetTextLine1("What happened?", -110, 30);
                    _speechBalloonComponent.SetTextLine2("Where am I?", -110, 5);
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
                        _stage = 48;
                    }

                    break;
                case 48:
                    _easeOutBackAnimation = new EaseOutBackAnimation(TimeSpan.FromMilliseconds(200));
                    _stage = 49;
                    break;
                case 49:
                    if (_easeOutBackAnimation.Update(gameTime.DeltaTime))
                    {
                        var value = _easeOutBackAnimation.Value;
                        _slooqTransform2DComponent.Scale = new Vector2(value, value);
                        _stage = 50;
                    }
                    else
                    {
                        var value = _easeOutBackAnimation.Value;
                        _slooqTransform2DComponent.Scale = new Vector2(value, value);
                    }

                    break;
                case 50:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 51;
                    break;
                case 51:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 52;
                    }

                    break;
                case 52:
                    _speechBalloonComponent.SetPosition(-50, 220);
                    _speechBalloonComponent.SetDimensions(220, 60);
                    _speechBalloonComponent.SetTextLine1("Hello friend.", -100, 22);
                    _speechBalloonComponent.Show();
                    _stage = 53;
                    break;
                case 53:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 54;
                    }

                    break;
                case 54:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 55;
                    break;
                case 55:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 56;
                    }

                    break;
                case 56:
                    _speechBalloonComponent.Hide();
                    _stage = 57;
                    break;
                case 57:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 58;
                    }

                    break;
                case 58:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 59;
                    break;
                case 59:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 60;
                    }

                    break;
                case 60:
                    _speechBalloonComponent.SetPosition(50, -200);
                    _speechBalloonComponent.SetDimensions(220, 60);
                    _speechBalloonComponent.SetTextLine1("Who are you?", -100, 22);
                    _speechBalloonComponent.Show();
                    _stage = 61;
                    break;
                case 61:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 62;
                    }

                    break;
                case 62:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 63;
                    break;
                case 63:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 64;
                    }

                    break;
                case 64:
                    _speechBalloonComponent.Hide();
                    _stage = 65;
                    break;
                case 65:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 66;
                    }

                    break;
                case 66:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 67;
                    break;
                case 67:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 68;
                    }

                    break;
                case 68:
                    _speechBalloonComponent.SetPosition(-140, 220);
                    _speechBalloonComponent.SetDimensions(380, 80);
                    _speechBalloonComponent.SetTextLine1("I'm Slooq and I locked", -180, 30);
                    _speechBalloonComponent.SetTextLine2("you here to play with you.", -180, 5);
                    _speechBalloonComponent.Show();
                    _stage = 69;
                    break;
                case 69:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 70;
                    }

                    break;
                case 70:
                    _wait = new Wait(TimeSpan.FromSeconds(4));
                    _stage = 71;
                    break;
                case 71:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 72;
                    }

                    break;
                case 72:
                    _speechBalloonComponent.Hide();
                    _stage = 73;
                    break;
                case 73:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 74;
                    }

                    break;
                case 74:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 75;
                    break;
                case 75:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 76;
                    }

                    break;
                case 76:
                    _speechBalloonComponent.SetPosition(60, -200);
                    _speechBalloonComponent.SetDimensions(250, 80);
                    _speechBalloonComponent.SetTextLine1("But I don’t want", -110, 30);
                    _speechBalloonComponent.SetTextLine2("to play!!!", -110, 5);
                    _speechBalloonComponent.Show();
                    _stage = 77;
                    break;
                case 77:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 78;
                    }

                    break;
                case 78:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 79;
                    break;
                case 79:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 80;
                    }

                    break;
                case 80:
                    _speechBalloonComponent.Hide();
                    _stage = 81;
                    break;
                case 81:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 82;
                    }

                    break;
                case 82:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 83;
                    break;
                case 83:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 84;
                    }

                    break;
                case 84:
                    _speechBalloonComponent.SetPosition(-100, 220);
                    _speechBalloonComponent.SetDimensions(300, 80);
                    _speechBalloonComponent.SetTextLine1("It's a pity because ", -135, 30);
                    _speechBalloonComponent.SetTextLine2("you have to!", -135, 5);
                    _speechBalloonComponent.Show();
                    _stage = 85;
                    break;
                case 85:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 86;
                    }

                    break;
                case 86:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 87;
                    break;
                case 87:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 88;
                    }

                    break;
                case 88:
                    _speechBalloonComponent.Hide();
                    _stage = 89;
                    break;
                case 89:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 90;
                    }

                    break;
                case 90:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 91;
                    break;
                case 91:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 92;
                    }

                    break;
                case 92:
                    _speechBalloonComponent.SetPosition(-30, -200);
                    _speechBalloonComponent.SetDimensions(80, 40);
                    _speechBalloonComponent.SetTextLine1("...", -20, 15);
                    _speechBalloonComponent.Show();
                    _stage = 93;
                    break;
                case 93:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 94;
                    }

                    break;
                case 94:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 95;
                    break;
                case 95:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 96;
                    }

                    break;
                case 96:
                    _speechBalloonComponent.Hide();
                    _stage = 97;
                    break;
                case 97:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 98;
                    }

                    break;
                case 98:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 99;
                    break;
                case 99:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 100;
                    }

                    break;
                case 100:
                    _speechBalloonComponent.SetPosition(-180, 220);
                    _speechBalloonComponent.SetDimensions(450, 80);
                    _speechBalloonComponent.SetTextLine1("Can you see these crates?", -210, 30);
                    _speechBalloonComponent.SetTextLine2("Put them in the right places.", -210, 5);
                    _speechBalloonComponent.Show();
                    _stage = 101;
                    break;
                case 101:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 102;
                    }

                    break;
                case 102:
                    _wait = new Wait(TimeSpan.FromSeconds(4));
                    _stage = 103;
                    break;
                case 103:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 104;
                    }

                    break;
                case 104:
                    _speechBalloonComponent.Hide();
                    _stage = 105;
                    break;
                case 105:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 106;
                    }

                    break;
                case 106:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 107;
                    break;
                case 107:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 108;
                    }

                    break;
                case 108:
                    _speechBalloonComponent.SetPosition(30, -200);
                    _speechBalloonComponent.SetDimensions(180, 60);
                    _speechBalloonComponent.SetTextLine1("What for?", -75, 22);
                    _speechBalloonComponent.Show();
                    _stage = 109;
                    break;
                case 109:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 110;
                    }

                    break;
                case 110:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 111;
                    break;
                case 111:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 112;
                    }

                    break;
                case 112:
                    _speechBalloonComponent.Hide();
                    _stage = 113;
                    break;
                case 113:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 114;
                    }

                    break;
                case 114:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 115;
                    break;
                case 115:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 116;
                    }

                    break;
                case 116:
                    _speechBalloonComponent.SetPosition(-100, 220);
                    _speechBalloonComponent.SetDimensions(300, 80);
                    _speechBalloonComponent.SetTextLine1("Put them! Put them!", -140, 30);
                    _speechBalloonComponent.SetTextLine2("Put them!", -140, 5);
                    _speechBalloonComponent.Show();
                    _stage = 117;
                    break;
                case 117:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 118;
                    }

                    break;
                case 118:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 119;
                    break;
                case 119:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 120;
                    }

                    break;
                case 120:
                    _speechBalloonComponent.Hide();
                    _stage = 121;
                    break;
                case 121:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 122;
                    }

                    break;
                case 122:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 123;
                    break;
                case 123:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 124;
                    }

                    break;
                case 124:
                    _easeOutBackAnimation = new EaseOutBackAnimation(TimeSpan.FromMilliseconds(200), true);
                    _stage = 125;
                    break;
                case 125:
                    if (_easeOutBackAnimation.Update(gameTime.DeltaTime))
                    {
                        var value = _easeOutBackAnimation.Value;
                        _slooqTransform2DComponent.Scale = new Vector2(value, value);
                        _stage = 126;
                    }
                    else
                    {
                        var value = _easeOutBackAnimation.Value;
                        _slooqTransform2DComponent.Scale = new Vector2(value, value);
                    }

                    break;
                case 126:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 127;
                    break;
                case 127:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 128;
                    }

                    break;
                case 128:
                    _speechBalloonComponent.SetPosition(10, -200);
                    _speechBalloonComponent.SetDimensions(140, 60);
                    _speechBalloonComponent.SetTextLine1("But...", -60, 22);
                    _speechBalloonComponent.Show();
                    _stage = 129;
                    break;
                case 129:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 130;
                    }

                    break;
                case 130:
                    _wait = new Wait(TimeSpan.FromSeconds(2));
                    _stage = 131;
                    break;
                case 131:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 132;
                    }

                    break;
                case 132:
                    _speechBalloonComponent.Hide();
                    _stage = 133;
                    break;
                case 133:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 134;
                    }

                    break;
                case 134:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 135;
                    break;
                case 135:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 136;
                    }

                    break;
                case 136:
                    _speechBalloonComponent.SetPosition(110, -220);
                    _speechBalloonComponent.SetDimensions(350, 80);
                    _speechBalloonComponent.SetTextLine1("Okay, I'll just do what", -160, 30);
                    _speechBalloonComponent.SetTextLine2("Slooq tells me to do.", -160, 5);
                    _speechBalloonComponent.Show();
                    _stage = 137;
                    break;
                case 137:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 138;
                    }

                    break;
                case 138:
                    _wait = new Wait(TimeSpan.FromSeconds(4));
                    _stage = 139;
                    break;
                case 139:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 140;
                    }

                    break;
                case 140:
                    _speechBalloonComponent.Hide();
                    _stage = 141;
                    break;
                case 141:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 142;
                    }

                    break;
                case 142:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 143;
                    break;
                case 143:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 999;
                    }

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