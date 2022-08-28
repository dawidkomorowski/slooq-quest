using System;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;

namespace SlooqQuest.CutScenes
{
    internal sealed class SpeechBalloonComponent : BehaviorComponent
    {
        private Transform2DComponent _transform2D = null!;

        private RectangleRendererComponent _borderRenderer = null!;
        private RectangleRendererComponent _insideRenderer = null!;

        private Transform2DComponent _textLine1Transform2D = null!;
        private TextRendererComponent _textLine1Renderer = null!;

        private Transform2DComponent _textLine2Transform2D = null!;
        private TextRendererComponent _textLine2Renderer = null!;

        private State _state = State.Idle;

        private TimeSpan _timer = TimeSpan.Zero;
        private readonly TimeSpan _duration = TimeSpan.FromMilliseconds(250);

        public SpeechBalloonComponent(Entity entity) : base(entity)
        {
        }

        public override void OnStart()
        {
            _transform2D = Entity.CreateComponent<Transform2DComponent>();

            var border = Entity.CreateChildEntity();
            border.CreateComponent<Transform2DComponent>();
            _borderRenderer = border.CreateComponent<RectangleRendererComponent>();
            _borderRenderer.Color = Color.FromArgb(255, 0, 0, 0);
            _borderRenderer.FillInterior = true;
            _borderRenderer.SortingLayerName = "UI";
            _borderRenderer.OrderInLayer = 0;

            var inside = Entity.CreateChildEntity();
            inside.CreateComponent<Transform2DComponent>();
            _insideRenderer = inside.CreateComponent<RectangleRendererComponent>();
            _insideRenderer.Color = Color.FromArgb(255, 255, 255, 255);
            _insideRenderer.FillInterior = true;
            _insideRenderer.SortingLayerName = "UI";
            _insideRenderer.OrderInLayer = 1;

            SetDimensions(100, 50);

            var textLine1 = Entity.CreateChildEntity();
            _textLine1Transform2D = textLine1.CreateComponent<Transform2DComponent>();
            _textLine1Renderer = textLine1.CreateComponent<TextRendererComponent>();
            _textLine1Renderer.Color = Color.FromArgb(255, 0, 0, 0);
            _textLine1Renderer.Text = string.Empty;
            _textLine1Renderer.SortingLayerName = "UI";
            _textLine1Renderer.OrderInLayer = 2;
            _textLine1Renderer.FontSize = FontSize.FromDips(25);

            var textLine2 = Entity.CreateChildEntity();
            _textLine2Transform2D = textLine2.CreateComponent<Transform2DComponent>();
            _textLine2Renderer = textLine2.CreateComponent<TextRendererComponent>();
            _textLine2Renderer.Color = Color.FromArgb(255, 0, 0, 0);
            _textLine2Renderer.Text = string.Empty;
            _textLine2Renderer.SortingLayerName = "UI";
            _textLine2Renderer.OrderInLayer = 2;
            _textLine2Renderer.FontSize = FontSize.FromDips(25);

            SetAlpha(0);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            switch (_state)
            {
                case State.Idle:
                    _timer = TimeSpan.Zero;
                    break;
                case State.Showing:
                    _timer += gameTime.DeltaTime;

                    if (_timer > _duration)
                    {
                        _timer = _duration;
                        _state = State.Idle;
                    }

                    SetAlpha(_timer / _duration);

                    break;
                case State.Hiding:
                    _timer += gameTime.DeltaTime;

                    if (_timer > _duration)
                    {
                        _timer = _duration;
                        _state = State.Idle;
                    }

                    SetAlpha(1d - _timer / _duration);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Show()
        {
            _state = State.Showing;
        }

        public void Hide()
        {
            _state = State.Hiding;
            _textLine1Renderer.Text = string.Empty;
            _textLine2Renderer.Text = string.Empty;
        }

        public bool WaitForAnimation()
        {
            return _state == State.Idle;
        }

        public void SetPosition(int x, int y)
        {
            _transform2D.Translation = new Vector2(x, y);
        }

        public void SetDimensions(int width, int height)
        {
            _borderRenderer.Dimension = new Vector2(width, height);
            _insideRenderer.Dimension = new Vector2(width - 10, height - 10);
        }

        public void SetTextLine1(string text, int x, int y)
        {
            _textLine1Renderer.Text = text;
            _textLine1Transform2D.Translation = new Vector2(x, y);
        }

        public void SetTextLine2(string text, int x, int y)
        {
            _textLine2Renderer.Text = text;
            _textLine2Transform2D.Translation = new Vector2(x, y);
        }

        private void SetAlpha(double alpha)
        {
            _borderRenderer.Color = Color.FromArgb(alpha, _borderRenderer.Color.DoubleR, _borderRenderer.Color.DoubleG, _borderRenderer.Color.DoubleB);
            _insideRenderer.Color = Color.FromArgb(alpha, _insideRenderer.Color.DoubleR, _insideRenderer.Color.DoubleG, _insideRenderer.Color.DoubleB);
            _textLine1Renderer.Color =
                Color.FromArgb(alpha, _textLine1Renderer.Color.DoubleR, _textLine1Renderer.Color.DoubleG, _textLine1Renderer.Color.DoubleB);
            _textLine2Renderer.Color =
                Color.FromArgb(alpha, _textLine2Renderer.Color.DoubleR, _textLine2Renderer.Color.DoubleG, _textLine2Renderer.Color.DoubleB);
        }

        private enum State
        {
            Idle,
            Showing,
            Hiding
        }
    }

    internal sealed class SpeechBalloonComponentFactory : ComponentFactory<SpeechBalloonComponent>
    {
        protected override SpeechBalloonComponent CreateComponent(Entity entity) => new SpeechBalloonComponent(entity);
    }
}