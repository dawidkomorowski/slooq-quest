using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;

namespace Sokoban.MainMenu
{
    internal sealed class MainMenuOptionComponent : BehaviorComponent
    {
        private Transform2DComponent _transform2DComponent = null!;
        private TextRendererComponent _textRendererComponent = null!;

        public MainMenuOptionComponent(Entity entity) : base(entity)
        {
        }

        public MainMenuOption? MainMenuOption { get; set; }

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
            _textRendererComponent.FontSize = FontSize.FromDips(60);
            _textRendererComponent.SortingLayerName = "UI";
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (MainMenuOption is null)
            {
                return;
            }

            if (MainMenuOption.IsSelected)
            {
                _transform2DComponent.Translation = new Vector2(-600, 50 - MainMenuOption.Index * 100);
                _textRendererComponent.FontSize = FontSize.FromDips(100);
            }
            else
            {
                _transform2DComponent.Translation = new Vector2(-600, 50 - MainMenuOption.Index * 100);
                _textRendererComponent.FontSize = FontSize.FromDips(60);
            }
        }
    }

    internal sealed class MainMenuOptionComponentFactory : ComponentFactory<MainMenuOptionComponent>
    {
        protected override MainMenuOptionComponent CreateComponent(Entity entity) => new MainMenuOptionComponent(entity);
    }
}