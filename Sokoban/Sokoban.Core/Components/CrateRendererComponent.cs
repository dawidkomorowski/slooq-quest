using System;
using System.Linq;
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
        private readonly Sprite _redGrayedOut;
        private readonly Sprite _blue;
        private readonly Sprite _blueGrayedOut;
        private readonly Sprite _green;
        private readonly Sprite _gray;
        private SpriteRendererComponent _spriteRendererComponent = null!;
        private TextRendererComponent _textRendererComponent = null!;

        public CrateRendererComponent(Entity entity, IAssetStore assetStore) : base(entity)
        {
            _brown = assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Crate.Brown);
            _red = assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Crate.Red);
            _redGrayedOut = assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Crate.RedGrayedOut);
            _blue = assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Crate.Blue);
            _blueGrayedOut = assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Crate.BlueGrayedOut);
            _green = assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Crate.Green);
            _gray = assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Crate.Gray);
        }

        public Crate? Crate { get; set; }

        public override void OnStart()
        {
            _spriteRendererComponent = Entity.GetComponent<SpriteRendererComponent>();
            _textRendererComponent = Entity.Children.Single().GetComponent<TextRendererComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (Crate == null)
            {
                return;
            }

            if (Crate.IsLocked)
            {
                _spriteRendererComponent.Sprite = Crate.Type switch
                {
                    CrateType.Red => _redGrayedOut,
                    CrateType.Blue => _blueGrayedOut,
                    _ => throw new ArgumentOutOfRangeException($"Missing sprite for crate type: {Crate?.Type}")
                };
            }
            else
            {
                _spriteRendererComponent.Sprite = Crate.Type switch
                {
                    CrateType.Brown => _brown,
                    CrateType.Red => _red,
                    CrateType.Blue => _blue,
                    CrateType.Green => _green,
                    _ => throw new ArgumentOutOfRangeException($"Missing sprite for crate type: {Crate?.Type}")
                };
            }

            if (Crate.Type is CrateType.Blue)
            {
                _textRendererComponent.Text = Crate.Counter > 0 ? Crate.Counter.ToString() : string.Empty;
            }
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