using System.Reflection;
using Autofac;
using Geisha.Engine;
using SlooqQuest.Core;
using SlooqQuest.Credits;
using SlooqQuest.CutScenes;
using SlooqQuest.CutScenes.Final;
using SlooqQuest.CutScenes.Intro;
using SlooqQuest.CutScenes.IntroToFinal;
using SlooqQuest.InGameMenu;
using SlooqQuest.LevelComplete;
using SlooqQuest.LevelSelectionMenu;
using SlooqQuest.MainMenu;
using SlooqQuest.RestartLevel;
using SlooqQuest.VisualEffects;

namespace SlooqQuest
{
    internal sealed class SokobanGame : IGame
    {
        private static string EngineInformation =>
            $"Geisha Engine {Assembly.GetAssembly(typeof(IGame))?.GetName().Version?.ToString(3)}";

        public string WindowTitle => $"Slooq Quest {Assembly.GetAssembly(typeof(SokobanGame))?.GetName().Version?.ToString(2)} ({EngineInformation})";

        public void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            SokobanCoreModule.RegisterComponents(componentsRegistry);

            componentsRegistry.AutofacContainerBuilder.RegisterType<GameAudio>().AsSelf().SingleInstance();
            componentsRegistry.AutofacContainerBuilder.RegisterType<GameState>().AsSelf().SingleInstance();
            componentsRegistry.RegisterSceneBehaviorFactory<MainSceneBehaviorFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<ModeInfo>().As<IModeInfo>().SingleInstance();
            componentsRegistry.RegisterSceneBehaviorFactory<SokobanGameSceneBehaviorFactory>();

            // Credits
            componentsRegistry.RegisterSceneBehaviorFactory<CreditsSceneBehaviorFactory>();
            componentsRegistry.AutofacContainerBuilder.RegisterType<CreditsEntityFactory>().AsSelf().SingleInstance();
            componentsRegistry.RegisterComponentFactory<CreditsTextComponentFactory>();

            // CutScenes
            componentsRegistry.RegisterComponentFactory<SpeechBalloonComponentFactory>();

            // CutScenes - Intro
            componentsRegistry.RegisterComponentFactory<IntroCutSceneComponentFactory>();
            componentsRegistry.RegisterSceneBehaviorFactory<IntroSceneBehaviorFactory>();

            // CutScenes - IntroToFinal
            componentsRegistry.RegisterComponentFactory<IntroToFinalCutSceneComponentFactory>();
            componentsRegistry.RegisterSceneBehaviorFactory<IntroToFinalSceneBehaviorFactory>();

            // CutScenes - Final
            componentsRegistry.RegisterComponentFactory<FinalCutSceneComponentFactory>();
            componentsRegistry.RegisterSceneBehaviorFactory<FinalSceneBehaviorFactory>();

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