using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Input.Mapping;

namespace SlooqQuest.LevelSelectionMenu
{
    internal sealed class LevelSelectionMenuEntityFactory
    {
        private readonly GameState _gameState;

        public LevelSelectionMenuEntityFactory(GameState gameState)
        {
            _gameState = gameState;
        }

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
            levelSelectionMenuComponent.LevelSelectionModel = new LevelSelectionModel(_gameState.Levels, _gameState.CurrentLevel);

            return entity;
        }
    }
}