using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;

namespace Sokoban.MainMenu
{
    internal sealed class MainMenuComponent : BehaviorComponent
    {
        private InputComponent _inputComponent = null!;

        public MainMenuComponent(Entity entity) : base(entity)
        {
        }

        public MainMenuModel? MainMenuModel { get; set; }

        public override void OnStart()
        {
            _inputComponent = Entity.GetComponent<InputComponent>();

            if (MainMenuModel is null)
            {
                return;
            }

            foreach (var mainMenuOption in MainMenuModel.Options)
            {
                CreateMenuOption(mainMenuOption);
            }

            _inputComponent.BindAction("OptionUp", MainMenuModel.OptionUp);
            _inputComponent.BindAction("OptionDown", MainMenuModel.OptionDown);
        }

        public override void OnUpdate(GameTime gameTime)
        {
        }

        private void CreateMenuOption(MainMenuOption mainMenuOption)
        {
            var entity = Entity.CreateChildEntity();
            var mainMenuOptionComponent = entity.CreateComponent<MainMenuOptionComponent>();
            mainMenuOptionComponent.MainMenuOption = mainMenuOption;
        }
    }

    internal sealed class MainMenuComponentFactory : ComponentFactory<MainMenuComponent>
    {
        protected override MainMenuComponent CreateComponent(Entity entity) => new MainMenuComponent(entity);
    }
}