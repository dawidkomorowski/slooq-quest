using System;
using System.Diagnostics;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using Sokoban.Core.EditorLogic;
using Sokoban.Core.Util;

namespace Sokoban.Editor.UserInterface
{
    internal sealed class CursorComponent : BehaviorComponent
    {
        private readonly IEngineManager _engineManager;
        private Transform2DComponent _transform = null!;

        public CursorComponent(Entity entity, IEngineManager engineManager) : base(entity)
        {
            _engineManager = engineManager;
        }

        public EditMode? EditMode { get; set; }

        public override void OnStart()
        {
            Debug.Assert(EditMode != null, nameof(EditMode) + " != null");

            _transform = Entity.GetComponent<Transform2DComponent>();

            var inputComponent = Entity.GetComponent<InputComponent>();
            inputComponent.BindAction("MoveUp", EditMode.MoveUp);
            inputComponent.BindAction("MoveDown", EditMode.MoveDown);
            inputComponent.BindAction("MoveLeft", EditMode.MoveLeft);
            inputComponent.BindAction("MoveRight", EditMode.MoveRight);
            inputComponent.BindAction("Exit", _engineManager.ScheduleEngineShutdown);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            Debug.Assert(EditMode != null, nameof(EditMode) + " != null");

            _transform.Translation = EditMode.SelectedTile.GetTranslation();

            AnimateCursor();
        }

        private void AnimateCursor()
        {
            var scale = 0.9 + Math.Sin(GameTime.TimeSinceStartUp.TotalSeconds * 3) * 0.1;
            _transform.Scale = new Vector2(scale, scale);
        }
    }

    internal sealed class CursorComponentFactory : ComponentFactory<CursorComponent>
    {
        private readonly IEngineManager _engineManager;

        public CursorComponentFactory(IEngineManager engineManager)
        {
            _engineManager = engineManager;
        }

        protected override CursorComponent CreateComponent(Entity entity) => new CursorComponent(entity, _engineManager);
    }
}