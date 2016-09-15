using System;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinMoneyTranfer: Abstract.aEntityWindowViewModel
    {
        public vmWinMoneyTranfer(object argTranfer)
        {
            if (argTranfer != null)
                Transfer = argTranfer as MoneyTransfer;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Transfer = new MoneyTransfer
            {
                Date = DateTime.Now
            };
        }

        private MoneyTransfer _transfer;
        public MoneyTransfer Transfer
        {
            get { return _transfer; }
            set
            {
                _transfer = value;
                OnPropertyChanged();
            }
        }

        protected override void OnSaveValidation()
        {
            if (Transfer.InitialAccount.Id == Transfer.TargetAccount.Id)
                throw new Exception("Счета совпадают");
        }

        protected override void OnSaveCreate()
        {
            // Add transfer to database
            ContextManager.Context.MoneyTransfers.Add(Transfer);
            // Change account's balances
            ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Transfer.InitialAccount.Id).Balance -= Transfer.Sum;
            ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Transfer.TargetAccount.Id).Balance += Transfer.Sum;
            // Add new transaction
            ContextManager.Context.dTransactions.Add(new dTransaction
            {
                Account = Transfer.InitialAccount,
                Date = Transfer.Date,
                Description = "Внутренний перевод",
                Participant = Transfer.TargetAccount.Name,
                Sum = Transfer.Sum
            });

            ContextManager.Context.dTransactions.Add(new dTransaction
            {
                Account = Transfer.TargetAccount,
                Date = Transfer.Date,
                Description = "Внутренний перевод",
                Participant = Transfer.InitialAccount.Name,
                Sum = Transfer.Sum
            });
        }
    }
}
