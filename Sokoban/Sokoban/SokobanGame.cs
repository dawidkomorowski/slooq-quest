using System.Reflection;
using Autofac;
using Geisha.Engine;
using Sokoban.Core;
using Sokoban.InGameMenu;
using Sokoban.LevelComplete;
using Sokoban.MainMenu;
using Sokoban.RestartLevel;
using Sokoban.VisualEffects;

namespace Sokoban
{
    // TODO Feature Request: Custom Window Icon?
    // TODO TextRendererComponent should support text alignment.
    // TODO Ability to change visibility of whole hierarchy of renderers.
    // TODO Multiple executables in same directory are forced to share engine-config.json.
    // TODO TextRendererComponent should have default font size (non zero) and default color (non transparent)?
    // TODO Support for volume control for audio playback.
    // TODO Native support for looped sound playback.
    // TODO Ability to set opacity for sprite renderer.
    // TODO Enter keypress to run the .exe makes handling of input components to execute on Enter key bindings. Input component created on action binding will trigger again the same binding.
    // TODO Clear All Bindings API for InputComponent.
    // TODO Enable/Disable InputComponent API?

    internal sealed class SokobanGame : IGame
    {
        private static string EngineInformation =>
            $"Geisha Engine {Assembly.GetAssembly(typeof(IGame))?.GetName().Version?.ToString(3)}";

        public string WindowTitle => $"Sokoban {Assembly.GetAssembly(typeof(SokobanGame))?.GetName().Version?.ToString(2)} ({EngineInformation})";

        public void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            SokobanCoreModule.RegisterComponents(componentsRegistry);

            componentsRegistry.AutofacContainerBuilder.RegisterType<GameAudio>().AsSelf().SingleInstance();
            componentsRegistry.AutofacContainerBuilder.RegisterType<GameState>().AsSelf().SingleInstance();
            componentsRegistry.RegisterSceneBehaviorFactory<MainSceneBehaviorFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<ModeInfo>().As<IModeInfo>().SingleInstance();
            componentsRegistry.RegisterSceneBehaviorFactory<SokobanGameSceneBehaviorFactory>();

            // InGameMenu
            componentsRegistry.RegisterComponentFactory<InGameMenuComponentFactory>();
            componentsRegistry.RegisterComponentFactory<InGameMenuOptionComponentFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<InGameMenuEntityFactory>().AsSelf().SingleInstance();

            // LevelComplete
            componentsRegistry.RegisterComponentFactory<LevelCompleteComponentFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<LevelCompleteEntityFactory>().AsSelf().SingleInstance();

            // MainMenu
            componentsRegistry.RegisterComponentFactory<MainMenuComponentFactory>();
            componentsRegistry.RegisterComponentFactory<MainMenuOptionComponentFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<MainMenuEntityFactory>().AsSelf().SingleInstance();
            componentsRegistry.RegisterSceneBehaviorFactory<MainMenuSceneBehaviorFactory>();

            // RestartLevel
            componentsRegistry.RegisterComponentFactory<RestartLevelComponentFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<RestartLevelEntityFactory>().AsSelf().SingleInstance();

            // Visual Effects
            componentsRegistry.RegisterComponentFactory<FadeInOutComponentFactory>();
        }
    }
}