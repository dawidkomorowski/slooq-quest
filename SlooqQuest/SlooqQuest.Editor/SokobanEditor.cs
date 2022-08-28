using System.Reflection;
using Autofac;
using Geisha.Engine;
using SlooqQuest.Core;
using SlooqQuest.Editor.ToggleMode;
using SlooqQuest.Editor.UserInterface;

namespace SlooqQuest.Editor
{
    internal sealed class SokobanEditor : IGame
    {
        private static string EngineInformation =>
            $"Geisha Engine {Assembly.GetAssembly(typeof(IGame))?.GetName().Version?.ToString(3)}";

        public string WindowTitle => $"Slooq Quest Editor {Assembly.GetAssembly(typeof(SokobanEditor))?.GetName().Version?.ToString(2)} ({EngineInformation})";

        public void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            SokobanCoreModule.RegisterComponents(componentsRegistry);

            // ToggleMode
            componentsRegistry.AutofacContainerBuilder.RegisterType<ToggleModeEntityFactory>().AsSelf().SingleInstance();
            componentsRegistry.RegisterComponentFactory<EnterModeComponentFactory>();
            componentsRegistry.RegisterComponentFactory<ToggleModeComponentFactory>();

            // UserInterface
            componentsRegistry.AutofacContainerBuilder.RegisterType<UserInterfaceEntityFactory>().AsSelf().SingleInstance();
            componentsRegistry.RegisterComponentFactory<CursorComponentFactory>();

            componentsRegistry.AutofacContainerBuilder.RegisterType<EditorState>().AsSelf().SingleInstance();
            componentsRegistry.RegisterSceneBehaviorFactory<MainSceneBehaviorFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<ModeInfo>().As<IModeInfo>().SingleInstance();
        }
    }
}