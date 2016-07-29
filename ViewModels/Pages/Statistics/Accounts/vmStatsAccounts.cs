using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows.Input;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;
using System.Linq;

namespace ViewModels.Pages.Statistics.Accounts
{
    public class vmStatsAccounts: aTabViewModel
    {
        public vmStatsAccounts()
        {
            Header = "Cчета";
        }

        protected override void Load()
        {
            cmdFilter30Days = new Command(cmdFilter30Days_Execute);
            cmdFilterAll = new Command(cmdFilterAll_Execute);

            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);

            Balances = new ObservableCollection<dAccountsBalance>(
                ContextManager.Context.dAccountsBalances.Where(p => p.Account.Active).Include(p => p.Account));
            Refresh();
        }

        private void Refresh()
        {
            var query = ContextManager.Context.dTransactions.Where(p => p.Date >= FilterFrom && p.Date <= FilterTo);
            if (FilterAccount != null)
                query = query.Where(p => p.Account.Id == FilterAccount.Id);
            Transactions = new ObservableCollection<dTransaction>(query);
        }

        #region Binding Properties

        private DateTime _filterFrom;
        public DateTime FilterFrom
        {
            get { return _filterFrom; }
            set
            {
                _filterFrom = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private DateTime _filterTo;
        public DateTime FilterTo
        {
            get { return _filterTo; }
            set
            {
                _filterTo = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private Account _filterAccount;
        public Account FilterAccount
        {
            get { return _filterAccount; }
            set
            {
                _filterAccount = value;
                OnPropertyChanged();
                Refresh();
            }
        }


        public ObservableCollection<dAccountsBalance> Balances { get; private set; }

        private ObservableCollection<dTransaction> _transactions;
        public ObservableCollection<dTransaction> Transactions
        {
            get { return _transactions; }
            private set
            {
                _transactions = value;
                OnPropertyChanged();
            }
        }

        #endregion

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


    }
}
