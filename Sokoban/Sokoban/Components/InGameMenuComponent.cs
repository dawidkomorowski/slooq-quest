using System;
using System.Collections.Generic;
using System.Linq;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using Sokoban.Core.Components;
using Sokoban.Core.Util;

namespace Sokoban.Components
{
    internal sealed class InGameMenuComponent : BehaviorComponent
    {
        private InputComponent _inputComponent = null!;
        private readonly List<InGameMenuOptionComponent> _menuOptions = new List<InGameMenuOptionComponent>();

        public InGameMenuComponent(Entity entity) : base(entity)
        {
        }

        public bool IsVisible { get; set; }
        public int CurrentIndex { get; set; }

        public override void OnStart()
        {
            _inputComponent = Entity.GetComponent<InputComponent>();

            _inputComponent.BindAction("ToggleMenu", ToggleMenu);
            _inputComponent.BindAction("OptionUp", OptionUp);
            _inputComponent.BindAction("OptionDown", OptionDown);
            _inputComponent.BindAction("SelectOption", SelectOption);

            HierarchyVisibility.SetVisibility(Entity, IsVisible);

            _menuOptions.AddRange(Entity.GetChildrenRecursively().Where(e => e.HasComponent<InGameMenuOptionComponent>())
                .Select(e => e.GetComponent<InGameMenuOptionComponent>()));
        }

        private void ToggleMenu()
        {
            IsVisible = !IsVisible;
            HierarchyVisibility.SetVisibility(Entity, IsVisible);
            FindPlayerControllerComponent().Enabled = !IsVisible;

            CurrentIndex = 0;
            SelectOptionAtIndex(CurrentIndex);
        }

        private void OptionUp()
        {
            CurrentIndex = Math.Abs((CurrentIndex - 1) % _menuOptions.Count);
            SelectOptionAtIndex(CurrentIndex);
        }

        private void OptionDown()
        {
            CurrentIndex = Math.Abs((CurrentIndex + 1) % _menuOptions.Count);
            SelectOptionAtIndex(CurrentIndex);
        }

        private void SelectOption()
        {
            var mo = _menuOptions.Single(mo => mo.IsSelected);
            mo.Action?.Invoke();
            ToggleMenu();
        }

        private void SelectOptionAtIndex(int index)
        {
            foreach (var menuOption in _menuOptions)
            {
                menuOption.IsSelected = menuOption.Index == index;
            }
        }

        private PlayerControllerComponent FindPlayerControllerComponent()
        {
            return Entity.Scene.AllEntities.Single(e => e.HasComponent<PlayerControllerComponent>())
                .GetComponent<PlayerControllerComponent>();
        }
    }

    internal sealed class InGameMenuComponentFactory : ComponentFactory<InGameMenuComponent>
    {
        protected override InGameMenuComponent CreateComponent(Entity entity) => new InGameMenuComponent(entity);
    }
}