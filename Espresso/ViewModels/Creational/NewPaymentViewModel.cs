using System;
using System.Linq;
using System.Windows.Input;

namespace Core.ViewModels.Creational
{
    public class NewPaymentViewModel :Abstract.aCreationalViewModel
    {
        protected override void Refresh()
        {
            NewPayment = new Entity.Payment
            {
                Date = DateTime.Now,
                Account = ContextManager.ActiveAccounts.FirstOrDefault()
            };
        }

        #region Binding Properties 

        private Entity.Payment _newPayment;
        public Entity.Payment NewPayment
        {
            get { return _newPayment; }
            set
            {
                _newPayment = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        protected override void cmdSave_Execute()
        {
            _context.Payments.Add(NewPayment);
            SaveContext();
        }


        public ICommand cmdMakeMonthly
        { get; private set; }

        private void cmdMakeMonthly_Execute()
        {
            Entity.MonthlyExpense NewMonthlyExpense = new Entity.MonthlyExpense
            {
                Amount = NewPayment.Sum,
                Designation = NewPayment.Designation,
                Day = NewPayment.Date.Day,
                RemindingDay = NewPayment.Date.AddDays(-5).Day
            };
            var addedExpense = _context.MonthlyExpenses.Add(NewMonthlyExpense);

            Entity.MonthlyPayment NewMonthlyPayment = new Entity.MonthlyPayment
            {
                Date = DateTime.Now,
                PaidAmount = NewPayment.Sum,
                MonthlyExpense = addedExpense,
                Account = _context.Accounts.FirstOrDefault()
            };
            _context.MonthlyPayments.Add(NewMonthlyPayment);

            SaveContext();
        }

        #endregion
    }
}
