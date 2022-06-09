using Autofac;
using Geisha.Engine;
using Sokoban.Core.Components;

namespace Sokoban.Core
{
    public static class SokobanCoreModule
    {
        public static void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            componentsRegistry.AutofacContainerBuilder.RegisterType<CoreEntityFactory>().AsSelf().SingleInstance();
            componentsRegistry.RegisterComponentFactory<TileObjectPositionComponentFactory>();
            componentsRegistry.RegisterComponentFactory<PlayerControllerComponentFactory>();
        }
    }
}