using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;
using ViewModels.Statistics.Abstract;

namespace ViewModels.Statistics.Accounts
{
    public class IndividualAccountViewModel : aTabViewModel
    {
        private ContextContainer _context = ContextManager.Context;

        public IndividualAccountViewModel(Account account)
        {
            Account = account;
            Header = account.Name;
        }

        public Account Account { get; }
        public double Balance { get; private set; }

        public ObservableCollection<dTransaction> Transactions
        {
            get { return _transactions; }
            private set
            {
                _transactions = value; 
                OnPropertyChanged();
            }
        }

        protected override void Load()
        {
            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);

            cmdSave = new Command(cmdSave_Execute);
            cmdDelete = new Command(cmdDelete_Execute);
            cmdFilter30Days = new Command(cmdFilter30Days_Execute);
            cmdFilterAll = new Command(cmdFilterAll_Execute);

            Balance = _context.dAccountsBalances.Find(Account.Id).Balance;
            Transactions = new ObservableCollection<dTransaction>(_context.dTransactions
                .Where(p => p.Account.Id == Account.Id)
                .OrderBy(p=>p.Date)
                .Take(5));
        }

        private void ReloadTransactions()
        {
            Transactions = new ObservableCollection<dTransaction>(
                _context.dTransactions.Where(p => p.Account.Id == Account.Id
                && p.Date >= _filterFrom && p.Date <= _filterTo));
        }

        #region Binding Properties

        protected DateTime _filterFrom;
        public DateTime FilterFrom
        {
            get { return _filterFrom; }
            set
            {
                _filterFrom = value;
                OnPropertyChanged();
                ReloadTransactions();
            }
        }

        protected DateTime _filterTo;
        private ObservableCollection<dTransaction> _transactions;

        public DateTime FilterTo
        {
            get { return _filterTo; }
            set
            {
                _filterTo = value;
                OnPropertyChanged();
                ReloadTransactions();
            }
        }

        #endregion

        #region Commands

        public ICommand cmdSave
        { get; private set; }
        protected void cmdSave_Execute()
        {
            _context.SaveChanges();
            DialogCoordinator.Instance.ShowMessageAsync(this, "Успех", "Сохранение завершено");
        }

        public ICommand cmdDelete
        { get; private set; }
        protected void cmdDelete_Execute(object argSelected) { }

        public ICommand cmdFilter30Days
        { get; private set; }
        protected void cmdFilter30Days_Execute()
        {
            _filterTo = DateTime.Now;
            OnPropertyChanged(nameof(FilterTo));
            FilterFrom = DateTime.Now.AddDays(-30);
        }

        public ICommand cmdFilterAll
        { get; private set; }
        protected void cmdFilterAll_Execute()
        {
            _filterTo = DateTime.Now;
            OnPropertyChanged(nameof(FilterTo));
            FilterFrom = DateTime.MinValue;
        }

        #endregion
    }
}
