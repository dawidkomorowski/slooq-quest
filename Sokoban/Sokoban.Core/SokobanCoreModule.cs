using Autofac;
using Geisha.Engine;
using Sokoban.Core.Components;
using Sokoban.Core.SceneLoading;

namespace Sokoban.Core
{
    public static class SokobanCoreModule
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