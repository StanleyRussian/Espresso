using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Espresso.ViewModels
{
    public class NewCoffeeSaleViewModel : INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        // Constructor
        public NewCoffeeSaleViewModel()
        {
            _context = new Entity.ContextContainer();

            _activeAccounts = new ObservableCollection<Entity.Account>(
                _context.Accounts.Where(x => x.Active == true));
            _activeMixes = new ObservableCollection<Entity.Mix>(
                _context.Mixes.Where(x => x.Active == true));
            _activeRecipients = new ObservableCollection<Entity.Recipient>(
                _context.Recipients.Where(x => x.Active == true));

            cmdSave = new Auxiliary.Command(cmdSave_Execute);

            Refresh();
        }

        private void Refresh()
        {
            _newSale = new Entity.CoffeeSale();
            Details = new ObservableCollection<Entity.CoffeeSale_Details>();

            _newSale.Date = DateTime.Now;
            _newSale.PaymentDate = DateTime.Now;
            _newSale.Paid = true;
            _newSale.Account = Accounts.FirstOrDefault();
            _newSale.Recipient = Recipients.FirstOrDefault();

            NewSale = _newSale;
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Entity.CoffeeSale _newSale;
        public Entity.CoffeeSale NewSale
        {
            get { return _newSale; }
            set
            {
                _newSale = value;
                OnPropertyChanged("NewSale");
            }
        }

        private ObservableCollection<Entity.CoffeeSale_Details> _details;
        public ObservableCollection<Entity.CoffeeSale_Details> Details
        {
            get { return _details; }
            set
            {
                _details = value;
                OnPropertyChanged("Details");
            }
        }

        private ObservableCollection<Entity.Account> _activeAccounts;
        public ObservableCollection<Entity.Account> Accounts
        {
            get { return _activeAccounts; }
        }

        private ObservableCollection<Entity.Recipient> _activeRecipients;
        public ObservableCollection<Entity.Recipient> Recipients
        {
            get { return _activeRecipients; }
        }

        private ObservableCollection<Entity.Mix> _activeMixes;
        public ObservableCollection<Entity.Mix> Mixes
        {
            get { return _activeMixes; }
        }

        #endregion

        #region Commands

        public ICommand cmdSave
        { get; private set; }


        private void cmdSave_Execute()
        {
            _newSale.Sale_Details.Clear();
            foreach (var x in Details)
                _newSale.Sale_Details.Add(x);
            _context.CoffeeSales.Add(_newSale);

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
