using System;
using System.Linq;
using Geisha.Common.Math;
using Geisha.Engine.Animation;
using Geisha.Engine.Animation.Components;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;
using SlooqQuest.Assets;
using SlooqQuest.Core.Components;
using SlooqQuest.Core.GameLogic;
using SlooqQuest.Core.LevelModel;
using SlooqQuest.Core.SceneLoading;
using SlooqQuest.VisualEffects;

namespace SlooqQuest.CutScenes.Final
{
    internal sealed class FinalCutSceneComponent : BehaviorComponent
    {
        private const double TargetSmokeScale = 1;
        private readonly IAssetStore _assetStore;
        private int _stage = 0;
        private TileObjectPositionComponent _playerTileObjectPositionComponent = null!;
        private Transform2DComponent _slooqTransform2DComponent = null!;
        private SpeechBalloonComponent _speechBalloonComponent = null!;
        private Transform2DComponent _smokeTransform2DComponent = null!;
        private Wait _wait = new Wait(TimeSpan.Zero);

        public FinalCutSceneComponent(Entity entity, IAssetStore assetStore) : base(entity)
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

            _slooqTransform2DComponent = Entity.Scene.AllEntities
                .Single(e =>
                    e.HasComponent<TileObjectPositionComponent>() &&
                    e.GetComponent<TileObjectPositionComponent>().TileObject is Crate &&
                    ((Crate)e.GetComponent<TileObjectPositionComponent>().TileObject!).Type is CrateType.Slooq)
                .GetComponent<Transform2DComponent>();

            var speechBalloonEntity = Scene.CreateEntity();
            _speechBalloonComponent = speechBalloonEntity.CreateComponent<SpeechBalloonComponent>();

            var smokeEntity = Scene.CreateEntity();
            _smokeTransform2DComponent = smokeEntity.CreateComponent<Transform2DComponent>();
            _smokeTransform2DComponent.Scale = new Vector2(0, 0);
            _smokeTransform2DComponent.Translation = new Vector2(32, 160);
            var smokeSpriteRendererComponent = smokeEntity.CreateComponent<SpriteRendererComponent>();
            smokeSpriteRendererComponent.SortingLayerName = "VFX";
            var smokeSpriteAnimationComponent = smokeEntity.CreateComponent<SpriteAnimationComponent>();
            smokeSpriteAnimationComponent.AddAnimation("Default", _assetStore.GetAsset<SpriteAnimation>(SlooqQuestAssetId.Animations.Smoke.Default));
            smokeSpriteAnimationComponent.PlayInLoop = true;
            smokeSpriteAnimationComponent.PlayAnimation("Default");

            // Make red and blue crate locked.
            if (GameMode != null)
            {
                ((Crate)GameMode.Level.GetTile(7, 4).TileObject!).Counter = 1;
                ((Crate)GameMode.Level.GetTile(7, 4).TileObject!).OnMove();
                ((Crate)GameMode.Level.GetTile(7, 5).TileObject!).OnMove();
            }
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
                    if (_smokeTransform2DComponent.Scale.X < TargetSmokeScale)
                    {
                        var deltaSmokeScale = TargetSmokeScale * gameTime.DeltaTime.TotalSeconds * 3;
                        _smokeTransform2DComponent.Scale = _smokeTransform2DComponent.Scale.Add(new Vector2(deltaSmokeScale, deltaSmokeScale));
                    }
                    else
                    {
                        _stage = 3;
                    }

                    break;
                case 3:
                    var wall = GameMode.Level.GetTile(5, 7).TileObject;

                    var wallEntity = Entity.Scene.AllEntities
                        .Single(e =>
                            e.HasComponent<TileObjectPositionComponent>() &&
                            e.GetComponent<TileObjectPositionComponent>().TileObject == wall);
                    wallEntity.RemoveAfterFullFrame();

                    GameMode.DeleteWall(5, 7);

                    _stage = 4;
                    break;
                case 4:
                    if (_smokeTransform2DComponent.Scale.X > 0)
                    {
                        var deltaSmokeScale = TargetSmokeScale * gameTime.DeltaTime.TotalSeconds * 3;
                        _smokeTransform2DComponent.Scale = _smokeTransform2DComponent.Scale.Subtract(new Vector2(deltaSmokeScale, deltaSmokeScale));
                    }
                    else
                    {
                        _stage = 5;
                    }

                    break;
                case 5:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 6;
                    break;
                case 6:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 7;
                    }

                    break;
                case 7:
                    _speechBalloonComponent.SetPosition(-200, 220);
                    _speechBalloonComponent.SetDimensions(370, 80);
                    _speechBalloonComponent.SetTextLine1("No! I don’t want to stop", -170, 30);
                    _speechBalloonComponent.SetTextLine2("playing with you!", -170, 5);
                    _speechBalloonComponent.Show();
                    _stage = 8;
                    break;
                case 8:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 9;
                    }

                    break;
                case 9:
                    _wait = new Wait(TimeSpan.FromSeconds(4));
                    _stage = 10;
                    break;
                case 10:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 11;
                    }

                    break;
                case 11:
                    _speechBalloonComponent.Hide();
                    _stage = 12;
                    break;
                case 12:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 13;
                    }

                    break;
                case 13:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 14;
                    break;
                case 14:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 15;
                    }

                    break;
                case 15:
                    GameMode.MoveUp();
                    _stage = 16;
                    break;
                case 16:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 17;
                    }

                    break;
                case 17:
                    GameMode.MoveUp();
                    _stage = 18;
                    break;
                case 18:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 19;
                    }

                    break;
                case 19:
                    GameMode.MoveUp();
                    _stage = 20;
                    break;
                case 20:
                    if (!_playerTileObjectPositionComponent.IsAnimating)
                    {
                        _stage = 21;
                    }

                    break;
                case 21:
                    if (_slooqTransform2DComponent.Scale.X > 0)
                    {
                        var deltaSlooqScale = gameTime.DeltaTime.TotalSeconds * 0.5;
                        _slooqTransform2DComponent.Scale -= new Vector2(deltaSlooqScale, deltaSlooqScale);
                        _slooqTransform2DComponent.Rotation += gameTime.DeltaTime.TotalSeconds * 5;
                    }
                    else
                    {
                        _stage = 22;
                    }

                    break;
                case 22:
                    _speechBalloonComponent.SetPosition(-200, 220);
                    _speechBalloonComponent.SetDimensions(370, 80);
                    _speechBalloonComponent.SetTextLine1("Noone will have to play", -170, 30);
                    _speechBalloonComponent.SetTextLine2("with you anymore, Slooq!", -170, 5);
                    _speechBalloonComponent.Show();
                    _stage = 23;
                    break;
                case 23:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 24;
                    }

                    break;
                case 24:
                    _wait = new Wait(TimeSpan.FromSeconds(4));
                    _stage = 25;
                    break;
                case 25:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 26;
                    }

                    break;
                case 26:
                    _speechBalloonComponent.Hide();
                    _stage = 27;
                    break;
                case 27:
                    if (_speechBalloonComponent.WaitForAnimation())
                    {
                        _stage = 28;
                    }

                    break;
                case 28:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 29;
                    break;
                case 29:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 30;
                    }

                    break;
                case 30:
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
                        loadSceneComponent.SceneBehaviorName = "Credits";
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

    internal sealed class FinalCutSceneComponentFactory : ComponentFactory<FinalCutSceneComponent>
    {
        private readonly IAssetStore _assetStore;

        public FinalCutSceneComponentFactory(IAssetStore assetStore)
        {
            _assetStore = assetStore;
        }

        protected override FinalCutSceneComponent CreateComponent(Entity entity) => new FinalCutSceneComponent(entity, _assetStore);
    }
}