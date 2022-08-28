using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;

namespace SlooqQuest.MainMenu
{
    internal sealed class MainMenuOptionComponent : BehaviorComponent
    {
        private Transform2DComponent _transform2DComponent = null!;
        private TextRendererComponent _textRendererComponent = null!;

        public MainMenuOptionComponent(Entity entity) : base(entity)
        {
        }

        public MainMenuOption? MainMenuOption { get; set; }
        public MainMenuModel? MainMenuModel { get; set; }

        public override void OnStart()
        {
            if (MainMenuOption is null)
            {
                return;
            }

            _transform2DComponent = Entity.CreateComponent<Transform2DComponent>();

            _textRendererComponent = Entity.CreateComponent<TextRendererComponent>();
            _textRendererComponent.Color = Color.FromArgb(255, 255, 255, 255);
            _textRendererComponent.Text = MainMenuOption.Text;
            _textRendererComponent.SortingLayerName = "UI";
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (MainMenuOption is null)
            {
                return;
            }

            if (MainMenuModel is null)
            {
                return;
            }

            if (MainMenuOption.IsSelected)
            {
                _transform2DComponent.Translation = new Vector2(-MainMenuOption.Text.Length * 28, 50 - MainMenuOption.Index * 60);
                _textRendererComponent.FontSize = FontSize.FromDips(100);
            }
            else
            {
                _textRendererComponent.FontSize = FontSize.FromDips(60);

                if (MainMenuOption.Index < MainMenuModel.GetSelectedOption().Index)
                {
                    _transform2DComponent.Translation = new Vector2(-MainMenuOption.Text.Length * 18, 50 - MainMenuOption.Index * 60);
                }
                else
                {
                    _transform2DComponent.Translation = new Vector2(-MainMenuOption.Text.Length * 18, 50 - 50 - MainMenuOption.Index * 60);
                }
            }
        }
    }

    internal sealed class MainMenuOptionComponentFactory : ComponentFactory<MainMenuOptionComponent>
    {
        protected override MainMenuOptionComponent CreateComponent(Entity entity) => new MainMenuOptionComponent(entity);
    }
}