using System;
using System.Diagnostics;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Sokoban.Core.LevelModel;
using Sokoban.Core.Util;

namespace Sokoban.Core.Components
{
    public sealed class TileObjectPositionComponent : BehaviorComponent
    {
        private Transform2DComponent _transform2D = null!;
        private readonly TimeSpan _animationDuration = TimeSpan.FromMilliseconds(250);
        private TimeSpan _timer = TimeSpan.Zero;
        private Vector2 _animationStartTranslation = Vector2.Zero;

        public TileObjectPositionComponent(Entity entity) : base(entity)
        {
        }

        public TileObject? TileObject { get; set; }

        public bool IsAnimating { get; private set; } = false;
        public Vector2 CurrentTranslation => _transform2D.Translation;
        public Vector2 TargetTranslation { get; private set; } = Vector2.Zero;

        public override void OnStart()
        {
            _transform2D = Entity.GetComponent<Transform2DComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            Debug.Assert(TileObject != null, nameof(TileObject) + " != null");

            TargetTranslation = TileObject.Tile.GetTranslation();

            if (_transform2D.Translation != TargetTranslation)
            {
                IsAnimating = true;
                _timer += gameTime.DeltaTime;

                var ratio = _timer / _animationDuration;
                var deltaTranslation = TargetTranslation - _animationStartTranslation;
                _transform2D.Translation = _animationStartTranslation + deltaTranslation * ratio;

                if (_timer >= _animationDuration)
                {
                    _transform2D.Translation = TargetTranslation;
                }
            }
            else
            {
                IsAnimating = false;
                _timer = TimeSpan.Zero;
                _animationStartTranslation = _transform2D.Translation;
            }
        }
    }

    public sealed class TileObjectPositionComponentFactory : ComponentFactory<TileObjectPositionComponent>
    {
        protected override TileObjectPositionComponent CreateComponent(Entity entity) => new TileObjectPositionComponent(entity);
    }
}