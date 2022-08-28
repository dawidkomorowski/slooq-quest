using Autofac;
using Geisha.Engine;
using SlooqQuest.Core.Components;
using SlooqQuest.Core.SceneLoading;

namespace SlooqQuest.Core
{
    public static class SlooqQuestCoreModule
    {
        public static void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            // Components
            componentsRegistry.RegisterComponentFactory<CrateRendererComponentFactory>();
            componentsRegistry.RegisterComponentFactory<PlayerAnimationControllerComponentFactory>();
            componentsRegistry.RegisterComponentFactory<PlayerControllerComponentFactory>();
            componentsRegistry.RegisterComponentFactory<TileObjectPositionComponentFactory>();

            // SceneLoading
            componentsRegistry.RegisterComponentFactory<LoadSceneComponentFactory>();
            componentsRegistry.RegisterSystem<SceneLoadingSystem>();

            componentsRegistry.AutofacContainerBuilder.RegisterType<CoreEntityFactory>().AsSelf().SingleInstance();
        }
    }
}