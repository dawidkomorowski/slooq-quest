using System;
using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;
using Sokoban.Core;
using Sokoban.Core.Components;
using Sokoban.Editor.UserInterface;

namespace Sokoban.Editor.ToggleMode
{
    internal sealed class EnterModeComponent : BehaviorComponent
    {
        private readonly EditorState _editorState;
        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly UserInterfaceEntityFactory _userInterfaceEntityFactory;
        private bool _previousModeCleanedUp = false;

        public EnterModeComponent(Entity entity, EditorState editorState, CoreEntityFactory coreEntityFactory,
            UserInterfaceEntityFactory userInterfaceEntityFactory) : base(entity)
        {
            _editorState = editorState;
            _coreEntityFactory = coreEntityFactory;
            _userInterfaceEntityFactory = userInterfaceEntityFactory;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            switch (_editorState.Mode)
            {
                case Mode.Edit:
                    EnterEditMode();
                    break;
                case Mode.Game:
                    EnterGameMode();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void EnterEditMode()
        {
            if (_previousModeCleanedUp == false)
            {
                var levelEntity = Scene.AllEntities.SingleOrDefault(e => e.Name == "Level");
                var playerControllerEntity = Scene.AllEntities.SingleOrDefault(e => e.HasComponent<PlayerControllerComponent>());

                levelEntity?.RemoveAfterFullFrame();
                playerControllerEntity?.RemoveAfterFullFrame();

                _previousModeCleanedUp = true;
            }
            else
            {
                var cameraEntity = Scene.AllEntities.Single(e => e.HasComponent<CameraComponent>());

                var levelEntity = _coreEntityFactory.CreateLevel(Scene, _editorState.EditMode.Level);
                levelEntity.Parent = cameraEntity;

                var cursor = _userInterfaceEntityFactory.CreateCursor(Scene);
                cursor.Parent = levelEntity;

                Entity.RemoveAfterFullFrame();
            }
        }

        private void EnterGameMode()
        {
            if (_previousModeCleanedUp == false)
            {
                var levelEntity = Scene.AllEntities.SingleOrDefault(e => e.Name == "Level");
                levelEntity?.RemoveAfterFullFrame();

                _previousModeCleanedUp = true;
            }
            else
            {
                var cameraEntity = Scene.AllEntities.Single(e => e.HasComponent<CameraComponent>());

                var levelEntity = _coreEntityFactory.CreateLevel(Scene, _editorState.GameMode.Level);
                levelEntity.Parent = cameraEntity;

                _coreEntityFactory.CreatePlayerController(Scene, _editorState.GameMode);

                Entity.RemoveAfterFullFrame();
            }
        }
    }

    internal sealed class EnterModeComponentFactory : ComponentFactory<EnterModeComponent>
    {
        private readonly EditorState _editorState;
        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly UserInterfaceEntityFactory _userInterfaceEntityFactory;

        public EnterModeComponentFactory(EditorState editorState, CoreEntityFactory coreEntityFactory, UserInterfaceEntityFactory userInterfaceEntityFactory)
        {
            _editorState = editorState;
            _coreEntityFactory = coreEntityFactory;
            _userInterfaceEntityFactory = userInterfaceEntityFactory;
        }

        protected override EnterModeComponent CreateComponent(Entity entity) =>
            new EnterModeComponent(entity, _editorState, _coreEntityFactory, _userInterfaceEntityFactory);
    }
}