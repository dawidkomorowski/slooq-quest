using Geisha.Engine.Core.Assets;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Input.Mapping;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using Sokoban.Assets;
using Sokoban.Core.EditorLogic;

namespace Sokoban.Editor.UserInterface
{
    internal sealed class UserInterfaceEntityFactory
    {
        private readonly IAssetStore _assetStore;

        public UserInterfaceEntityFactory(IAssetStore assetStore)
        {
            _assetStore = assetStore;
        }

        public Entity CreateCursor(Scene scene, EditMode editMode)
        {
            var entity = scene.CreateEntity();

            entity.CreateComponent<Transform2DComponent>();

            var spriteRendererComponent = entity.CreateComponent<SpriteRendererComponent>();
            spriteRendererComponent.Sprite = _assetStore.GetAsset<Sprite>(SokobanAssetId.Sprites.Editor.Cursor);
            spriteRendererComponent.SortingLayerName = "UI";

            var inputComponent = entity.CreateComponent<InputComponent>();
            inputComponent.InputMapping = new InputMapping
            {
                ActionMappings =
                {
                    new ActionMapping
                    {
                        ActionName = "MoveUp",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Up) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "MoveDown",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Down) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "MoveLeft",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Left) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "MoveRight",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Right) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "Exit",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Escape) } }
                    }
                }
            };

            var cursorComponent = entity.CreateComponent<CursorComponent>();
            cursorComponent.EditMode = editMode;

            return entity;
        }
    }
}