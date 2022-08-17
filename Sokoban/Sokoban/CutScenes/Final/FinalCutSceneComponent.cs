using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Sokoban.Core.GameLogic;
using Sokoban.VisualEffects;
using System;
using Sokoban.Core.SceneLoading;

namespace Sokoban.CutScenes.Final
{
    internal sealed class FinalCutSceneComponent : BehaviorComponent
    {
        private int _stage = 0;
        private Wait _wait = new Wait(TimeSpan.Zero);

        public FinalCutSceneComponent(Entity entity) : base(entity)
        {
        }

        public GameMode? GameMode { get; set; }

        public override void OnUpdate(GameTime gameTime)
        {
            if (GameMode is null)
            {
                return;
            }

            switch (_stage)
            {
                case 0:
                    _wait = new Wait(TimeSpan.FromSeconds(1));
                    _stage = 1;
                    break;
                case 1:
                    if (_wait.Update(gameTime.DeltaTime))
                    {
                        _stage = 2;
                    }

                    break;
                case 2:
                    _stage = 999;
                    break;
                case 999:
                    var fadeInOutEntity = Scene.CreateEntity();
                    var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                    fadeInOutComponent.Duration = TimeSpan.FromSeconds(1);
                    fadeInOutComponent.Mode = FadeInOutComponent.FadeMode.FadeOut;
                    fadeInOutComponent.Action = () =>
                    {
                        var e = Scene.CreateEntity();
                        var loadSceneComponent = e.CreateComponent<LoadSceneComponent>();
                        loadSceneComponent.SceneBehaviorName = "MainMenu";
                    };
                    _stage = 1000;
                    break;
                case 1000:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_stage), _stage, "Invalid stage value.");
            }
        }
    }

    internal sealed class FinalCutSceneComponentFactory : ComponentFactory<FinalCutSceneComponent>
    {
        protected override FinalCutSceneComponent CreateComponent(Entity entity) => new FinalCutSceneComponent(entity);
    }
}