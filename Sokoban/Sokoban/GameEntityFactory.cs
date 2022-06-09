using Geisha.Engine.Core.Assets;

namespace Sokoban
{
    internal sealed class GameEntityFactory
    {
        private readonly IAssetStore _assetStore;

        public GameEntityFactory(IAssetStore assetStore)
        {
            _assetStore = assetStore;
        }
    }
}