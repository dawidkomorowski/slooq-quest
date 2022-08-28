using System;
using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using SlooqQuest.Assets;
using SlooqQuest.Core.LevelModel;

namespace SlooqQuest.Core.Components
{
    public sealed class CrateRendererComponent : BehaviorComponent
    {
        private readonly IModeInfo _modeInfo;
        private readonly Sprite _brown;
        private readonly Sprite _red;
        private readonly Sprite _redGrayedOut;
        private readonly Sprite _blue;
        private readonly Sprite _blueGrayedOut;
        private readonly Sprite _green;
        private readonly Sprite _gray;
        private readonly Sprite _grayGrayedOut;
        private readonly Sprite _slooq;
        private SpriteRendererComponent _spriteRendererComponent = null!;
        private TextRendererComponent _labelTextRendererComponent = null!;
        private TextRendererComponent _editorLabelTextRendererComponent = null!;

        public CrateRendererComponent(Entity entity, IAssetStore assetStore, IModeInfo modeInfo) : base(entity)
        {
            _modeInfo = modeInfo;

            _brown = assetStore.GetAsset<Sprite>(SlooqQuestAssetId.Sprites.Crate.Brown);
            _red = assetStore.GetAsset<Sprite>(SlooqQuestAssetId.Sprites.Crate.Red);
            _redGrayedOut = assetStore.GetAsset<Sprite>(SlooqQuestAssetId.Sprites.Crate.RedGrayedOut);
            _blue = assetStore.GetAsset<Sprite>(SlooqQuestAssetId.Sprites.Crate.Blue);
            _blueGrayedOut = assetStore.GetAsset<Sprite>(SlooqQuestAssetId.Sprites.Crate.BlueGrayedOut);
            _green = assetStore.GetAsset<Sprite>(SlooqQuestAssetId.Sprites.Crate.Green);
            _gray = assetStore.GetAsset<Sprite>(SlooqQuestAssetId.Sprites.Crate.Gray);
            _grayGrayedOut = assetStore.GetAsset<Sprite>(SlooqQuestAssetId.Sprites.Crate.GrayGrayedOut);
            _slooq = assetStore.GetAsset<Sprite>(SlooqQuestAssetId.Sprites.Slooq.Default);
        }

        public Crate? Crate { get; set; }

        public override void OnStart()
        {
            Entity.GetComponent<Transform2DComponent>();
            _spriteRendererComponent = Entity.GetComponent<SpriteRendererComponent>();
            _labelTextRendererComponent = Entity.Children.Single(e => e.Name == "Label").GetComponent<TextRendererComponent>();
            _editorLabelTextRendererComponent = Entity.Children.Single(e => e.Name == "EditorLabel").GetComponent<TextRendererComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (Crate == null)
            {
                return;
            }

            if (Crate.Type is CrateType.Slooq)
            {
                _spriteRendererComponent.Sprite = _slooq;
                _labelTextRendererComponent.Text = string.Empty;
                _editorLabelTextRendererComponent.Visible = false;

                return;
            }


            if (Crate.IsHidden && _modeInfo.Mode is Mode.Game)
            {
                _spriteRendererComponent.Sprite = Crate.IsLocked ? _grayGrayedOut : _gray;
                _labelTextRendererComponent.Text = string.Empty;
                _editorLabelTextRendererComponent.Visible = false;
            }
            else
            {
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
                    _labelTextRendererComponent.Text = Crate.Counter > 0 ? Crate.Counter.ToString() : string.Empty;
                }

                _editorLabelTextRendererComponent.Visible = Crate.IsHidden;
            }
        }
    }

    public sealed class CrateRendererComponentFactory : ComponentFactory<CrateRendererComponent>
    {
        private readonly IAssetStore _assetStore;
        private readonly IModeInfo _modeInfo;

        public CrateRendererComponentFactory(IAssetStore assetStore, IModeInfo modeInfo)
        {
            _assetStore = assetStore;
            _modeInfo = modeInfo;
        }

        protected override CrateRendererComponent CreateComponent(Entity entity) => new CrateRendererComponent(entity, _assetStore, _modeInfo);
    }
}