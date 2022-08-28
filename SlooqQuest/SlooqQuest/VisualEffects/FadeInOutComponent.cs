using System;
using System.Linq;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;

namespace Sokoban.VisualEffects
{
    internal sealed class FadeInOutComponent : BehaviorComponent
    {
        private RectangleRendererComponent _rectangleRenderer = null!;
        private TimeSpan _lifeSpan = TimeSpan.Zero;

        public FadeInOutComponent(Entity entity) : base(entity)
        {
        }

        public enum FadeMode
        {
            FadeIn,
            FadeOut
        }

        public TimeSpan Duration { get; set; }
        public FadeMode Mode { get; set; } = FadeMode.FadeIn;
        public Action? Action { get; set; }

        public override void OnStart()
        {
            Entity.CreateComponent<Transform2DComponent>();

            var cameraEntity = Scene.AllEntities.Single(e => e.HasComponent<CameraComponent>());
            Entity.Parent = cameraEntity;

            _rectangleRenderer = Entity.CreateComponent<RectangleRendererComponent>();
            _rectangleRenderer.SortingLayerName = "VFX";
            _rectangleRenderer.FillInterior = true;
            _rectangleRenderer.Dimension = new Vector2(2000, 2000);
            _rectangleRenderer.Color = Color.FromArgb(255, 0, 0, 0);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _lifeSpan += gameTime.DeltaTime;

            if (_lifeSpan > Duration)
            {
                _lifeSpan = Duration;
                Entity.RemoveAfterFullFrame();
                Action?.Invoke();
            }

            var value = Mode switch
            {
                FadeMode.FadeIn => 1d - _lifeSpan / Duration,
                FadeMode.FadeOut => _lifeSpan / Duration,
                _ => throw new ArgumentOutOfRangeException()
            };
            SetValue(value);
        }

        private void SetValue(double value)
        {
            _rectangleRenderer.Color = Color.FromArgb(value, 0d, 0d, 0d);
        }
    }

    internal sealed class FadeInOutComponentFactory : ComponentFactory<FadeInOutComponent>
    {
        protected override FadeInOutComponent CreateComponent(Entity entity) => new FadeInOutComponent(entity);
    }
}