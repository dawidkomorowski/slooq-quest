using System;
using System.Diagnostics;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using Sokoban.Core.GameLogic;

namespace Sokoban.Components
{
    internal sealed class PlayerControllerComponent : BehaviorComponent
    {
        private InputComponent _inputComponent = null!;
        private TimeSpan _timer;

        public PlayerControllerComponent(Entity entity) : base(entity)
        {
        }

        public GameMode? GameMode { get; set; }

        public override void OnStart()
        {
            _inputComponent = Entity.GetComponent<InputComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _timer += gameTime.DeltaTime;
            if (_timer <= TimeSpan.FromMilliseconds(200)) return;

            Debug.Assert(GameMode != null, nameof(GameMode) + " != null");

            if (_inputComponent.GetActionState("MoveUp"))
            {
                GameMode.MoveUp();
                _timer = TimeSpan.Zero;
            }

            if (_inputComponent.GetActionState("MoveDown"))
            {
                GameMode.MoveDown();
                _timer = TimeSpan.Zero;
            }

            if (_inputComponent.GetActionState("MoveLeft"))
            {
                GameMode.MoveLeft();
                _timer = TimeSpan.Zero;
            }

            if (_inputComponent.GetActionState("MoveRight"))
            {
                GameMode.MoveRight();
                _timer = TimeSpan.Zero;
            }
        }
    }

    internal sealed class PlayerControllerComponentFactory : ComponentFactory<PlayerControllerComponent>
    {
        protected override PlayerControllerComponent CreateComponent(Entity entity) => new PlayerControllerComponent(entity);
    }
}