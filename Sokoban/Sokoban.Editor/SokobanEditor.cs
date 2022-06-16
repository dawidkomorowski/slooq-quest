using System.Reflection;
using Autofac;
using Geisha.Engine;
using Sokoban.Core;
using Sokoban.Editor.UserInterface;

namespace Sokoban.Editor
{
    internal sealed class SokobanEditor : IGame
    {
        private static string EngineInformation =>
            $"Geisha Engine {Assembly.GetAssembly(typeof(IGame))?.GetName().Version?.ToString(3)}";

        public string WindowTitle => $"Sokoban Editor {Assembly.GetAssembly(typeof(SokobanEditor))?.GetName().Version?.ToString(2)} ({EngineInformation})";

        public void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            SokobanCoreModule.RegisterComponents(componentsRegistry);

            // UserInterface
            componentsRegistry.AutofacContainerBuilder.RegisterType<UserInterfaceEntityFactory>().AsSelf().SingleInstance();
            componentsRegistry.RegisterComponentFactory<CursorComponentFactory>();

            componentsRegistry.RegisterSceneBehaviorFactory<MainSceneBehaviorFactory>();
        }
    }
}