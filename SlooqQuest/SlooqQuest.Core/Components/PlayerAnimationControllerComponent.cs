using System.Diagnostics;
using Geisha.Engine.Animation.Components;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;

namespace Sokoban.Core.Components
{
    public sealed class PlayerAnimationControllerComponent : BehaviorComponent
    {
        private TileObjectPositionComponent _tileObjectPositionComponent = null!;
        private SpriteAnimationComponent _spriteAnimationComponent = null!;
        private const int StandAnimationThreshold = 3;
        private int _standingFrameCounter = 0;

        public PlayerAnimationControllerComponent(Entity entity) : base(entity)
        {
        }

        public override void OnStart()
        {
            _tileObjectPositionComponent = Entity.GetComponent<TileObjectPositionComponent>();
            _spriteAnimationComponent = Entity.GetComponent<SpriteAnimationComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            Debug.Assert(_spriteAnimationComponent.CurrentAnimation != null, "_spriteAnimationComponent.CurrentAnimation != null");

            var deltaTranslation = _tileObjectPositionComponent.TargetTranslation - _tileObjectPositionComponent.CurrentTranslation;

            if (deltaTranslation.Y > 0)
            {
                StartAnimation("MoveUp");
                _standingFrameCounter = 0;
            }

            if (deltaTranslation.Y < 0)
            {
                StartAnimation("MoveDown");
                _standingFrameCounter = 0;
            }

            if (deltaTranslation.X < 0)
            {
                StartAnimation("MoveLeft");
                _standingFrameCounter = 0;
            }

            if (deltaTranslation.X > 0)
            {
                StartAnimation("MoveRight");
                _standingFrameCounter = 0;
            }

            if (deltaTranslation.Length == 0)
            {
                _standingFrameCounter++;
            }

            if (_standingFrameCounter > StandAnimationThreshold)
            {
                switch (_spriteAnimationComponent.CurrentAnimation.Value.Name)
                {
                    case "MoveUp":
                        StartAnimation("StandUp");
                        break;
                    case "MoveDown":
                        StartAnimation("StandDown");
                        break;
                    case "MoveLeft":
                        StartAnimation("StandLeft");
                        break;
                    case "MoveRight":
                        StartAnimation("StandRight");
                        break;
                }
            }
        }

        private void StartAnimation(string name)
        {
            Debug.Assert(_spriteAnimationComponent.CurrentAnimation != null, "_spriteAnimationComponent.CurrentAnimation != null");

            if (_spriteAnimationComponent.CurrentAnimation.Value.Name != name)
            {
                _spriteAnimationComponent.PlayAnimation(name);
            }
        }
    }

    public sealed class PlayerAnimationControllerComponentFactory : ComponentFactory<PlayerAnimationControllerComponent>
    {
        protected override PlayerAnimationControllerComponent CreateComponent(Entity entity) => new PlayerAnimationControllerComponent(entity);
    }
}