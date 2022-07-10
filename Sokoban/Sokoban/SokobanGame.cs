using System.Reflection;
using Autofac;
using Geisha.Engine;
using Sokoban.Core;
using Sokoban.InGameMenu;
using Sokoban.LevelComplete;
using Sokoban.RestartLevel;

namespace Sokoban
{
    // TODO Feature Request: Custom Window Icon?
    // TODO TextRendererComponent should support text alignment.
    // TODO Ability to change visibility of whole hierarchy of renderers.
    // TODO Multiple executables in same directory are forced to share engine-config.json.
    // TODO TextRendererComponent should have default font size (non zero) and default color (non transparent)?

    internal sealed class SokobanGame : IGame
    {
        private static string EngineInformation =>
            $"Geisha Engine {Assembly.GetAssembly(typeof(IGame))?.GetName().Version?.ToString(3)}";

        public string WindowTitle => $"Sokoban {Assembly.GetAssembly(typeof(SokobanGame))?.GetName().Version?.ToString(2)} ({EngineInformation})";

        public void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            SokobanCoreModule.RegisterComponents(componentsRegistry);

            componentsRegistry.AutofacContainerBuilder.RegisterType<GameState>().AsSelf().SingleInstance();
            componentsRegistry.RegisterSceneBehaviorFactory<MainSceneBehaviorFactory>();
            componentsRegistry.RegisterSceneBehaviorFactory<SokobanGameSceneBehaviorFactory>();

            // InGameMenu
            componentsRegistry.RegisterComponentFactory<InGameMenuComponentFactory>();
            componentsRegistry.RegisterComponentFactory<InGameMenuOptionComponentFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<InGameMenuEntityFactory>().AsSelf().SingleInstance();

            // LevelComplete
            componentsRegistry.RegisterComponentFactory<LevelCompleteComponentFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<LevelCompleteEntityFactory>().AsSelf().SingleInstance();

            // RestartLevel
            componentsRegistry.RegisterComponentFactory<RestartLevelComponentFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<RestartLevelEntityFactory>().AsSelf().SingleInstance();
        }
    }
}
