using System.Collections.Generic;
using System.Linq;

namespace Sokoban.MainMenu
{
    internal sealed class MainMenuModel
    {
        private readonly List<MainMenuOption> _options = new List<MainMenuOption>();

        public MainMenuModel()
        {
            _options.Add(new MainMenuOption { Index = 0, Text = "Continue", IsSelected = true, IsVisible = true });
            _options.Add(new MainMenuOption { Index = 1, Text = "New Game", IsSelected = false, IsVisible = true });
            _options.Add(new MainMenuOption { Index = 2, Text = "Credits", IsSelected = false, IsVisible = true });
            _options.Add(new MainMenuOption { Index = 3, Text = "Exit", IsSelected = false, IsVisible = true });
        }

        public IReadOnlyList<MainMenuOption> Options => _options.AsReadOnly();

        public void OptionUp()
        {
            var selectedOption = GetSelectedOption();
            selectedOption.IsSelected = false;

            var optionToSelect = GetOption((selectedOption.Index - 1) % _options.Count);
            optionToSelect.IsSelected = true;
        }

        public void OptionDown()
        {
            var selectedOption = GetSelectedOption();
            selectedOption.IsSelected = false;

            var optionToSelect = GetOption((selectedOption.Index + 1) % _options.Count);
            optionToSelect.IsSelected = true;
        }

        private MainMenuOption GetSelectedOption()
        {
            return _options.Single(o => o.IsSelected);
        }

        private MainMenuOption GetOption(int index)
        {
            return _options.Single(o => o.Index == index);
        }
    }

    internal sealed class MainMenuOption
    {
        public int Index { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsVisible { get; set; }
        public bool IsSelected { get; set; }
    }
}