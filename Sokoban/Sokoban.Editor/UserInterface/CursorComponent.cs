﻿using System;
using System.Linq;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Rendering.Components;
using Sokoban.Core;
using Sokoban.Core.Util;

namespace Sokoban.Editor.UserInterface
{
    internal sealed class CursorComponent : BehaviorComponent
    {
        private readonly IEngineManager _engineManager;
        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly EditorState _editorState;
        private Transform2DComponent _transform = null!;

        public CursorComponent(Entity entity, IEngineManager engineManager, CoreEntityFactory coreEntityFactory, EditorState editorState) : base(entity)
        {
            _engineManager = engineManager;
            _coreEntityFactory = coreEntityFactory;
            _editorState = editorState;
        }

        public override void OnStart()
        {
            _transform = Entity.GetComponent<Transform2DComponent>();

            var inputComponent = Entity.GetComponent<InputComponent>();
            inputComponent.BindAction("MoveUp", _editorState.EditMode.MoveUp);
            inputComponent.BindAction("MoveDown", _editorState.EditMode.MoveDown);
            inputComponent.BindAction("MoveLeft", _editorState.EditMode.MoveLeft);
            inputComponent.BindAction("MoveRight", _editorState.EditMode.MoveRight);
            inputComponent.BindAction("Delete", _editorState.EditMode.Delete);
            inputComponent.BindAction("CreateRedGrayWall", _editorState.EditMode.CreateRedGrayWall);
            inputComponent.BindAction("CreateBrownCrate", _editorState.EditMode.CreateBrownCrate);
            inputComponent.BindAction("CreateRedCrate", _editorState.EditMode.CreateRedCrate);
            inputComponent.BindAction("CreateBrownCrateSpot", _editorState.EditMode.CreateBrownCrateSpot);
            inputComponent.BindAction("CreateRedCrateSpot", _editorState.EditMode.CreateRedCrateSpot);
            inputComponent.BindAction("PlacePlayer", _editorState.EditMode.PlacePlayer);
            inputComponent.BindAction("Exit", _engineManager.ScheduleEngineShutdown);

            _editorState.EditMode.LevelModified += EditModeOnLevelModified;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _transform.Translation = _editorState.EditMode.SelectedTile.GetTranslation();

            AnimateCursor();

            if (_editorState.Mode is Mode.Game)
            {
                _editorState.EditMode.LevelModified -= EditModeOnLevelModified;
            }
        }

        private void AnimateCursor()
        {
            var scale = 0.9 + Math.Sin(GameTime.TimeSinceStartUp.TotalSeconds * 3) * 0.1;
            _transform.Scale = new Vector2(scale, scale);
        }

        private void ReloadLevel()
        {
            var camera = Scene.RootEntities.Single(e => e.HasComponent<CameraComponent>());

            var currentLevelRoot = Scene.AllEntities.Single(e => e.Name == "Level");
            currentLevelRoot.RemoveAfterFullFrame();

            var newLevelRoot = _coreEntityFactory.CreateLevel(Scene, _editorState.EditMode.Level);
            newLevelRoot.Parent = camera;

            Entity.Parent = newLevelRoot;
        }

        private void EditModeOnLevelModified(object? sender, EventArgs e)
        {
            ReloadLevel();
        }
    }

    internal sealed class CursorComponentFactory : ComponentFactory<CursorComponent>
    {
        private readonly IEngineManager _engineManager;
        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly EditorState _editorState;

        public CursorComponentFactory(IEngineManager engineManager, CoreEntityFactory coreEntityFactory, EditorState editorState)
        {
            _engineManager = engineManager;
            _coreEntityFactory = coreEntityFactory;
            _editorState = editorState;
        }

        protected override CursorComponent CreateComponent(Entity entity) => new CursorComponent(entity, _engineManager, _coreEntityFactory, _editorState);
    }
}