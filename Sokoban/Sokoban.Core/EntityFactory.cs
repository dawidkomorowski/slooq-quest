using Geisha.Common.Math;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using Sokoban.Assets;
using Sokoban.Core.LevelModel;

namespace Sokoban.Core
{
    public sealed class EntityFactory
    {
        private readonly IAssetStore _assetStore;

        public EntityFactory(IAssetStore assetStore)
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
            transform2DComponent.Translation = GetTranslation(tile);

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Ground.Gray);
            spriteRendererComponent.SortingLayerName = "Ground";

            return entity;
        }

        private static Vector2 GetTranslation(Tile tile)
        {
            const int tileSize = 64;
            return new Vector2(tile.X * tileSize, tile.Y * tileSize);
        }
    }
}