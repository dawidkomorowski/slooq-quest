using Geisha.Common.Math;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using Sokoban.Assets;

namespace Sokoban.MainMenu
{
    internal sealed class MainMenuEntityFactory
    {
        private readonly IAssetStore _assetStore;

        public MainMenuEntityFactory(IAssetStore assetStore)
        {
            _assetStore = assetStore;
        }

        public Entity CreateTitle(Scene scene)
        {
            var entity = scene.CreateEntity();

            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(0, 200);
            transform2DComponent.Scale = new Vector2(0.6, 0.6);

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.MainMenu.Title);
            spriteRendererComponent.SortingLayerName = "UI";

            return entity;
        }
    }
}