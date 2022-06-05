using System.Reflection;
using Autofac;
using Geisha.Engine;
using Sokoban.Core;

namespace Sokoban
{
    // TODO Feature Request: IComponentsRegistry.RegisterSingleInstance<TImplementation>();
    // TODO Feature Request: IComponentsRegistry.RegisterSingleInstance<TImplementation, TInterface>();
    // TODO Feature Request: Custom Window Icon?

    internal sealed class SokobanGame : IGame
    {
        private static string EngineInformation =>
            $"Geisha Engine {Assembly.GetAssembly(typeof(IGame))?.GetName().Version?.ToString(3)}";

        public string WindowTitle => $"Sokoban {Assembly.GetAssembly(typeof(SokobanGame))?.GetName().Version?.ToString(2)} ({EngineInformation})";

        public void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            // Sokoban.Core
            componentsRegistry.AutofacContainerBuilder.RegisterType<EntityFactory>().AsSelf().SingleInstance();

            // Sokoban
            componentsRegistry.RegisterSceneBehaviorFactory<SokobanGameSceneBehaviorFactory>();
        }
    }
}