using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;
using Sokoban.Core;
using Sokoban.Core.Components;

namespace Sokoban.RestartLevel
{
    internal sealed class RestartLevelComponent : BehaviorComponent
    {
        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly GameState _gameState;
        private bool _readyForRestart;

        public RestartLevelComponent(Entity entity, CoreEntityFactory coreEntityFactory, GameState gameState) : base(entity)
        {
            _coreEntityFactory = coreEntityFactory;
            _gameState = gameState;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (_readyForRestart == false)
            {
                var levelEntity = Scene.AllEntities.SingleOrDefault(e => e.Name == "Level");
                var playerControllerEntity = Scene.AllEntities.SingleOrDefault(e => e.HasComponent<PlayerControllerComponent>());

                levelEntity?.RemoveAfterFullFrame();
                playerControllerEntity?.RemoveAfterFullFrame();

                _readyForRestart = true;
            }
            else
            {
                var cameraEntity = Scene.AllEntities.Single(e => e.HasComponent<CameraComponent>());

                _gameState.RecreateGameMode();

                var levelEntity = _coreEntityFactory.CreateLevel(Scene, _gameState.GameMode.Level);
                levelEntity.Parent = cameraEntity;

                _coreEntityFactory.CreatePlayerController(Scene, _gameState.GameMode);

                Entity.RemoveAfterFullFrame();
            }
        }
    }

    internal sealed class RestartLevelComponentFactory : ComponentFactory<RestartLevelComponent>
    {
        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly GameState _gameState;

        public RestartLevelComponentFactory(CoreEntityFactory coreEntityFactory, GameState gameState)
        {
            _coreEntityFactory = coreEntityFactory;
            _gameState = gameState;
        }

        protected override RestartLevelComponent CreateComponent(Entity entity) => new RestartLevelComponent(entity, _coreEntityFactory, _gameState);
    }
}