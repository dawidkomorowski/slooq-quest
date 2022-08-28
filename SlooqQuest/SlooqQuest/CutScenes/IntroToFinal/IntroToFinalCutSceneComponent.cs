using System;
using System.Linq;
using Geisha.Common.Math;
using Geisha.Engine.Animation;
using Geisha.Engine.Animation.Components;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using SlooqQuest.Assets;
using SlooqQuest.Core.Components;
using SlooqQuest.Core.GameLogic;
using SlooqQuest.Core.LevelModel;
using SlooqQuest.Core.SceneLoading;
using SlooqQuest.VisualEffects;

namespace SlooqQuest.CutScenes.IntroToFinal
{
    internal sealed class IntroToFinalCutSceneComponent : BehaviorComponent
    {
        private const double TargetSmokeScale = 20;
        private readonly IAssetStore _assetStore;
        private int _stage = 0;
        private TileObjectPositionComponent _playerTileObjectPositionComponent = null!;
        private SpeechBalloonComponent _speechBalloonComponent = null!;
        private Transform2DComponent _smokeTransform2DComponent = null!;
        private Wait _wait = new Wait(TimeSpan.Zero);

        public IntroToFinalCutSceneComponent(Entity entity, IAssetStore assetStore) : base(entity)
        {
            _assetStore = assetStore;
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

            var smokeEntity = Scene.CreateEntity();
            _smokeTransform2DComponent = smokeEntity.CreateComponent<Transform2DComponent>();
            _smokeTransform2DComponent.Scale = new Vector2(0, 0);
            var smokeSpriteRendererComponent = smokeEntity.CreateComponent<SpriteRendererComponent>();
            smokeSpriteRendererComponent.SortingLayerName = "VFX";
            var smokeSpriteAnimationComponent = smokeEntity.CreateComponent<SpriteAnimationComponent>();
            smokeSpriteAnimationComponent.AddAnimation("Default", _assetStore.GetAsset<SpriteAnimation>(SlooqQuestAssetId.Animations.Smoke.Default));
            smokeSpriteAnimationComponent.PlayInLoop = true;
            smokeSpriteAnimationComponent.PlayAnimation("Default");
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
                    _speechBalloonComponent.SetPosition(-150, 170);
                    _speechBalloonComponent.SetDimensions(80, 40);
                    _speechBalloonComponent.SetTextLine1("!!!", -20, 15);
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
                    _speechBalloonComponent.SetPosition(-50, 230);
                    _speechBalloonComponent.SetDimensions(300, 80);
                    _speechBalloonComponent.SetTextLine1("Slooq! I have", -135, 30);
                    _speechBalloonComponent.SetTextLine2("finally found you.", -135, 5);
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
                    _speechBalloonComponent.SetPosition(-30, 230);
                    _speechBalloonComponent.SetDimensions(370, 80);
                    _speechBalloonComponent.SetTextLine1("I don't want to play with", -170, 30);
                    _speechBalloonComponent.SetTextLine2("you anymore. Let me out.", -170, 5);
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
                    _wait = new Wait(TimeSpan.FromSeconds(4));
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
                    _speechBalloonComponent.SetPosition(160, 240);
                    _speechBalloonComponent.SetDimensions(370, 80);
                    _speechBalloonComponent.SetTextLine1("No! I have a great time", -170, 30);
                    _speechBalloonComponent.SetTextLine2("playing with you.", -170, 5);
                    _speechBalloonComponent.Show();
                    _stage = 45;
                    break;
                case 45:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 46;
                    }

                    break;
                case 46:
                    _wait = new Wait(TimeSpan.FromSeconds(4));
                    _stage = 47;
                    break;
                case 47:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 48;
                    }

                    break;
                case 48:
                    _speechBalloonComponent.Hide();
                    _stage = 49;
                    break;
                case 49:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 50;
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
                    _speechBalloonComponent.SetPosition(160, 240);
                    _speechBalloonComponent.SetDimensions(320, 80);
                    _speechBalloonComponent.SetTextLine1("You will be playing", -145, 30);
                    _speechBalloonComponent.SetTextLine2("with me forever!", -145, 5);
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
                    _wait = new Wait(TimeSpan.FromSeconds(4));
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
                    _speechBalloonComponent.SetPosition(-30, 230);
                    _speechBalloonComponent.SetDimensions(370, 80);
                    _speechBalloonComponent.SetTextLine1("You leave me no choice,", -170, 30);
                    _speechBalloonComponent.SetTextLine2("demon.", -170, 5);
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
                    _wait = new Wait(TimeSpan.FromSeconds(4));
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
                    _speechBalloonComponent.SetPosition(-50, 230);
                    _speechBalloonComponent.SetDimensions(300, 60);
                    _speechBalloonComponent.SetTextLine1("I must destroy you!", -135, 22);
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
                    _wait = new Wait(TimeSpan.FromSeconds(2));
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
                    _speechBalloonComponent.SetPosition(160, 240);
                    _speechBalloonComponent.SetDimensions(370, 80);
                    _speechBalloonComponent.SetTextLine1("Ha, ha, ha. You’re not", -170, 30);
                    _speechBalloonComponent.SetTextLine2("gonna make it!", -170, 5);
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
                    _wait = new Wait(TimeSpan.FromSeconds(4));
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
                    _speechBalloonComponent.SetPosition(160, 240);
                    _speechBalloonComponent.SetDimensions(320, 80);
                    _speechBalloonComponent.SetTextLine1("Check out my most", -145, 30);
                    _speechBalloonComponent.SetTextLine2("terrible power!", -145, 5);
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
                    _wait = new Wait(TimeSpan.FromSeconds(4));
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
                    _speechBalloonComponent.SetPosition(250, 230);
                    _speechBalloonComponent.SetDimensions(210, 60);
                    _speechBalloonComponent.SetTextLine1("FOG OF CHEAT!", -90, 22);
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
                        _stage = 200;
                    }

                    break;
                case 200:
                    if (_smokeTransform2DComponent.Scale.X < TargetSmokeScale)
                    {
                        var deltaSmokeScale = TargetSmokeScale * gameTime.DeltaTime.TotalSeconds * 3;
                        _smokeTransform2DComponent.Scale = _smokeTransform2DComponent.Scale.Add(new Vector2(deltaSmokeScale, deltaSmokeScale));
                    }
                    else
                    {
                        _stage = 201;
                    }

                    break;
                case 201:
                    GameMode.DeleteSlooq();

                    var slooq = Entity.Scene.AllEntities
                        .Single(e =>
                            e.HasComponent<TileObjectPositionComponent>() &&
                            e.GetComponent<TileObjectPositionComponent>().TileObject is Crate &&
                            ((Crate)e.GetComponent<TileObjectPositionComponent>().TileObject!).Type is CrateType.Slooq);
                    slooq.RemoveAfterFullFrame();

                    ((Crate)GameMode.Level.GetTile(3, 5).TileObject!).IsHidden = true;
                    ((Crate)GameMode.Level.GetTile(4, 5).TileObject!).IsHidden = true;
                    ((Crate)GameMode.Level.GetTile(5, 5).TileObject!).IsHidden = true;
                    ((Crate)GameMode.Level.GetTile(6, 5).TileObject!).IsHidden = true;

                    var wallEntities = Scene.AllEntities.Where(e => e.Name == "Wall");
                    var wallRenderers = wallEntities.Select(e => e.GetComponent<SpriteRendererComponent>());
                    foreach (var wallRenderer in wallRenderers)
                    {
                        wallRenderer.Sprite = _assetStore.GetAsset<Sprite>(SlooqQuestAssetId.Sprites.Wall.Gray);
                    }

                    _stage = 202;
                    break;
                case 202:
                    if (_smokeTransform2DComponent.Scale.X > 0)
                    {
                        var deltaSmokeScale = TargetSmokeScale * gameTime.DeltaTime.TotalSeconds * 3;
                        _smokeTransform2DComponent.Scale = _smokeTransform2DComponent.Scale.Subtract(new Vector2(deltaSmokeScale, deltaSmokeScale));
                    }
                    else
                    {
                        _stage = 203;
                    }

                    break;
                case 203:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 204;
                    break;
                case 204:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 100;
                    }

                    break;
                case 100:
                    _speechBalloonComponent.SetPosition(20, 230);
                    _speechBalloonComponent.SetDimensions(440, 80);
                    _speechBalloonComponent.SetTextLine1("It won’t stop me! Playing with", -205, 30);
                    _speechBalloonComponent.SetTextLine2("Slooq must come to an end!", -205, 5);
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
                    GameMode.MoveRight();
                    _stage = 109;
                    break;
                case 109:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 110;
                    }

                    break;
                case 110:
                    GameMode.MoveRight();
                    _stage = 111;
                    break;
                case 111:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 112;
                    }

                    break;
                case 112:
                    GameMode.MoveRight();
                    _stage = 113;
                    break;
                case 113:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 114;
                    }

                    break;
                case 114:
                    GameMode.MoveRight();
                    _stage = 115;
                    break;
                case 115:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 116;
                    }

                    break;
                case 116:
                    GameMode.MoveRight();
                    _stage = 117;
                    break;
                case 117:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 118;
                    }

                    break;
                case 118:
                    GameMode.MoveRight();
                    _stage = 119;
                    break;
                case 119:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 120;
                    }

                    break;
                case 120:
                    GameMode.MoveRight();
                    _stage = 121;
                    break;
                case 121:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 122;
                    }

                    break;
                case 122:
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
                        loadSceneComponent.SceneBehaviorName = "SlooqQuestGame";
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
        private readonly IAssetStore _assetStore;

        public IntroToFinalCutSceneComponentFactory(IAssetStore assetStore)
        {
            _assetStore = assetStore;
        }

        protected override IntroToFinalCutSceneComponent CreateComponent(Entity entity) => new IntroToFinalCutSceneComponent(entity, _assetStore);
    }
}