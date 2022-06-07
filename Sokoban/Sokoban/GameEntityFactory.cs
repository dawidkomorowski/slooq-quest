using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Input.Mapping;
using Sokoban.Assets;
using Sokoban.Components;
using Sokoban.Core.GameLogic;

namespace Sokoban
{
    internal sealed class GameEntityFactory
    {
        private readonly IAssetStore _assetStore;

        public GameEntityFactory(IAssetStore assetStore)
        {
            _assetStore = assetStore;
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
    }
}