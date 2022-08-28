using System;
using System.IO;
using Geisha.Engine.Core.SceneModel;
using SlooqQuest.Core;
using SlooqQuest.Core.GameLogic;
using SlooqQuest.Core.LevelModel;
using SlooqQuest.VisualEffects;

namespace SlooqQuest.CutScenes.Final
{
    internal sealed class FinalSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "FinalCutScene";

        private readonly CoreEntityFactory _coreEntityFactory;

        public FinalSceneBehaviorFactory(CoreEntityFactory coreEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
        }

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new FinalSceneBehavior(scene, _coreEntityFactory);

        private sealed class FinalSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;

            public FinalSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                var cameraEntity = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = cameraEntity;

                var fadeInOutEntity = Scene.CreateEntity();
                var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                fadeInOutComponent.Duration = TimeSpan.FromSeconds(1);

                var serializedLevel = File.ReadAllText(Path.Combine("CutScenes", "Final.sokoban-level"));
                var level = Level.Deserialize(serializedLevel);
                var gameMode = new GameMode(level);

                var levelEntity = _coreEntityFactory.CreateLevel(Scene, gameMode.Level);
                levelEntity.Parent = cameraEntity;

                var finalCutSceneEntity = Scene.CreateEntity();
                var finalCutSceneComponent = finalCutSceneEntity.CreateComponent<FinalCutSceneComponent>();
                finalCutSceneComponent.GameMode = gameMode;
            }
        }
    }
}