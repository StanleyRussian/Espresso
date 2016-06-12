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
    public class CoffeePurchasesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CoffeePurchaseViewModel> _coffeePurchases;
        private Entity.ContextContainer _context;

        public CoffeePurchasesViewModel(Entity.Account argAccount = null, Entity.Supplier argSupplier = null)
        {
            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);
            _filterAccount = argAccount;
            _filterSupplier = argSupplier;

            cmdSaveChanges = new Auxiliary.Command(cmdSaveChanges_Execute);
            cmdDeletePurchase = new Auxiliary.Command(cmdDeletePurchase_Execute);
            cmdNewPurchase = new Auxiliary.Command(cmdNewPurchase_Execute);
            cmdFilter30Days = new Auxiliary.Command(cmdFilter30Days_Execute);
            cmdFilterAll = new Auxiliary.Command(cmdFilterAll_Execute);
            cmdClearSupplier = new Auxiliary.Command(cmdClearSupplier_Execute);
            cmdClearAccount = new Auxiliary.Command(cmdClearAccount_Execute);

            _context = new Entity.ContextContainer();
            CoffeePurchases = new ObservableCollection<CoffeePurchaseViewModel>();

            _context.Accounts.Load();
            _context.Suppliers.Load();
            _context.CoffeeSorts.Load();
            _context.CoffeePurchases.Load();

            Refresh();
        }

        // Refresh viewed items based on current filters without requering database
        private void Refresh()
        {
            var query = _context.CoffeePurchases.Local.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (_filterSupplier != null)
                query = query.Where(p => p.Supplier.Id == _filterSupplier.Id);
            if (_filterAccount != null)
                query = query.Where(p => p.Account.Id == _filterAccount.Id);

            CoffeePurchases.Clear();
            foreach (var x in query)
                CoffeePurchases.Add(new CoffeePurchaseViewModel(x));
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<CoffeePurchaseViewModel> CoffeePurchases
        {
            get { return _coffeePurchases; }
            private set
            {
                _coffeePurchases = value;
                OnPropertyChanged("CoffeePurchases");
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

        private DateTime _filterFrom;
        public DateTime FilterFrom
        {
            get { return _filterFrom; }
            set
            {
                _filterFrom = value;
                OnPropertyChanged("FilterFrom");
                Refresh();
            }
        }

        private DateTime _filterTo;
        public DateTime FilterTo
        {
            get { return _filterTo; }
            set
            {
                _filterTo = value;
                OnPropertyChanged("FilterTo");
                Refresh();
            }
        }

        private Entity.Account _filterAccount;
        public Entity.Account FilterAccount
        {
            get { return _filterAccount; }
            set
            {
                _filterAccount = value;
                OnPropertyChanged("FilterAccount");
                Refresh();
            }
        }

        private Entity.Supplier _filterSupplier;
        public Entity.Supplier FilterSupplier
        {
            get { return _filterSupplier; }
            set
            {
                _filterSupplier = value;
                OnPropertyChanged("FilterSupplier");
                Refresh();
            }
        }

        #endregion

        #region Commands

        public ICommand cmdSaveChanges
        { get; private set; }

        private void cmdSaveChanges_Execute()
        {
            foreach (var p in CoffeePurchases)
                p.SaveDetails();

            _context.SaveChanges();
            Refresh();
        }

        public ICommand cmdDeletePurchase
        { get; private set; }

        private void cmdDeletePurchase_Execute(object argSelected)
        {
            if (argSelected == null)
            {
                MessageBox.Show("Вы не выбрали закупку!");
                return;
            }

            CoffeePurchaseViewModel selected = argSelected as CoffeePurchaseViewModel;
            foreach (var detail in selected.Details)
                _context.CoffeePurchase_Details.Remove(_context.CoffeePurchase_Details.Find(detail.Id));

            _context.CoffeePurchases.Remove(_context.CoffeePurchases.Find(selected.Id));
            _context.SaveChanges();
            Refresh();
        }

        public ICommand cmdNewPurchase
        { get; private set; }

        private void cmdNewPurchase_Execute(object argSelected)
        {
            new Views.NewCoffeePurchase().ShowDialog();
            Refresh();
        }

        public ICommand cmdFilter30Days
        { get; private set; }

        private void cmdFilter30Days_Execute()
        {
            _filterTo = DateTime.Now;
            FilterFrom = DateTime.Now.AddDays(-30);
        }

        public ICommand cmdFilterAll
        { get; private set; }

         private void cmdFilterAll_Execute()
        {
            _filterTo = DateTime.Now;
            FilterFrom = DateTime.MinValue;
        }

        public ICommand cmdClearSupplier
        { get; private set; }

        private void cmdClearSupplier_Execute()
        {
            FilterSupplier = null;
        }

        public ICommand cmdClearAccount
        { get; private set; }

        private void cmdClearAccount_Execute()
        {
            FilterAccount = null;
        }

        #endregion
    }






    // Helper class to represent individual CoffeePurchase
    public class CoffeePurchaseViewModel : INotifyPropertyChanged
    {
        private Entity.CoffeePurchase _purchase;

        // Constructor
        public CoffeePurchaseViewModel(Entity.CoffeePurchase argPurchase)
        {
            _purchase = argPurchase;

            Details = new ObservableCollection<Entity.CoffeePurchase_Details>();
            foreach (var x in _purchase.CoffeePurchase_Details)
            {
                Details.Add(x);
            }
        }

        public void SaveDetails()
        {
            var context_Details = _purchase.CoffeePurchase_Details.ToList();
            foreach (var detail in Details)
            {
                if (!context_Details.Contains(detail))
                    _purchase.CoffeePurchase_Details.Add(detail);
            }
        }

        #region UI Properties and INotifyPropertyChanged implementation

        protected bool isSelected = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value != isSelected)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public string Title
        {
            get { return Date.ToString("d")/* + " - " + Supplier.Name*/; }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Entity Properties

        public int Id
        {
            get { return _purchase.Id; }
        }

        public DateTime Date
        {
            get { return _purchase.Date; }
            set { _purchase.Date = value; }
        }
        public DateTime PaymentDate
        {
            get { return _purchase.PaymentDate; }
            set { _purchase.PaymentDate = value; }
        }
        public bool Paid
        {
            get { return _purchase.Paid; }
            set { _purchase.Paid = value; }
        }

        public Entity.Supplier Supplier
        {
            get { return _purchase.Supplier; }
            set { _purchase.Supplier = value; }
        }
        public Entity.Account Account
        {
            get { return _purchase.Account; }
            set { _purchase.Account = value; }
        }

        public ObservableCollection<Entity.CoffeePurchase_Details> Details
        { get; set; }

        #endregion
    }

}
