using System.Reflection;
using Geisha.Engine;
using Sokoban.Core;

namespace Sokoban.Editor
{
    internal sealed class SokobanEditor : IGame
    {
        private static string EngineInformation =>
            $"Geisha Engine {Assembly.GetAssembly(typeof(IGame))?.GetName().Version?.ToString(3)}";

        public string WindowTitle => $"Sokoban Editor {Assembly.GetAssembly(typeof(SokobanEditor))?.GetName().Version?.ToString(2)} ({EngineInformation})";

        public void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            SokobanCoreModule.RegisterComponents(componentsRegistry);

            componentsRegistry.RegisterSceneBehaviorFactory<MainSceneBehaviorFactory>();
        }
    }
}