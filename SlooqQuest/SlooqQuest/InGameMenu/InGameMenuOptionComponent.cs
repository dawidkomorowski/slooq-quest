using System;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;

namespace SlooqQuest.InGameMenu
{
    internal sealed class InGameMenuOptionComponent : BehaviorComponent
    {
        private Transform2DComponent _transform2DComponent = null!;

        public InGameMenuOptionComponent(Entity entity) : base(entity)
        {
        }

        public int Index { get; set; }
        public bool IsSelected { get; set; }
        public Action? Action { get; set; }

        public override void OnStart()
        {
            _transform2DComponent = Entity.GetComponent<Transform2DComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _transform2DComponent.Scale = IsSelected ? Vector2.One : new Vector2(0.5, 0.5);
        }
    }

    internal sealed class InGameMenuOptionComponentFactory : ComponentFactory<InGameMenuOptionComponent>
    {
        protected override InGameMenuOptionComponent CreateComponent(Entity entity) => new InGameMenuOptionComponent(entity);
    }
}