using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class EditPackagesViewModel : aSubjectsListingViewModel
    {
        protected override void Refresh()
        {
            if (IsActiveSelected)
                Selected = new ObservableCollection<Package>(
                    ContextManager.LocalPackages.Where(p => p.Active));
            else
                Selected = new ObservableCollection<Package>(
                    ContextManager.LocalPackages.Where(p => p.Active == false));
        }

        #region Binding Properties

        private ObservableCollection<Package> _selected;
        public ObservableCollection<Package> Selected
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
            Selected = new ObservableCollection<Package>(
                Selected.Where(p => p.Name.Contains(FilterName)));
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as Package;
            _context.Packages.Remove(selected);
            SaveContext();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            Package selected = _context.Packages.Find(((Package)argSelected).Id);
            selected.Active = selected.Active ? false : true;
            SaveContext();
        }

        protected override void cmdSelectActive_Execute()
        {
            var query = _context.Packages.Local.Where(p => p.Active);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));
            Selected = new ObservableCollection<Package>(query);
            base.cmdSelectActive_Execute();
        }

        protected override void cmdSelectInactive_Execute()
        {
            var query = _context.Packages.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));
            Selected = new ObservableCollection<Package>(query);
            base.cmdSelectInactive_Execute();
        }
        #endregion
    }
}
