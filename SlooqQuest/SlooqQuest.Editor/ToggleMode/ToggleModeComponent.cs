using System;
using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using SlooqQuest.Core;
using SlooqQuest.Core.Components;

namespace SlooqQuest.Editor.ToggleMode
{
    internal sealed class ToggleModeComponent : BehaviorComponent
    {
        private readonly EditorState _editorState;
        private readonly ToggleModeEntityFactory _toggleModeEntityFactory;
        private readonly TimeSpan _leaveGameModeDuration = TimeSpan.FromMilliseconds(500);
        private TimeSpan _leaveGameModeTimer = TimeSpan.Zero;
        private InputComponent _inputComponent = null!;

        public ToggleModeComponent(Entity entity, EditorState editorState, ToggleModeEntityFactory toggleModeEntityFactory) : base(entity)
        {
            _editorState = editorState;
            _toggleModeEntityFactory = toggleModeEntityFactory;
        }

        public override void OnStart()
        {
            _inputComponent = Entity.GetComponent<InputComponent>();
            _inputComponent.BindAction("ToggleMode", ToggleMode);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (_editorState.Mode is Mode.Game && _editorState.GameMode.IsLevelComplete())
            {
                if (Scene.AllEntities.Any(e => e.HasComponent<EnterModeComponent>()))
                {
                    return;
                }

                _leaveGameModeTimer += gameTime.DeltaTime;

                var playerControllerComponent = FindPlayerControllerComponent();
                playerControllerComponent.Enabled = false;

                if (_leaveGameModeTimer > _leaveGameModeDuration)
                {
                    _leaveGameModeTimer = TimeSpan.Zero;
                    ToggleMode();
                }
            }
        }

        private void ToggleMode()
        {
            if (Scene.AllEntities.Any(e => e.HasComponent<EnterModeComponent>()))
            {
                return;
            }

            _editorState.ToggleMode();
            _toggleModeEntityFactory.CreateEnterModeEntity(Scene);
        }

        private PlayerControllerComponent FindPlayerControllerComponent()
        {
            return Scene.AllEntities.Single(e => e.HasComponent<PlayerControllerComponent>()).GetComponent<PlayerControllerComponent>();
        }
    }

    internal sealed class ToggleModeComponentFactory : ComponentFactory<ToggleModeComponent>
    {
        private readonly EditorState _editorState;
        private readonly ToggleModeEntityFactory _toggleModeEntityFactory;

        public ToggleModeComponentFactory(EditorState editorState, ToggleModeEntityFactory toggleModeEntityFactory)
        {
            _editorState = editorState;
            _toggleModeEntityFactory = toggleModeEntityFactory;
        }

        protected override ToggleModeComponent CreateComponent(Entity entity) => new ToggleModeComponent(entity, _editorState, _toggleModeEntityFactory);
    }
}