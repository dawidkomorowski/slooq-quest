using System.Reflection;
using Autofac;
using Geisha.Engine;
using Sokoban.Core;
using Sokoban.CutScenes;
using Sokoban.CutScenes.Intro;
using Sokoban.CutScenes.IntroToFinal;
using Sokoban.InGameMenu;
using Sokoban.LevelComplete;
using Sokoban.LevelSelectionMenu;
using Sokoban.MainMenu;
using Sokoban.RestartLevel;
using Sokoban.VisualEffects;

namespace Sokoban
{
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

            // CutScenes
            componentsRegistry.RegisterComponentFactory<SpeechBalloonComponentFactory>();

            // CutScenes - Intro
            componentsRegistry.RegisterComponentFactory<IntroCutSceneComponentFactory>();
            componentsRegistry.RegisterSceneBehaviorFactory<IntroSceneBehaviorFactory>();

            // CutScenes - IntroToFinal
            componentsRegistry.RegisterComponentFactory<IntroToFinalCutSceneComponentFactory>();
            componentsRegistry.RegisterSceneBehaviorFactory<IntroToFinalSceneBehaviorFactory>();

            // InGameMenu
            componentsRegistry.RegisterComponentFactory<InGameMenuComponentFactory>();
            componentsRegistry.RegisterComponentFactory<InGameMenuOptionComponentFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<InGameMenuEntityFactory>().AsSelf().SingleInstance();

            // LevelComplete
            componentsRegistry.RegisterComponentFactory<LevelCompleteComponentFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<LevelCompleteEntityFactory>().AsSelf().SingleInstance();

            // LevelSelectionMenu
            componentsRegistry.RegisterComponentFactory<LevelSelectionMenuComponentFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<LevelSelectionMenuEntityFactory>().AsSelf().SingleInstance();
            componentsRegistry.RegisterSceneBehaviorFactory<LevelSelectionMenuSceneBehaviorFactory>();

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
