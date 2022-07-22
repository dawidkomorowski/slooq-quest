using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Input.Mapping;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;

namespace Sokoban.LevelSelectionMenu
{
    internal sealed class LevelSelectionMenuEntityFactory
    {
        public Entity CreateLevelSelectionMenu(Scene scene)
        {
            var entity = scene.CreateEntity();

            entity.CreateComponent<Transform2DComponent>();

            var inputComponent = entity.CreateComponent<InputComponent>();
            inputComponent.InputMapping = new InputMapping
            {
                ActionMappings =
                {
                    new ActionMapping
                    {
                        ActionName = "SelectPreviousLevel",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Up) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "SelectNextLevel",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Down) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "SelectLevel",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Enter) } }
                    },
                    new ActionMapping
                    {
                        ActionName = "Back",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Escape) } }
                    }
                }
            };

            var levelSelectionMenuComponent = entity.CreateComponent<LevelSelectionMenuComponent>();
            levelSelectionMenuComponent.LevelSelectionModel = new LevelSelectionModel();

            return entity;
        }
    }
}