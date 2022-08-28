using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Input.Mapping;

namespace Sokoban.Editor.ToggleMode
{
    internal sealed class ToggleModeEntityFactory
    {
        public void CreateToggleModeEntity(Scene scene)
        {
            var entity = scene.CreateEntity();

            var inputComponent = entity.CreateComponent<InputComponent>();
            inputComponent.InputMapping = new InputMapping
            {
                ActionMappings =
                {
                    new ActionMapping
                    {
                        ActionName = "ToggleMode",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Enter) } }
                    }
                }
            };

            entity.CreateComponent<ToggleModeComponent>();
        }

        public void CreateEnterModeEntity(Scene scene)
        {
            var entity = scene.CreateEntity();
            entity.CreateComponent<EnterModeComponent>();
        }
    }
}