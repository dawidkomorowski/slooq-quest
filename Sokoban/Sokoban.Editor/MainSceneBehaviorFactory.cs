using System.Windows.Forms;
using Geisha.Engine.Core.SceneModel;
using Sokoban.Core;
using Sokoban.Editor.ToggleMode;
using Sokoban.Editor.UserInterface;

namespace Sokoban.Editor
{
    internal sealed class MainSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "Main";
        private readonly CoreEntityFactory _coreEntityFactory;
        private readonly UserInterfaceEntityFactory _userInterfaceEntityFactory;
        private readonly ToggleModeEntityFactory _toggleModeEntityFactory;

        public MainSceneBehaviorFactory(CoreEntityFactory coreEntityFactory, UserInterfaceEntityFactory userInterfaceEntityFactory,
            ToggleModeEntityFactory toggleModeEntityFactory)
        {
            _coreEntityFactory = coreEntityFactory;
            _userInterfaceEntityFactory = userInterfaceEntityFactory;
            _toggleModeEntityFactory = toggleModeEntityFactory;
        }

        public string BehaviorName => SceneBehaviorName;

        public SceneBehavior Create(Scene scene) =>
            new MainSceneBehavior(scene, _coreEntityFactory, _userInterfaceEntityFactory, _toggleModeEntityFactory);

        private sealed class MainSceneBehavior : SceneBehavior
        {
            private readonly CoreEntityFactory _coreEntityFactory;
            private readonly UserInterfaceEntityFactory _userInterfaceEntityFactory;
            private readonly ToggleModeEntityFactory _toggleModeEntityFactory;

            public MainSceneBehavior(Scene scene, CoreEntityFactory coreEntityFactory,
                UserInterfaceEntityFactory userInterfaceEntityFactory, ToggleModeEntityFactory toggleModeEntityFactory) : base(scene)
            {
                _coreEntityFactory = coreEntityFactory;
                _userInterfaceEntityFactory = userInterfaceEntityFactory;
                _toggleModeEntityFactory = toggleModeEntityFactory;
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                Application.OpenForms[0].KeyDown += OnKeyDown;

                var cameraEntity = _coreEntityFactory.CreateCamera(Scene);

                var background = _coreEntityFactory.CreateBackground(Scene);
                background.Parent = cameraEntity;

                var help = _userInterfaceEntityFactory.CreateHelp(Scene);
                help.Parent = cameraEntity;

                _toggleModeEntityFactory.CreateToggleModeEntity(Scene);
                _toggleModeEntityFactory.CreateEnterModeEntity(Scene);
            }
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F10)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}