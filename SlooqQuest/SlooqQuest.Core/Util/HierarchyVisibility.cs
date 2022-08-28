using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;

namespace SlooqQuest.Core.Util
{
    public static class HierarchyVisibility
    {
        public static void SetVisibility(Entity rootEntity, bool visible)
        {
            if (rootEntity.HasComponent<Renderer2DComponent>())
            {
                rootEntity.GetComponent<Renderer2DComponent>().Visible = visible;
            }

            foreach (var childEntity in rootEntity.Children)
            {
                SetVisibility(childEntity, visible);
            }
        }
    }
}