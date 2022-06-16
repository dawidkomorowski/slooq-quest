using Geisha.Common.Math;
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
                        ActionName = "Delete",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Delete) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "CreateBrownCrate",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.F1) } }
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

        public Entity CreateHelp(Scene scene)
        {
            var help = scene.CreateEntity();

            var transform2DComponent = help.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(-635, 0);

            var index = 0;
            AddHelpLabel(help, "Arrows - Move Cursor", index++);
            AddHelpLabel(help, "F1 - Create Brown Crate", index++);
            AddHelpLabel(help, "Delete - Remove object", index++);
            AddHelpLabel(help, "Esc - Exit editor", index);

            return help;
        }

        private void AddHelpLabel(Entity help, string label, int index)
        {
            const int size = 14;
            var labelEntity = help.CreateChildEntity();
            var transform2DComponent = labelEntity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(0, -index * size);

            var textRendererComponent = labelEntity.CreateComponent<TextRendererComponent>();
            textRendererComponent.Text = label;
            textRendererComponent.Color = Color.FromArgb(255, 255, 255, 255);
            textRendererComponent.FontSize = FontSize.FromDips(size);
            textRendererComponent.SortingLayerName = "UI";
        }
    }
}