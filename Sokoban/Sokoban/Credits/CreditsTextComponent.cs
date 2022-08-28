using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;

namespace Sokoban.Credits
{
    internal sealed class CreditsTextComponent : BehaviorComponent
    {
        private const double ScrollingSpeed = 75;
        private Transform2DComponent _transform = null!;

        public CreditsTextComponent(Entity entity) : base(entity)
        {
        }

        public override void OnStart()
        {
            _transform = Entity.GetComponent<Transform2DComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _transform.Translation += new Vector2(0, ScrollingSpeed) * gameTime.DeltaTime.TotalSeconds;
        }
    }

    internal sealed class CreditsTextComponentFactory : ComponentFactory<CreditsTextComponent>
    {
        protected override CreditsTextComponent CreateComponent(Entity entity) => new CreditsTextComponent(entity);
    }
}