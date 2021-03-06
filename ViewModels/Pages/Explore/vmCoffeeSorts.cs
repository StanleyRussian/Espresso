﻿using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmCoffeeSorts: aSubjectsListViewModel
    {
        private ObservableCollection<CoffeeSort> _active;
        private ObservableCollection<CoffeeSort> _inactive;

        public vmCoffeeSorts()
        {
            Header = "Сорта кофе";
        }

        protected override void Load()
        {
            if (IsActiveSelected)
            {
                _active = new ObservableCollection<CoffeeSort>(
                    ContextManager.Context.CoffeeSorts.Where(p => p.Active));
                Selected = _active;
            }
            else
            {
                _inactive = new ObservableCollection<CoffeeSort>(
                    ContextManager.Context.CoffeeSorts.Where(p => !p.Active));
                Selected = _inactive;
            }
        }

        #region Binding Properties

        private ObservableCollection<CoffeeSort> _selected;
        public ObservableCollection<CoffeeSort> Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        #endregion

        protected override void cmdSearch_Execute()
        { }

        protected override void cmdDelete_Execute(object argSelected)
        { }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            var selected = argSelected as CoffeeSort;
            selected.Active = !selected.Active;
            SaveContext();
            cmdReload_Execute();
        }
    }
}
