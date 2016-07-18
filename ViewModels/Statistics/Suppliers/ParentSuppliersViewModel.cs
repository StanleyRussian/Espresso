using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Statistics.Abstract;
using ViewModels.Statistics.Accounts;

namespace ViewModels.Statistics.Suppliers
{
    public class ParentSuppliersViewModel: aSubjectsListViewModel
    {
        private ObservableCollection<IndividualSupplierViewModel> _active;
        private ObservableCollection<IndividualSupplierViewModel> _inactive;

        public ParentSuppliersViewModel()
        {
            Header = "Поставщики";
        }

        protected override void Load()
        {
            if (IsActiveSelected)
            {
                if (_active == null)
                {
                    _active = new ObservableCollection<IndividualSupplierViewModel>();
                    foreach (var active in _context.Suppliers.Where(p => p.Active))
                        _active.Add(new IndividualSupplierViewModel(active));
                }
                Selected = _active;
            }
            else
            {
                if (_inactive == null)
                {
                    _inactive = new ObservableCollection<IndividualSupplierViewModel>();
                    foreach (var inactive in _context.Suppliers.Where(p => !p.Active))
                        _inactive.Add(new IndividualSupplierViewModel(inactive));
                }
                Selected = _inactive;
            }

        }

        #region Binding Properties

        private ObservableCollection<IndividualSupplierViewModel> _selected;
        public ObservableCollection<IndividualSupplierViewModel> Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        #endregion

        protected override void cmdReload_Execute()
        {
            _active = null;
            _inactive = null;
            Load();
        }

        protected override void cmdSearch_Execute()
        { }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as IndividualSupplierViewModel;
            _context.Suppliers.Remove(selected.Supplier);
            SaveContext();
        }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            var selected = argSelected as IndividualSupplierViewModel;
            selected.Supplier.Active = !selected.Supplier.Active;
            SaveContext();
            cmdReload_Execute();
        }

    }
}
