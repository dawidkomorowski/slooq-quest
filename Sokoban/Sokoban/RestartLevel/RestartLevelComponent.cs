using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;
using Sokoban.Core;
using Sokoban.Core.Components;
using Sokoban.Core.GameLogic;
using Sokoban.Core.LevelModel;

namespace Sokoban.RestartLevel
{
    internal sealed class RestartLevelComponent : BehaviorComponent
    {
        private readonly CoreEntityFactory _coreEntityFactory;
        private bool _readyForRestart;

        public RestartLevelComponent(Entity entity, CoreEntityFactory coreEntityFactory) : base(entity)
        {
            _coreEntityFactory = coreEntityFactory;
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

                var level = Level.CreateTestLevel();
                var gameMode = new GameMode(level);

                var levelEntity = _coreEntityFactory.CreateLevel(Scene, level);
                levelEntity.Parent = cameraEntity;

                _coreEntityFactory.CreatePlayerController(Scene, gameMode);

                Entity.RemoveAfterFullFrame();
            }
        }
    }

    internal sealed class RestartLevelComponentFactory : ComponentFactory<RestartLevelComponent>
    {
        private readonly CoreEntityFactory _coreEntityFactory;

        public RestartLevelComponentFactory(CoreEntityFactory coreEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
        }

        protected override RestartLevelComponent CreateComponent(Entity entity) => new RestartLevelComponent(entity, _coreEntityFactory);
    }
}