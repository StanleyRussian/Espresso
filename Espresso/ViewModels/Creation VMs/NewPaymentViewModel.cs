using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    class NewPaymentViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public NewPaymentViewModel()
        {
            _context = new Entity.ContextContainer();
            _activeAccounts = new ObservableCollection<Entity.Account>(
                _context.Accounts.Where(x => x.Active == true));

            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            Refresh();
        }

        private void Refresh()
        {
            _newPayment = new Entity.Payment();
            _newPayment.Date = DateTime.Now;
            _newPayment.Account = Accounts.FirstOrDefault();
            OnPropertyChanged("NewPayment");
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Entity.Payment _newPayment;
        public Entity.Payment NewPayment
        {
            get { return _newPayment; }
            set
            {
                _newPayment = value;
                OnPropertyChanged("NewPayment");
            }
        }

        private ObservableCollection<Entity.Account> _activeAccounts;
        public ObservableCollection<Entity.Account> Accounts
        {
            get { return _activeAccounts; }
        }

        #endregion

        #region Commands

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.Payments.Add(NewPayment);
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно");
                Refresh();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var x in ex.EntityValidationErrors)
                    foreach (var y in x.ValidationErrors)
                        MessageBox.Show(y.ErrorMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public ICommand cmdMakeMonthly
        { get; private set; }

        private void cmdMakeMonthly_Execute()
        {
            Entity.MonthlyExpense NewMonthlyExpense = new Entity.MonthlyExpense
            {
                Amount = NewPayment.Amount,
                Designation = NewPayment.Designation,
                Day = NewPayment.Date.Day,
                RemindingDay = NewPayment.Date.AddDays(-5).Day
            };
            var addedExpense = _context.MonthlyExpenses.Add(NewMonthlyExpense);

            Entity.MonthlyPayment NewMonthlyPayment = new Entity.MonthlyPayment
            {
                Date = DateTime.Now,
                PaidAmount = NewPayment.Amount,
                MonthlyExpense = addedExpense,
                Account = _context.Accounts.FirstOrDefault()
            };
            _context.MonthlyPayments.Add(NewMonthlyPayment);

            try
            {
                _context.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно");
                Refresh();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var x in ex.EntityValidationErrors)
                    foreach (var y in x.ValidationErrors)
                        MessageBox.Show(y.ErrorMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
