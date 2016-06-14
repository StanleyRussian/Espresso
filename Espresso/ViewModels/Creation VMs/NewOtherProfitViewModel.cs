using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    class NewOtherProfitViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        public NewOtherProfitViewModel()
        {
            _context = new Entity.ContextContainer();
            _activeAccounts = new ObservableCollection<Entity.Account>(
                _context.Accounts.Where(x => x.Active == true));

            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            Refresh();
        }

        private void Refresh()
        {
            _newOtherProfit = new Entity.OtherProfit();
            _newOtherProfit.Date = DateTime.Now;
            _newOtherProfit.Account = Accounts.FirstOrDefault();
            OnPropertyChanged("NewOtherProfit");
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Entity.OtherProfit _newOtherProfit;
        public Entity.OtherProfit NewOtherProfit
        {
            get { return _newOtherProfit; }
            set
            {
                _newOtherProfit = value;
                OnPropertyChanged("NewOtherProfit");
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
            _context.OtherProfits.Add(NewOtherProfit);
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
