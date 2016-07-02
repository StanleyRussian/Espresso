﻿using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class EditPackedCategoriesViewModel: aSubjectsListingViewModel
    {
        protected override void Refresh()
        {
            if (IsActiveSelected)
                Selected = new ObservableCollection<PackedCategory>(
                    ContextManager.LocalPackedCategories.Where(p => p.Active));
            else
                Selected = new ObservableCollection<PackedCategory>(
                    ContextManager.LocalPackedCategories.Where(p => p.Active == false));
        }

        #region Binding Properties 

        private ObservableCollection<PackedCategory> _selected;
        public ObservableCollection<PackedCategory> Selected
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
            Selected = new ObservableCollection<PackedCategory>(
                Selected.Where(p => p.Name.Contains(FilterName)));
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as PackedCategory;
            _context.PackedCategories.Remove(selected);
            SaveContext();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            PackedCategory selected = argSelected as PackedCategory;
            selected.Active = !selected.Active;
            SaveContext();
        }

        protected override void cmdSelectActive_Execute()
        {
            var query = _context.PackedCategories.Local.Where(p => p.Active);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));

            Selected = new ObservableCollection<PackedCategory>(query);
            base.cmdSelectActive_Execute();
        }

        protected override void cmdSelectInactive_Execute()
        {
            var query = _context.PackedCategories.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));

            Selected = new ObservableCollection<PackedCategory>(query);
            base.cmdSelectInactive_Execute();
        }

        #endregion
    }
}