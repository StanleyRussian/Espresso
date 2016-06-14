using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    class NewMonthlyExpenseViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public NewMonthlyExpenseViewModel()
        {
            _context = new Entity.ContextContainer();
            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            Refresh();
        }

        private void Refresh()
        {
            _newMonthlyExpense = new Entity.MonthlyExpense();
            _newMonthlyExpense.Day = DateTime.Now.Day;
            _newMonthlyExpense.RemindingDay = DateTime.Now.AddDays(-5).Day;
            OnPropertyChanged("NewMonthlyExpense");
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            _context.MonthlyExpenses.Add(NewMonthlyExpense);
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
