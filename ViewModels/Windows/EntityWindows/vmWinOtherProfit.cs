using System;
using System.Linq;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinOtherProfit : Abstract.aEntityWindowViewModel
    {
        public vmWinOtherProfit(object argProfit = null)
        {
            if (argProfit != null)
                Profit = argProfit as OtherProfit;
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }

        protected override void Refresh()
        {
            Profit = new OtherProfit
            {
                Date = DateTime.Now,
                Account = ContextManager.ActiveAccounts.FirstOrDefault()
            };
        }

        #region Binding Properties 

        private OtherProfit _profit;
        public OtherProfit Profit
        {
            get { return _profit; }
            set
            {
                _profit = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void OnSaveValidation()
        {
            if (Profit.Sum <= 0)
                throw new Exception("Введите сумму больше нуля");
        }

        protected override void OnSaveCreate()
        {
            // Add profit to database
            ContextManager.Context.OtherProfits.Add(Profit);
            // Change account balance
            ContextManager.Context.dAccountsBalances.First(
                p => p.Account.Id == Profit.Account.Id).Balance += Profit.Sum;
            // Add new transaction
            ContextManager.Context.dTransactions.Add(new dTransaction
            {
                Account = Profit.Account,
                Date = Profit.Date,
                Description = "Доход",
                Participant = Profit.Designation,
                Sum = Profit.Sum
            });

        }

        #endregion
    }
}
