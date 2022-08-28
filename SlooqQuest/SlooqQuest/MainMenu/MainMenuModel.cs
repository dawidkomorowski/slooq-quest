using System;
using System.Collections.Generic;
using System.Linq;

namespace SlooqQuest.MainMenu
{
    internal sealed class MainMenuModel
    {
        private readonly List<MainMenuOption> _options = new List<MainMenuOption>();

        public MainMenuModel(IEnumerable<MainMenuOption> options)
        {
            _options.AddRange(options);
        }

        public IReadOnlyList<MainMenuOption> Options => _options.AsReadOnly();

        public void OptionUp()
        {
            var selectedOption = GetSelectedOption();
            selectedOption.IsSelected = false;

            var optionToSelect = GetOption((selectedOption.Index - 1 + _options.Count) % _options.Count);
            optionToSelect.IsSelected = true;
        }

        public void OptionDown()
        {
            var selectedOption = GetSelectedOption();
            selectedOption.IsSelected = false;

            var optionToSelect = GetOption((selectedOption.Index + 1 + _options.Count) % _options.Count);
            optionToSelect.IsSelected = true;
        }

        public void SelectOption()
        {
            var option = GetSelectedOption();
            option.Action?.Invoke();
        }

        public MainMenuOption GetSelectedOption()
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
        public bool IsSelected { get; set; }
        public Action? Action { get; set; }
    }
}