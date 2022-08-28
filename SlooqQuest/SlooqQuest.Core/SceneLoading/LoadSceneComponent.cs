using Geisha.Engine.Core.SceneModel;

namespace SlooqQuest.Core.SceneLoading
{
    public sealed class LoadSceneComponent : Component
    {
        public LoadSceneComponent(Entity entity) : base(entity)
        {
        }

        public string SceneBehaviorName { get; set; } = string.Empty;
    }

    public sealed class LoadSceneComponentFactory : ComponentFactory<LoadSceneComponent>
    {
        protected override LoadSceneComponent CreateComponent(Entity entity) => new LoadSceneComponent(entity);
    }
}