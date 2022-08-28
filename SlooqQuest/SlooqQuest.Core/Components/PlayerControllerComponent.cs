using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using SlooqQuest.Core.GameLogic;
using SlooqQuest.Core.LevelModel;

namespace SlooqQuest.Core.Components
{
    public sealed class PlayerControllerComponent : BehaviorComponent
    {
        private InputComponent _inputComponent = null!;
        private TileObjectPositionComponent _playerTileObjectPositionComponent = null!;

        public PlayerControllerComponent(Entity entity) : base(entity)
        {
        }

        public GameMode? GameMode { get; set; }
        public bool Enabled { get; set; } = true;

        public override void OnStart()
        {
            _inputComponent = Entity.GetComponent<InputComponent>();

            _playerTileObjectPositionComponent = Entity.Scene.AllEntities
                .Single(e => e.HasComponent<TileObjectPositionComponent>() && e.GetComponent<TileObjectPositionComponent>().TileObject is Player)
                .GetComponent<TileObjectPositionComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!Enabled)
            {
                return;
            }

            if (_playerTileObjectPositionComponent.IsAnimating)
            {
                return;
            }

            if (GameMode is null)
            {
                return;
            }

            if (_inputComponent.GetActionState("MoveUp"))
            {
                GameMode.MoveUp();
                return;
            }

            if (_inputComponent.GetActionState("MoveDown"))
            {
                GameMode.MoveDown();
                return;
            }

            if (_inputComponent.GetActionState("MoveLeft"))
            {
                GameMode.MoveLeft();
                return;
            }

            if (_inputComponent.GetActionState("MoveRight"))
            {
                GameMode.MoveRight();
                return;
            }
        }
    }

    public sealed class PlayerControllerComponentFactory : ComponentFactory<PlayerControllerComponent>
    {
        protected override PlayerControllerComponent CreateComponent(Entity entity) => new PlayerControllerComponent(entity);
    }
}