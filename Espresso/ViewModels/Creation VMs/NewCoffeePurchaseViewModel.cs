
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
    public class NewCoffeePurchaseViewModel: INotifyPropertyChanged
    {
        private Entity.ContextContainer _context;

        // Constructor
        public NewCoffeePurchaseViewModel()
        {
            _context = new Entity.ContextContainer();

            _activeAccounts = new ObservableCollection<Entity.Account>(
                _context.Accounts.Where(x => x.Active == true));
            _activeCoffeeSorts = new ObservableCollection<Entity.CoffeeSort>(
                _context.CoffeeSorts.Where(x => x.Active == true));
            _activeSuppliers = new ObservableCollection<Entity.Supplier>(
                _context.Suppliers.Where(x => x.Active == true));

            cmdSave = new Auxiliary.Command(cmdSave_Execute);

            Refresh();
        }

        private void Refresh()
        {
            NewPurchase = new Entity.CoffeePurchase();
            Details = new ObservableCollection<Entity.CoffeePurchase_Details>();

            NewPurchase.Date = DateTime.Now;
            NewPurchase.PaymentDate = DateTime.Now;
            NewPurchase.Paid = true;
            NewPurchase.Account = Accounts.FirstOrDefault();
            NewPurchase.Supplier = Suppliers.FirstOrDefault();
        }

        #region Binding Properties

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Entity.CoffeePurchase _newPurchase;
        public Entity.CoffeePurchase NewPurchase
        {
            get { return _newPurchase; }
            set
            {
                _newPurchase = value;
                OnPropertyChanged("NewPurchase");
            }
        }

        private ObservableCollection<Entity.CoffeePurchase_Details> _details;
        public ObservableCollection<Entity.CoffeePurchase_Details> Details
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

        private ObservableCollection<Entity.Supplier> _activeSuppliers;
        public ObservableCollection<Entity.Supplier> Suppliers
        {
            get { return _activeSuppliers; }
        }

        private ObservableCollection<Entity.CoffeeSort> _activeCoffeeSorts;
        public ObservableCollection<Entity.CoffeeSort> CoffeeSorts
        {
            get { return _activeCoffeeSorts; }
        }

        #endregion

        #region Commands

        public ICommand cmdSave
        { get; private set; }


        private void cmdSave_Execute()
        {
            foreach (var x in Details)
                _newPurchase.CoffeePurchase_Details.Add(x);
            _context.CoffeePurchases.Add(_newPurchase);
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
