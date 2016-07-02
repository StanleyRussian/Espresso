using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Entity;

namespace Core.ViewModels.Statistic
{
    public class ParentAccounts:iTabViewModel
    {
        public ObservableCollection<StatAccount> AccountsTabs { get; private set; }

        public ParentAccounts()
        {
            AccountsTabs = new ObservableCollection<StatAccount>();
            foreach (var activeAccount in ContextManager.ActiveAccounts)
            {
                AccountsTabs.Add(new StatAccount(activeAccount));
            }
        }

        public string Header { get; set; }
    }

    public class StatAccount:iTabViewModel
    {
        private Account _account;
        public StatAccount(Account account)
        {
            //_account = account;
            //Balance = ContextManager.dLocalAccountsBalances.First(x => x.Account.Name == _account.Name).Balance;
            //Transactions = new ObservableCollection<TransactionAdapter>();

            //var query = ContextManager.Context.CoffeePurchases.Where(p => p.Date >= DateTime.Today.AddDays(-30) && p.Date <= DateTime.Today);
            //foreach (var purchase in query)
            //{
            //    Transactions.Add(new TransactionAdapter("Закупка кофе", purchase.Supplier.Name, purchase.Sum));
            //}
        }

        public string Header { get; set; }
        public double Balance { get; private set; }
        public ObservableCollection<TransactionAdapter> Transactions;

    }

    public class TransactionAdapter
    {
        public TransactionAdapter(string type, string recipient, double amount)
        {
            Type = type;
            Recipient = recipient;
            Amount = amount;
        }

        public string Type { get; set; }
        public string Recipient { get; set; }
        public double Amount { get; set; }
    }
}
