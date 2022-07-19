using System;
using Geisha.Common.Math;
using Geisha.Engine.Animation;
using Geisha.Engine.Animation.Components;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Input.Mapping;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using Sokoban.Assets;
using Sokoban.Core.Components;
using Sokoban.Core.GameLogic;
using Sokoban.Core.LevelModel;
using Sokoban.Core.Util;

namespace Sokoban.Core
{
    public sealed class CoreEntityFactory
    {
        private readonly IAssetStore _assetStore;

        public CoreEntityFactory(IAssetStore assetStore)
        {
            _assetStore = assetStore;
        }

        public Entity CreateCamera(Scene scene)
        {
            var entity = scene.CreateEntity();

            entity.CreateComponent<Transform2DComponent>();

            var cameraComponent = entity.CreateComponent<CameraComponent>();
            cameraComponent.ViewRectangle = new Vector2(1280, 720);

            return entity;
        }

        public Entity CreateBackground(Scene scene)
        {
            var entity = scene.CreateEntity();

            entity.CreateComponent<Transform2DComponent>();

            var rectangleRendererComponent = entity.CreateComponent<RectangleRendererComponent>();
            rectangleRendererComponent.Dimension = new Vector2(1280, 720);
            rectangleRendererComponent.Color = Color.FromArgb(255, 91, 105, 107);
            rectangleRendererComponent.FillInterior = true;
            rectangleRendererComponent.SortingLayerName = "Background";

            return entity;
        }

        public Entity CreateLevel(Scene scene, Level level)
        {
            var levelEntity = scene.CreateEntity();
            levelEntity.Name = "Level";

            var transform2DComponent = levelEntity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(-320 + 32, -320 + 32);

            for (var x = 0; x < level.Width; x++)
            {
                for (var y = 0; y < level.Height; y++)
                {
                    var tile = level.GetTile(x, y);
                    CreateGround(levelEntity, tile);

                    if (tile.TileObject is Wall wall)
                    {
                        CreateWall(levelEntity, wall);
                    }

                    if (tile.TileObject is Crate crate)
                    {
                        CreateCrate(levelEntity, crate);
                    }

                    if (tile.TileObject is Player player)
                    {
                        CreatePlayer(levelEntity, player);
                    }

                    if (tile.CrateSpot != null)
                    {
                        CreateCrateSpot(levelEntity, tile.CrateSpot);
                    }
                }
            }

            return levelEntity;
        }

        public Entity CreatePlayerController(Scene scene, GameMode gameMode)
        {
            var entity = scene.CreateEntity();

            var inputComponent = entity.CreateComponent<InputComponent>();
            inputComponent.InputMapping = _assetStore.GetAsset<InputMapping>(SokobanAssetId.InputMapping.Default);

            var playerControllerComponent = entity.CreateComponent<PlayerControllerComponent>();
            playerControllerComponent.GameMode = gameMode;

            return entity;
        }

        private void CreateGround(Entity levelEntity, Tile tile)
        {
            if (tile.Ground is Ground.None)
            {
                return;
            }

            var entity = levelEntity.CreateChildEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = tile.GetTranslation();

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.SortingLayerName = "Ground";

            var groundAssetId = tile.Ground switch
            {
                Ground.None => throw new ArgumentOutOfRangeException(),
                Ground.Brown => SokobanAssetId.Sprites.Ground.Brown,
                Ground.Green => SokobanAssetId.Sprites.Ground.Green,
                Ground.Gray => SokobanAssetId.Sprites.Ground.Gray,
                _ => throw new ArgumentOutOfRangeException()
            };

            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(groundAssetId);
        }

        private void CreateWall(Entity levelEntity, Wall wall)
        {
            var entity = levelEntity.CreateChildEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = wall.Tile.GetTranslation();

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();

            var wallAssetId = wall.Type switch
            {
                WallType.Red => SokobanAssetId.Sprites.Wall.Red,
                WallType.RedGray => SokobanAssetId.Sprites.Wall.RedGray,
                WallType.Gray => SokobanAssetId.Sprites.Wall.Gray,
                WallType.Brown => SokobanAssetId.Sprites.Wall.Brown,
                WallType.TopRed => SokobanAssetId.Sprites.Wall.TopRed,
                WallType.TopRedGray => SokobanAssetId.Sprites.Wall.TopRedGray,
                WallType.TopGray => SokobanAssetId.Sprites.Wall.TopGray,
                WallType.TopBrown => SokobanAssetId.Sprites.Wall.TopBrown,
                _ => throw new ArgumentOutOfRangeException()
            };

            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(wallAssetId);
            spriteRendererComponent.SortingLayerName = "TileObject";

            var tileObjectPositionComponent = entity.CreateComponent<TileObjectPositionComponent>();
            tileObjectPositionComponent.TileObject = wall;
        }

        private void CreateCrate(Entity levelEntity, Crate crate)
        {
            var entity = levelEntity.CreateChildEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = crate.Tile.GetTranslation();

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.SortingLayerName = "TileObject";

            var tileObjectPositionComponent = entity.CreateComponent<TileObjectPositionComponent>();
            tileObjectPositionComponent.TileObject = crate;

            var crateRendererComponent = entity.CreateComponent<CrateRendererComponent>();
            crateRendererComponent.Crate = crate;

            var labelEntity = entity.CreateChildEntity();
            labelEntity.Name = "Label";

            var labelTransform2DComponent = labelEntity.CreateComponent<Transform2DComponent>();
            labelTransform2DComponent.Translation = new Vector2(-9, 33);

            var labelRendererComponent = labelEntity.CreateComponent<TextRendererComponent>();
            labelRendererComponent.SortingLayerName = "TileObject";
            labelRendererComponent.OrderInLayer = 1;
            labelRendererComponent.FontSize = FontSize.FromDips(35);
            labelRendererComponent.Color = Color.FromArgb(255, 255, 255, 255);

            var editorLabelEntity = entity.CreateChildEntity();
            editorLabelEntity.Name = "EditorLabel";

            var editorLabelTransform2DComponent = editorLabelEntity.CreateComponent<Transform2DComponent>();
            editorLabelTransform2DComponent.Translation = new Vector2(-27, -10);

            var editorLabelRendererComponent = editorLabelEntity.CreateComponent<TextRendererComponent>();
            editorLabelRendererComponent.SortingLayerName = "TileObject";
            editorLabelRendererComponent.OrderInLayer = 1;
            editorLabelRendererComponent.FontSize = FontSize.FromDips(15);
            editorLabelRendererComponent.Color = Color.FromArgb(255, 255, 255, 255);
            editorLabelRendererComponent.Text = "Hidden";
        }

        private void CreatePlayer(Entity levelEntity, Player player)
        {
            var entity = levelEntity.CreateChildEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = player.Tile.GetTranslation();

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Player.Default);
            spriteRendererComponent.SortingLayerName = "TileObject";

            var spriteAnimationComponent = entity.CreateComponent<SpriteAnimationComponent>();
            spriteAnimationComponent.AddAnimation("StandUp", _assetStore.GetAsset<SpriteAnimation>(SokobanAssetId.Animations.Player.StandUp));
            spriteAnimationComponent.AddAnimation("StandDown", _assetStore.GetAsset<SpriteAnimation>(SokobanAssetId.Animations.Player.StandDown));
            spriteAnimationComponent.AddAnimation("StandLeft", _assetStore.GetAsset<SpriteAnimation>(SokobanAssetId.Animations.Player.StandLeft));
            spriteAnimationComponent.AddAnimation("StandRight", _assetStore.GetAsset<SpriteAnimation>(SokobanAssetId.Animations.Player.StandRight));
            spriteAnimationComponent.AddAnimation("MoveUp", _assetStore.GetAsset<SpriteAnimation>(SokobanAssetId.Animations.Player.MoveUp));
            spriteAnimationComponent.AddAnimation("MoveDown", _assetStore.GetAsset<SpriteAnimation>(SokobanAssetId.Animations.Player.MoveDown));
            spriteAnimationComponent.AddAnimation("MoveLeft", _assetStore.GetAsset<SpriteAnimation>(SokobanAssetId.Animations.Player.MoveLeft));
            spriteAnimationComponent.AddAnimation("MoveRight", _assetStore.GetAsset<SpriteAnimation>(SokobanAssetId.Animations.Player.MoveRight));
            spriteAnimationComponent.PlayInLoop = true;
            spriteAnimationComponent.PlaybackSpeed = 3.0;
            spriteAnimationComponent.PlayAnimation("StandDown");

            var tileObjectPositionComponent = entity.CreateComponent<TileObjectPositionComponent>();
            tileObjectPositionComponent.TileObject = player;

            entity.CreateComponent<PlayerAnimationControllerComponent>();
        }

        private void CreateCrateSpot(Entity levelEntity, CrateSpot crateSpot)
        {
            var entity = levelEntity.CreateChildEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = crateSpot.Tile.GetTranslation();

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.SortingLayerName = "CrateSpot";
            spriteRendererComponent.Sprite = crateSpot.Type switch
            {
                CrateSpotType.Brown => _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.CrateSpot.Brown),
                CrateSpotType.Red => _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.CrateSpot.Red),
                CrateSpotType.Blue => _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.CrateSpot.Blue),
                CrateSpotType.Green => _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.CrateSpot.Green),
                _ => throw new ArgumentOutOfRangeException($"Missing sprite for crate spot type: {crateSpot.Type}")
            };
        }
    }
}