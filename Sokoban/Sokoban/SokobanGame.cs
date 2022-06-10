using System.Reflection;
using Autofac;
using Geisha.Engine;
using Sokoban.Core;
using Sokoban.InGameMenu;
using Sokoban.RestartLevel;

namespace Sokoban
{
    // TODO Feature Request: IComponentsRegistry.RegisterSingleInstance<TImplementation>();
    // TODO Feature Request: IComponentsRegistry.RegisterSingleInstance<TImplementation, TInterface>();
    // TODO Feature Request: Custom Window Icon?
    // TODO Better exception information when entity in hierarchy does not have Transform2DComponent.
    // TODO TextRendererComponent should support text alignment.
    // TODO Ability to change visibility of whole hierarchy of renderers.

    internal sealed class SokobanGame : IGame
    {
        private static string EngineInformation =>
            $"Geisha Engine {Assembly.GetAssembly(typeof(IGame))?.GetName().Version?.ToString(3)}";

        public string WindowTitle => $"Sokoban {Assembly.GetAssembly(typeof(SokobanGame))?.GetName().Version?.ToString(2)} ({EngineInformation})";

        public void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            SokobanCoreModule.RegisterComponents(componentsRegistry);

            componentsRegistry.RegisterSceneBehaviorFactory<SokobanGameSceneBehaviorFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<InGameMenuEntityFactory>().AsSelf().SingleInstance();
            componentsRegistry.AutofacContainerBuilder.RegisterType<RestartLevelEntityFactory>().AsSelf().SingleInstance();
            componentsRegistry.RegisterComponentFactory<InGameMenuComponentFactory>();
            componentsRegistry.RegisterComponentFactory<InGameMenuOptionComponentFactory>();
            componentsRegistry.RegisterComponentFactory<RestartLevelComponentFactory>();
        }
    }
}