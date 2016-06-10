
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
            cmdSaveChanges = new Auxiliary.Command(cmdSaveChanges_Execute);

            Refresh();
        }

        private void Refresh()
        {
            _context = new Entity.ContextContainer();
            NewPurchase = new Entity.CoffeePurchase();
            Details = new ObservableCollection<Entity.CoffeePurchase_Details>();

            _context.Accounts.Load();
            _context.Suppliers.Load();
            _context.CoffeeSorts.Load();

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

        public ObservableCollection<Entity.Account> Accounts
        {
            get { return _context.Accounts.Local; }
        }

        public ObservableCollection<Entity.Supplier> Suppliers
        {
            get { return _context.Suppliers.Local; }
        }

        public ObservableCollection<Entity.CoffeeSort> CoffeeSorts
        {
            get { return _context.CoffeeSorts.Local; }
        }

        #endregion

        #region Commands

        public ICommand cmdSaveChanges
        { get; private set; }


        private void cmdSaveChanges_Execute()
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
