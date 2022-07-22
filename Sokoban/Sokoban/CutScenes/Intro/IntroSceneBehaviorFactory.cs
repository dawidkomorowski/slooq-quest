using System;
using System.IO;
using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;
using Sokoban.Core.GameLogic;
using Sokoban.Core.LevelModel;
using Sokoban.VisualEffects;

namespace Sokoban.CutScenes.Intro
{
    internal sealed class IntroSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "IntroCutScene";

        private readonly CoreEntityFactory _coreEntityFactory;


        public IntroSceneBehaviorFactory(CoreEntityFactory coreEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
        }

        public string BehaviorName => SceneBehaviorName;
        public SceneBehavior Create(Scene scene) => new IntroSceneBehavior(scene, _coreEntityFactory);

        private sealed class IntroSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;

            public IntroSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory) : base(scene)
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
                fadeInOutComponent.Duration = TimeSpan.FromMilliseconds(250);

                var serializedLevel = File.ReadAllText(Path.Combine("CutScenes", "Intro.sokoban-level"));
                var level = Level.Deserialize(serializedLevel);
                var gameMode = new GameMode(level);

                var levelEntity = _coreEntityFactory.CreateLevel(Scene, gameMode.Level);
                levelEntity.Parent = cameraEntity;

                var introCutSceneEntity = Scene.CreateEntity();
                var introCutSceneComponent = introCutSceneEntity.CreateComponent<IntroCutSceneComponent>();
                introCutSceneComponent.GameMode = gameMode;
            }
        }
    }
}