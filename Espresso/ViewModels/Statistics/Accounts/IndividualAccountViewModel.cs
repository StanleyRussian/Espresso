using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Core.Annotations;
using Core.Entity;
using Core.ViewModels.Statistics.Abstract;

namespace Core.ViewModels.Statistics.Accounts
{
    public class IndividualAccountViewModel : aTabViewModel
    {
        public IndividualAccountViewModel(Account account)
        {
            Account = account;
            Header = Account.Name;
        }

        public Account Account { get; }
        public string Header { get; }
        public double Balance { get; private set; }
        public ObservableCollection<dTransaction> Transactions { get; private set; }

        protected override void Load()
        {
            Balance = ContextManager.Context.dAccountsBalances.Local.First(x => x.Account.Id == Account.Id).Balance;
            Transactions = new ObservableCollection<Entity.dTransaction>(
                ContextManager.Context.dTransactions.Where(p => p.Account.Id == Account.Id));
        }
    }
}
