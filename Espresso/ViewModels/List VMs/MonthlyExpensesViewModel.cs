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
    class MonthlyExpensesViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public MonthlyExpensesViewModel()
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
            _context.MonthlyExpenses.Load();
            Refresh();
        }

        private void Refresh()
        {
            MonthlyExpensesSelected = new ObservableCollection<Entity.MonthlyExpense>(
                _context.MonthlyExpenses.Local.Where(p => p.Active == true));
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

        private ObservableCollection<Entity.MonthlyExpense> _packagesSelected;
        public ObservableCollection<Entity.MonthlyExpense> MonthlyExpensesSelected
        {
            get { return _packagesSelected; }
            set
            {
                _packagesSelected = value;
                OnPropertyChanged("MonthlyExpensesSelected");
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
            MonthlyExpensesSelected = new ObservableCollection<Entity.MonthlyExpense>(
                MonthlyExpensesSelected.Where(p => p.Designation.Contains(FilterName) == true));
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
            new Views.NewMonthlyExpense().ShowDialog();
            _context.MonthlyExpenses.Load();
            Refresh();
        }

        public ICommand cmdDelete
        { get; private set; }

        private void cmdDelete_Execute(object argSelected)
        {
            if (argSelected == null)
            {
                MessageBox.Show("Вы не выбрали расход!");
                return;
            }

            var selected = argSelected as Entity.MonthlyExpense;
            try
            {
                _context.MonthlyExpenses.Remove(selected);
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
            Entity.MonthlyExpense selected = _context.MonthlyExpenses.Find(((Entity.MonthlyExpense)argSelected).Id);
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
            var query = _context.MonthlyExpenses.Local.Where(p => p.Active == true);
            if (FilterName != null)
                query = query.Where(p => p.Designation.Contains(FilterName) == true);

            MonthlyExpensesSelected = new ObservableCollection<Entity.MonthlyExpense>(query);
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
            var query = _context.MonthlyExpenses.Local.Where(p => p.Active == false);
            if (FilterName != null)
                query = query.Where(p => p.Designation.Contains(FilterName) == true);

            MonthlyExpensesSelected = new ObservableCollection<Entity.MonthlyExpense>(query);
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
