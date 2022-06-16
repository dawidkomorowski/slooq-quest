using System.Reflection;
using Autofac;
using Geisha.Engine;
using Sokoban.Core;
using Sokoban.InGameMenu;
using Sokoban.LevelComplete;
using Sokoban.RestartLevel;

namespace Sokoban
{
    // TODO Feature Request: IComponentsRegistry.RegisterSingleInstance<TImplementation>();
    // TODO Feature Request: IComponentsRegistry.RegisterSingleInstance<TImplementation, TInterface>();
    // TODO Feature Request: Custom Window Icon?
    // TODO Better exception information when entity in hierarchy does not have Transform2DComponent.
    // TODO TextRendererComponent should support text alignment.
    // TODO Ability to change visibility of whole hierarchy of renderers.
    // BUG Cannot inject ISceneManager to component.
    // BUG Cannot inject ISceneManager to scene behavior.
    // TODO Multiple executables in same directory are forced to share engine-config.json.
    // TODO Prevent default behavior of F10 key press.

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