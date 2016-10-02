using System;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinMoneyTranfer: Abstract.aEntityWindowViewModel
    {
        public vmWinMoneyTranfer(object argEntity) : base(argEntity) { }

        //public vmWinMoneyTranfer(object argTranfer)
        //{
        //    if (argTranfer != null)
        //        Transfer = argTranfer as MoneyTransfer;
        //    else
        //    {
        //        CreatingNew = true;
        //        OnOpenNew();
        //    }
        //}

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

        private MoneyTransfer _oldTransfer;

        protected override void OnOpenEdit(object argEntity)
        {
            Transfer = argEntity as MoneyTransfer;
            _oldTransfer = Transfer.Clone();
        }

        protected override void OnOpenNew()
        {
            Transfer = new MoneyTransfer
            {
                Date = DateTime.Now
            };
        }

        protected override void OnSaveValidation()
        {
            if (Transfer.InitialAccount.Id == Transfer.TargetAccount.Id)
                throw new Exception("Счета совпадают");
        }

        protected override void OnSaveEdit()
        {
            // Delete transfer from database
            ContextManager.Context.MoneyTransfers.Remove(_oldTransfer);
            // Change account's balances
            ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == _oldTransfer.InitialAccount.Id).Balance += Transfer.Sum;
            ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == _oldTransfer.TargetAccount.Id).Balance -= Transfer.Sum;
            // Delete transactions
            ContextManager.Context.dTransactions.Remove(
                ContextManager.Context.dTransactions.Find(_oldTransfer.TransactionIDInitial));
            ContextManager.Context.dTransactions.Remove(
                ContextManager.Context.dTransactions.Find(_oldTransfer.TransactionIDTarget));
        }

        protected override void OnSaveNew()
        {
            // Change account's balances
            ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Transfer.InitialAccount.Id).Balance -= Transfer.Sum;
            ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Transfer.TargetAccount.Id).Balance += Transfer.Sum;

            // Add new transactions
            var dTransactionInitial = ContextManager.Context.dTransactions.Add(new dTransaction
            {
                Account = Transfer.InitialAccount,
                Date = Transfer.Date,
                Description = "Внутренний перевод",
                Participant = Transfer.TargetAccount.Name,
                Sum = - Transfer.Sum
            });

            var dTransactionTarget = ContextManager.Context.dTransactions.Add(new dTransaction
            {
                Account = Transfer.TargetAccount,
                Date = Transfer.Date,
                Description = "Внутренний перевод",
                Participant = Transfer.InitialAccount.Name,
                Sum = Transfer.Sum
            });

            Transfer.TransactionIDInitial = dTransactionInitial.Id;
            Transfer.TransactionIDTarget = dTransactionTarget.Id;

            // Add transfer to database
            ContextManager.Context.MoneyTransfers.Add(Transfer);
        }
    }
}
