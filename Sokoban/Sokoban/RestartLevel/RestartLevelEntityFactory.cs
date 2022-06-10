using Geisha.Engine.Core.SceneModel;

namespace Sokoban.RestartLevel
{
    internal sealed class RestartLevelEntityFactory
    {
        public void CreateRestartLevelEntity(Scene scene)
        {
            var entity = scene.CreateEntity();
            entity.CreateComponent<RestartLevelComponent>();
        }
    }
}