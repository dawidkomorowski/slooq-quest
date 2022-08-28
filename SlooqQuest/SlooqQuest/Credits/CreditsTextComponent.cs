using System;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using SlooqQuest.Core.SceneLoading;
using SlooqQuest.VisualEffects;

namespace SlooqQuest.Credits
{
    internal sealed class CreditsTextComponent : BehaviorComponent
    {
        private const double ScrollingSpeed = 75;
        private Transform2DComponent _transform = null!;

        public CreditsTextComponent(Entity entity) : base(entity)
        {
        }

        public bool IsLast { get; set; }

        public override void OnStart()
        {
            _transform = Entity.GetComponent<Transform2DComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _transform.Translation += new Vector2(0, ScrollingSpeed) * gameTime.DeltaTime.TotalSeconds;

            if (IsLast)
            {
                if (_transform.Translation.Y > 500)
                {
                    GoBackToMainMenu();
                    Entity.RemoveAfterFullFrame();
                }
            }
        }

        private void GoBackToMainMenu()
        {
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
        }
    }

    internal sealed class CreditsTextComponentFactory : ComponentFactory<CreditsTextComponent>
    {
        protected override CreditsTextComponent CreateComponent(Entity entity) => new CreditsTextComponent(entity);
    }
}