using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class SuppliersViewModel : aSubjectsListingViewModel
    {
        protected override void Refresh()
        {
            if (IsActiveSelected)
                Selected = new ObservableCollection<Supplier>(
                    ContextManager.LocalSuppliers.Where(p => p.Active));
            else
                Selected = new ObservableCollection<Supplier>(
                    ContextManager.LocalSuppliers.Where(p => p.Active == false));
        }

        #region Binding Properties

        private ObservableCollection<Supplier> _selected;
        public ObservableCollection<Supplier> Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }

        #endregion

        #region Commands

        protected override void cmdSearch_Execute()
        {
            Selected = new ObservableCollection<Supplier>(
                Selected.Where(p => p.Name.Contains(FilterName)));
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as Supplier;
            _context.Suppliers.Remove(selected);
            SaveContext();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            Supplier selected = _context.Suppliers.Find(((Supplier)argSelected).Id);
            selected.Active = !selected.Active;
            SaveContext();
        }

        protected override void cmdSelectActive_Execute()
        {
            var query = _context.Suppliers.Local.Where(p => p.Active);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));
            Selected = new ObservableCollection<Supplier>(query);
            base.cmdSelectActive_Execute();
        }

        protected override void cmdSelectInactive_Execute()
        {
            var query = _context.Suppliers.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName));
            Selected = new ObservableCollection<Supplier>(query);
            base.cmdSelectInactive_Execute();
        }

        #endregion

    }
}
