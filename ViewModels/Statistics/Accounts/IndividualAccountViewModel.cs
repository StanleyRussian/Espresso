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
    public class IndividualAccountViewModel : aProcessListingViewModel
    {
        public IndividualAccountViewModel(Account account)
        {
            Account = account;
            Header = account.Name;
        }

        public Account Account { get; }
        public double Balance { get; private set; }

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

        protected override void Load()
        {
            _context = ContextManager.Context;
            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);

            Balance = _context.dAccountsBalances.Find(Account.Id).Balance;
            Transactions = new ObservableCollection<dTransaction>(_context.dTransactions
                .Where(p => p.Account.Id == Account.Id)
                .OrderBy(p=>p.Date)
                .Take(5));
        }

        protected override void Refresh()
        {
            Transactions = new ObservableCollection<dTransaction>(
                _context.dTransactions.Where(p => p.Account.Id == Account.Id
                && p.Date >= _filterFrom && p.Date <= _filterTo));
        }

        protected override void cmdDelete_Execute(object argSelected)
        { }
    }
}
