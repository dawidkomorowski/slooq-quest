using Geisha.Common.Math;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using Sokoban.Assets;
using Sokoban.Core.Components;
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

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(320 - 32, 320 - 32);

            var cameraComponent = entity.CreateComponent<CameraComponent>();
            cameraComponent.ViewRectangle = new Vector2(1280, 720);

            return entity;
        }

        public Entity CreateBackground(Scene scene)
        {
            var entity = scene.CreateEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(320 - 32, 320 - 32);

            var rectangleRendererComponent = entity.CreateComponent<RectangleRendererComponent>();
            rectangleRendererComponent.Dimension = new Vector2(1280, 720);
            rectangleRendererComponent.Color = Color.FromArgb(255, 91, 105, 107);
            rectangleRendererComponent.FillInterior = true;
            rectangleRendererComponent.SortingLayerName = "Background";

            return entity;
        }

        public Entity CreateGround(Scene scene, Tile tile)
        {
            var entity = scene.CreateEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = tile.GetTranslation();

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Ground.Gray);
            spriteRendererComponent.SortingLayerName = "Ground";

            return entity;
        }

        public Entity CreateWall(Scene scene, Wall wall)
        {
            var entity = scene.CreateEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = wall.Tile.GetTranslation();

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Wall.RedGray);
            spriteRendererComponent.SortingLayerName = "TileObject";

            var tileObjectPositionComponent = entity.CreateComponent<TileObjectPositionComponent>();
            tileObjectPositionComponent.TileObject = wall;

            return entity;
        }

        public Entity CreateCrate(Scene scene, Crate crate)
        {
            var entity = scene.CreateEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = crate.Tile.GetTranslation();

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Crate.Brown);
            spriteRendererComponent.SortingLayerName = "TileObject";

            var tileObjectPositionComponent = entity.CreateComponent<TileObjectPositionComponent>();
            tileObjectPositionComponent.TileObject = crate;

            return entity;
        }

        public Entity CreatePlayer(Scene scene, Player player)
        {
            var entity = scene.CreateEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = player.Tile.GetTranslation();

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Player.Default);
            spriteRendererComponent.SortingLayerName = "TileObject";

            var tileObjectPositionComponent = entity.CreateComponent<TileObjectPositionComponent>();
            tileObjectPositionComponent.TileObject = player;

            return entity;
        }
    }
}