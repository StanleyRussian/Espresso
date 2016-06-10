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
    class AccountsViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public AccountsViewModel()
        {
            cmdNewAccount = new Auxiliary.Command(cmdNewAccount_Execute);
            cmdSaveChanges = new Auxiliary.Command(cmdSaveChanges_Execute);
            cmdDeleteAccount = new Auxiliary.Command(cmdDeleteAccount_Execute);

            cmdToggleActive = new Auxiliary.Command(cmdToggleActive_Execute);
            cmdSelectActive = new Auxiliary.Command(cmdSelectActive_Execute, cmdSelectActive_CanExecute);
            cmdSelectInactive = new Auxiliary.Command(cmdSelectInactive_Execute, cmdSelectInactive_CanExecute);
            cmdSearch = new Auxiliary.Command(cmdSearch_Execute);
            cmdCleanSearch = new Auxiliary.Command(cmdCleanSearch_Execute);

            _context = new Entity.ContextContainer();
            _context.Accounts.Load();
            Refresh();
        }

        private void Refresh()
        {
            AccountsSelected = new ObservableCollection<Entity.Account>(
                _context.Accounts.Local.Where(p => p.Active == true));
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

        private ObservableCollection<Entity.Account> _accountsSelected;
        public ObservableCollection<Entity.Account> AccountsSelected
        {
            get { return _accountsSelected; }
            set
            {
                _accountsSelected = value;
                OnPropertyChanged("AccountsSelected");
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
            AccountsSelected = new ObservableCollection<Entity.Account>(
                AccountsSelected.Where(p => p.Name.Contains(FilterName) == true));
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

        public ICommand cmdNewAccount
        { get; private set; }

        private void cmdNewAccount_Execute()
        {
            new Views.NewAccount().ShowDialog();
            Refresh();
        }

        public ICommand cmdDeleteAccount
        { get; private set; }

        private void cmdDeleteAccount_Execute(object argSelected)
        {
            if (argSelected == null)
            {
                MessageBox.Show("Вы не выбрали счёт!");
                return;
            }

            var selected = argSelected as Entity.Account;
            try
            {
                _context.Accounts.Remove(selected);
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
            Entity.Account selected = _context.Accounts.Find(((Entity.Account)argSelected).Id);
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
            var query = _context.Accounts.Local.Where(p => p.Active == true);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName) == true);

            AccountsSelected = new ObservableCollection<Entity.Account>(query);
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
            var query = _context.Accounts.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Name.Contains(FilterName) == true);

            AccountsSelected = new ObservableCollection<Entity.Account>(query);
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
