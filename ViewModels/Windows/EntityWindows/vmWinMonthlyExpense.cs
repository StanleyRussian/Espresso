using System;
using Model;
using Model.Entity;

namespace ViewModels.Windows.EntityWindows
{
    public class vmWinMonthlyExpense : Abstract.aEntityWindowViewModel
    {
        public vmWinMonthlyExpense(object argEntity) : base(argEntity) { }

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

        protected override void OnOpenEdit(object argEntity)
        {
            Expense = argEntity as MonthlyExpense;
        }

        protected override void OnOpenNew()
        {
            Expense = new MonthlyExpense
            {
                Day = DateTime.Now.Day,
                RemindingDay = DateTime.Now.AddDays(-5).Day
            };
        }

        protected override void OnSaveValidation() { }

        protected override void OnSaveEdit()
        {
            throw new NotImplementedException();
        }


        protected override void OnSaveNew()
        {
            ContextManager.Context.MonthlyExpenses.Add(Expense);
        }
    }
}
