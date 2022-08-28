using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering;
using Geisha.Engine.Rendering.Components;

namespace Sokoban.LevelComplete
{
    internal sealed class LevelCompleteEntityFactory
    {
        public Entity CreateLevelCompleteEntity(Scene scene)
        {
            var entity = scene.CreateEntity();

            entity.CreateComponent<LevelCompleteComponent>();

            entity.CreateComponent<Transform2DComponent>();

            var rectangleRendererComponent = entity.CreateComponent<RectangleRendererComponent>();
            rectangleRendererComponent.Color = Color.FromArgb(0, 0, 0, 0);
            rectangleRendererComponent.FillInterior = true;
            rectangleRendererComponent.Dimension = new Vector2(1280, 720);
            rectangleRendererComponent.SortingLayerName = "UI";
            rectangleRendererComponent.OrderInLayer = 0;


            var textEntity = entity.CreateChildEntity();
            var textTransform = textEntity.CreateComponent<Transform2DComponent>();
            textTransform.Translation = new Vector2(-375, 75);

            var textRendererComponent = textEntity.CreateComponent<TextRendererComponent>();
            textRendererComponent.Color = Color.FromArgb(0, 255, 255, 255);
            textRendererComponent.Text = "Level complete";
            textRendererComponent.FontSize = FontSize.FromDips(100);
            textRendererComponent.SortingLayerName = "UI";
            textRendererComponent.OrderInLayer = 1;

            return entity;
        }
    }
}