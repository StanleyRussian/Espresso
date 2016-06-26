using System;

namespace Core.ViewModels.Creational
{
    public class NewMonthlyExpenseViewModel : Abstract.aCreationalViewModel
    {
        public NewMonthlyExpenseViewModel() : base() { }

        protected override void Refresh()
        {
            NewMonthlyExpense = new Entity.MonthlyExpense();
            NewMonthlyExpense.Day = DateTime.Now.Day;
            NewMonthlyExpense.RemindingDay = DateTime.Now.AddDays(-5).Day;
        }

        #region Binding Properties

        private Entity.MonthlyExpense _newMonthlyExpense;
        public Entity.MonthlyExpense NewMonthlyExpense
        {
            get { return _newMonthlyExpense; }
            set
            {
                _newMonthlyExpense = value;
                OnPropertyChanged("NewMonthlyExpense");
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            _context.MonthlyExpenses.Add(NewMonthlyExpense);
            SaveContext();
        }

        #endregion
    }
}
