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
    class PackagesViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public PackagesViewModel()
        {
            cmdNew = new Auxiliary.Command(cmdNew_Execute);
            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            cmdDelete = new Auxiliary.Command(cmdDelete_Execute);

            cmdToggleActive = new Auxiliary.Command(cmdToggleActive_Execute);
            cmdSelectActive = new Auxiliary.Command(cmdSelectActive_Execute, cmdSelectActive_CanExecute);
            cmdSelectInactive = new Auxiliary.Command(cmdSelectInactive_Execute, cmdSelectInactive_CanExecute);
            cmdSearch = new Auxiliary.Command(cmdSearch_Execute);
            cmdClearSearch = new Auxiliary.Command(cmdClearSearch_Execute);

            _context = new Entity.ContextContainer();
            _context.Packages.Load();
            Refresh();
        }

        private void Refresh()
        {
            PackagesSelected = new ObservableCollection<Entity.Package>(
                _context.Packages.Local.Where(p => p.Active == true));
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

        private ObservableCollection<Entity.Package> _packagesSelected;
        public ObservableCollection<Entity.Package> PackagesSelected
        {
            get { return _packagesSelected; }
            set
            {
                _packagesSelected = value;
                OnPropertyChanged("PackagesSelected");
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
            PackagesSelected = new ObservableCollection<Entity.Package>(
                PackagesSelected.Where(p => p.Name.Contains(FilterName) == true));
        }

        public ICommand cmdClearSearch
        { get; private set; }

        private void cmdClearSearch_Execute()
        {
            FilterName = null;
            cmdSelectActive_Execute();
        }

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.SaveChanges();
        }

        public ICommand cmdNew
        { get; private set; }

        private void cmdNew_Execute()
        {
            new Views.NewPackage().ShowDialog();
            _context.Packages.Load();
            Refresh();
        }

        public ICommand cmdDelete
        { get; private set; }

        private void cmdDelete_Execute(object argSelected)
        {
            if (argSelected == null)
            {
                MessageBox.Show("Вы не выбрали упаковку!");
                return;
            }

            var selected = argSelected as Entity.Package;
            try
            {
                _context.Packages.Remove(selected);
                _context.SaveChanges();
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ICommand cmdToggleActive
        { get; private set; }

        private void cmdToggleActive_Execute(object argSelected)
        {
            Entity.Package selected = _context.Packages.Find(((Entity.Package)argSelected).Id);
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
            var query = _context.Packages.Local.Where(p => p.Active == true);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName) == true);

            PackagesSelected = new ObservableCollection<Entity.Package>(query);
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
            var query = _context.Packages.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName) == true);

            PackagesSelected = new ObservableCollection<Entity.Package>(query);
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
