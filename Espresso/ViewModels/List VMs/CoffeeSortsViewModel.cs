using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    class CoffeeSortsViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public CoffeeSortsViewModel()
        {
            cmdNewCoffeeSort = new Auxiliary.Command(cmdNewCoffeeSort_Execute);
            cmdSaveChanges = new Auxiliary.Command(cmdSaveChanges_Execute);
            cmdDeleteCoffeeSort = new Auxiliary.Command(cmdDeleteCoffeeSort_Execute);

            cmdToggleActive = new Auxiliary.Command(cmdToggleActive_Execute);
            cmdSelectActive = new Auxiliary.Command(cmdSelectActive_Execute, cmdSelectActive_CanExecute);
            cmdSelectInactive = new Auxiliary.Command(cmdSelectInactive_Execute, cmdSelectInactive_CanExecute);
            cmdSearch = new Auxiliary.Command(cmdSearch_Execute);
            cmdCleanSearch = new Auxiliary.Command(cmdCleanSearch_Execute);

            _context = new Entity.ContextContainer();
            _context.CoffeeSorts.Load();
            Refresh();
        }

        private void Refresh()
        {
            CoffeeSortsSelected = new ObservableCollection<Entity.CoffeeSort>(
                _context.CoffeeSorts.Local.Where(p => p.Active == true));
            activeIsSelected = true;

            cmdSelectActive.CanExecute(null);
            cmdSelectInactive.CanExecute(null);
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Entity.CoffeeSort> _accountsSelected;
        public ObservableCollection<Entity.CoffeeSort> CoffeeSortsSelected
        {
            get { return _accountsSelected; }
            set
            {
                _accountsSelected = value;
                OnPropertyChanged("CoffeeSortsSelected");
            }
        }

        private string _filterName;
        public string FilterName
        {
            get { return _filterName; }
            set
            {
                _filterName = value;
                OnPropertyChanged("FilterName");
            }
        }

        #endregion

        #region Commands

        public ICommand cmdSearch
        { get; private set; }

        private void cmdSearch_Execute()
        {
            CoffeeSortsSelected = new ObservableCollection<Entity.CoffeeSort>(
                CoffeeSortsSelected.Where(p => p.Name.Contains(FilterName) == true));
        }

        public ICommand cmdCleanSearch
        { get; private set; }

        private void cmdCleanSearch_Execute()
        {
            FilterName = null;
            cmdSelectActive_Execute();
        }

        public ICommand cmdSaveChanges
        { get; private set; }

        private void cmdSaveChanges_Execute()
        {
            _context.SaveChanges();
        }

        public ICommand cmdNewCoffeeSort
        { get; private set; }

        private void cmdNewCoffeeSort_Execute()
        {
            new Views.NewCoffeeSort().ShowDialog();
            Refresh();
        }

        public ICommand cmdDeleteCoffeeSort
        { get; private set; }

        private void cmdDeleteCoffeeSort_Execute(object argSelected)
        {
            if (argSelected == null)
            {
                MessageBox.Show("Вы не выбрали счёт!");
                return;
            }

            var selected = argSelected as Entity.CoffeeSort;
            try
            {
                _context.CoffeeSorts.Remove(selected);
                _context.SaveChanges();
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Имеются связанные закупки, невозможно удалить этот счёт");
            }
        }

        public ICommand cmdToggleActive
        { get; private set; }

        private void cmdToggleActive_Execute(object argSelected)
        {
            Entity.CoffeeSort selected = _context.CoffeeSorts.Find(((Entity.CoffeeSort)argSelected).Id);
            selected.Active = (selected.Active == true) ? false : true;
            _context.SaveChanges();
            Refresh();
        }

        private bool activeIsSelected;

        // Select ACTIVE command
        public ICommand cmdSelectActive
        { get; private set; }

        private void cmdSelectActive_Execute()
        {
            var query = _context.CoffeeSorts.Local.Where(p => p.Active == true);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName) == true);

            CoffeeSortsSelected = new ObservableCollection<Entity.CoffeeSort>(query);
            activeIsSelected = true;

            cmdSelectActive.CanExecute(null);
            cmdSelectInactive.CanExecute(null);
        }

        private bool cmdSelectActive_CanExecute(object arg)
        {
            return !activeIsSelected;
        }

        // Select INACTIVE command
        public ICommand cmdSelectInactive
        { get; private set; }

        private void cmdSelectInactive_Execute()
        {
            var query = _context.CoffeeSorts.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName) == true);

            CoffeeSortsSelected = new ObservableCollection<Entity.CoffeeSort>(query);
            activeIsSelected = false;

            cmdSelectActive.CanExecute(null);
            cmdSelectInactive.CanExecute(null);
        }

        private bool cmdSelectInactive_CanExecute(object arg)
        {
            return activeIsSelected;
        }
        #endregion
    }
}
