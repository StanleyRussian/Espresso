using System;
using Model;
using Model.Entity;

namespace ViewModels.EntityWindows
{
    public class MonthlyExpenseViewModel : Abstract.aEntityWindowViewModel
    {
        public MonthlyExpenseViewModel(object argExpense = null)
        {
            if (argExpense != null)
            {
                Expense = argExpense as MonthlyExpense;
            }
            else
            {
                CreatingNew = true;
                Refresh();
            }
        }
        protected override void Refresh()
        {
            Expense = new MonthlyExpense
            {
                Day = DateTime.Now.Day,
                RemindingDay = DateTime.Now.AddDays(-5).Day
            };
        }

        #region Binding Properties

        private MonthlyExpense _expense;
        public MonthlyExpense Expense
        {
            get { return _expense; }
            set
            {
                _expense = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            if (CreatingNew)
                ContextManager.Context.MonthlyExpenses.Add(Expense);
            SaveContext();
        }

        #endregion
    }
}
