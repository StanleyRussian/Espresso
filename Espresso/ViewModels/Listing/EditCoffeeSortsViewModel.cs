﻿using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class EditCoffeeSortsViewModel : aSubjectsListingViewModel
    {
        protected override void Refresh()
        {
            if (IsActiveSelected)
                Selected = new ObservableCollection<CoffeeSort>(
                    ContextManager.LocalCoffeeSorts.Where(p => p.Active));
            else
                Selected = new ObservableCollection<CoffeeSort>(
                    ContextManager.LocalCoffeeSorts.Where(p => p.Active == false));
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

        #region Commands

        protected override void cmdSearch_Execute()
        {
            Selected = new ObservableCollection<CoffeeSort>(
                Selected.Where(p => p.Name.Contains(FilterName)));
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as CoffeeSort;
            _context.CoffeeSorts.Remove(selected);
            SaveContext();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            CoffeeSort selected = (CoffeeSort)argSelected;
            selected.Active = !selected.Active;
            SaveContext();
        }

        protected override void cmdSelectActive_Execute()
        {
            var query = _context.CoffeeSorts.Local.Where(p => p.Active);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));
            Selected = new ObservableCollection<CoffeeSort>(query);

            base.cmdSelectActive_Execute();
        }

        protected override void cmdSelectInactive_Execute()
        {
            var query = _context.CoffeeSorts.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));
            Selected = new ObservableCollection<CoffeeSort>(query);

            base.cmdSelectActive_Execute();
        }
        #endregion
    }
}