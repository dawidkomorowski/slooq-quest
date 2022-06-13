using System;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using Sokoban.Assets;
using Sokoban.Core.LevelModel;

namespace Sokoban.Core.Components
{
    public sealed class CrateRendererComponent : BehaviorComponent
    {
        private readonly Sprite _brown;
        private readonly Sprite _red;
        private SpriteRendererComponent _spriteRendererComponent = null!;

        public CrateRendererComponent(Entity entity, IAssetStore assetStore) : base(entity)
        {
            _brown = assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Crate.Brown);
            _red = assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Crate.Red);
        }

        public Crate? Crate { get; set; }

        public override void OnStart()
        {
            _spriteRendererComponent = Entity.GetComponent<SpriteRendererComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _spriteRendererComponent.Sprite = Crate?.Type switch
            {
                CrateType.Brown => _brown,
                CrateType.Red => _red,
                _ => throw new ArgumentOutOfRangeException($"Missing sprite for crate type: {Crate?.Type}")
            };
        }
    }

    public sealed class CrateRendererComponentFactory : ComponentFactory<CrateRendererComponent>
    {
        private readonly IAssetStore _assetStore;

        public CrateRendererComponentFactory(IAssetStore assetStore)
        {
            _assetStore = assetStore;
        }

        protected override CrateRendererComponent CreateComponent(Entity entity) => new CrateRendererComponent(entity, _assetStore);
    }
}