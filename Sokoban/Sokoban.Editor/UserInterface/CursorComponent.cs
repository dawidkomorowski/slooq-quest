using System;
using System.Diagnostics;
using System.Linq;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Rendering.Components;
using Sokoban.Core;
using Sokoban.Core.EditorLogic;
using Sokoban.Core.Util;

namespace Sokoban.Editor.UserInterface
{
    internal sealed class CursorComponent : BehaviorComponent
    {
        private readonly IEngineManager _engineManager;
        private readonly CoreEntityFactory _coreEntityFactory;
        private Transform2DComponent _transform = null!;

        public CursorComponent(Entity entity, IEngineManager engineManager, CoreEntityFactory coreEntityFactory) : base(entity)
        {
            _engineManager = engineManager;
            _coreEntityFactory = coreEntityFactory;
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
            inputComponent.BindAction("Delete", ReloadLevelAfter(EditMode.Delete));
            inputComponent.BindAction("CreateRedGrayWall", ReloadLevelAfter(EditMode.CreateRedGrayWall));
            inputComponent.BindAction("CreateBrownCrate", ReloadLevelAfter(EditMode.CreateBrownCrate));
            inputComponent.BindAction("CreateRedCrate", ReloadLevelAfter(EditMode.CreateRedCrate));
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

        private void ReloadLevel()
        {
            Debug.Assert(EditMode != null, nameof(EditMode) + " != null");

            var camera = Scene.RootEntities.Single(e => e.HasComponent<CameraComponent>());

            var currentLevelRoot = Scene.AllEntities.Single(e => e.Name == "Level");
            currentLevelRoot.RemoveAfterFullFrame();

            var newLevelRoot = _coreEntityFactory.CreateLevel(Scene, EditMode.Level);
            newLevelRoot.Parent = camera;

            Entity.Parent = newLevelRoot;
        }

        private Action ReloadLevelAfter(Action action)
        {
            return () =>
            {
                action();
                ReloadLevel();
            };
        }
    }

    internal sealed class CursorComponentFactory : ComponentFactory<CursorComponent>
    {
        private readonly IEngineManager _engineManager;
        private readonly CoreEntityFactory _coreEntityFactory;

        public CursorComponentFactory(IEngineManager engineManager, CoreEntityFactory coreEntityFactory)
        {
            _engineManager = engineManager;
            _coreEntityFactory = coreEntityFactory;
        }

        protected override CursorComponent CreateComponent(Entity entity) => new CursorComponent(entity, _engineManager, _coreEntityFactory);
    }
}