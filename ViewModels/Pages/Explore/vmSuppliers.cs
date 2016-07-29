using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmSuppliers: aSubjectsListViewModel
    {
        private ObservableCollection<Supplier> _active;
        private ObservableCollection<Supplier> _inactive;

        public vmSuppliers()
        {
            Header = "Поставщики";
        }

        protected override void Load()
        {
            if (IsActiveSelected)
            {
                _active = new ObservableCollection<Supplier>(
                    ContextManager.Context.Suppliers.Where(p => p.Active));
                Selected = _active;
            }
            else
            {
                _inactive = new ObservableCollection<Supplier>(
                    ContextManager.Context.Suppliers.Where(p => !p.Active));
                Selected = _inactive;
            }

        }

        #region Binding Properties

        private ObservableCollection<Supplier> _selected;
        public ObservableCollection<Supplier> Selected
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
            ContextManager.ReloadContext();
            Load();
        }

        protected override void cmdSearch_Execute()
        { }

        protected override void cmdDelete_Execute(object argSelected)
        { }

        protected override void cmdToggleActive_Execute(object argSelected)
        {
            var selected = argSelected as Supplier;
            selected.Active = !selected.Active;
            SaveContext();
            cmdReload_Execute();
        }

    }
}
