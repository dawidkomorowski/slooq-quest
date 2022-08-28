using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Input.Mapping;
using Sokoban.VisualEffects;
using System;
using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;
using Sokoban.Core.SceneLoading;

namespace Sokoban.Credits
{
    internal sealed class CreditsEntityFactory
    {
        public void CreateExitCreditsEntity(Scene scene)
        {
            var entity = scene.CreateEntity();

            var inputComponent = entity.CreateComponent<InputComponent>();
            inputComponent.InputMapping = new InputMapping
            {
                ActionMappings =
                {
                    new ActionMapping
                    {
                        ActionName = "Exit",
                        HardwareActions = { new HardwareAction { HardwareInputVariant = HardwareInputVariant.CreateKeyboardVariant(Key.Escape) } }
                    }
                }
            };

            inputComponent.BindAction("Exit", () =>
            {
                inputComponent.InputMapping = null;

                var fadeInOutEntity = scene.CreateEntity();
                var fadeInOutComponent = fadeInOutEntity.CreateComponent<FadeInOutComponent>();
                fadeInOutComponent.Duration = TimeSpan.FromSeconds(1);
                fadeInOutComponent.Mode = FadeInOutComponent.FadeMode.FadeOut;
                fadeInOutComponent.Action = () =>
                {
                    var e = scene.CreateEntity();
                    var loadSceneComponent = e.CreateComponent<LoadSceneComponent>();
                    loadSceneComponent.SceneBehaviorName = "MainMenu";
                };
            });
        }

        public void CreateCreditsText(Scene scene, double x, double y, double fontSize, string text)
        {
            var entity = scene.CreateEntity();
            var transform2DComponent = entity.CreateComponent<Transform2DComponent>();
            transform2DComponent.Translation = new Vector2(x, y);

            var textRendererComponent = entity.CreateComponent<TextRendererComponent>();
            textRendererComponent.FontSize = FontSize.FromDips(fontSize);
            textRendererComponent.Color = Color.FromArgb(255, 255, 255, 255);
            textRendererComponent.SortingLayerName = "UI";
            textRendererComponent.Text = text;

            entity.CreateComponent<CreditsTextComponent>();
        }
    }
}