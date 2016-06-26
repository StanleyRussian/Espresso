using System;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class CoffeePurchasesViewModel : aProcessListingViewModel
    {
        public CoffeePurchasesViewModel(Account argAccount = null, Supplier argSupplier = null)
        {
            _filterAccount = argAccount;
            _filterSupplier = argSupplier;
        }

        protected override void Refresh()
        {
            var query = _context.CoffeePurchases.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (_filterSupplier != null)
                query = query.Where(p => p.Supplier.Id == _filterSupplier.Id);
            if (_filterAccount != null)
                query = query.Where(p => p.Account.Id == _filterAccount.Id);

            CoffeePurchases = new ObservableCollection<CoffeePurchaseViewModel>();
            foreach (var x in query)
                CoffeePurchases.Add(new CoffeePurchaseViewModel(x));
        }

        #region Binding Properties

        private ObservableCollection<CoffeePurchaseViewModel> _coffeePurchases;
        public ObservableCollection<CoffeePurchaseViewModel> CoffeePurchases
        {
            get { return _coffeePurchases; }
            private set
            {
                _coffeePurchases = value;
                OnPropertyChanged("CoffeePurchases");
            }
        }

        private Account _filterAccount;
        public Account FilterAccount
        {
            get { return _filterAccount; }
            set
            {
                _filterAccount = value;
                OnPropertyChanged("FilterAccount");
                Refresh();
            }
        }

        private Supplier _filterSupplier;
        public Supplier FilterSupplier
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

        protected override void cmdSave_Execute()
        {
            foreach (var p in CoffeePurchases)
                p.SaveDetails();
            base.cmdSave_Execute();
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            CoffeePurchaseViewModel selected = argSelected as CoffeePurchaseViewModel;
            foreach (var detail in selected.Details)
                _context.CoffeePurchase_Details.Remove(_context.CoffeePurchase_Details.Find(detail.Id));
            _context.CoffeePurchases.Remove(_context.CoffeePurchases.Find(selected.Id));
            SaveContext();
        }
        #endregion
    }






    // Helper class
    public class CoffeePurchaseViewModel
    {
        private CoffeePurchase _purchase;

        // Constructor
        public CoffeePurchaseViewModel(CoffeePurchase argPurchase)
        {
            _purchase = argPurchase;
            Details = new ObservableCollection<CoffeePurchase_Details>();
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

        public string Title => Date.ToString("d") + " - " + Supplier.Name;

        #endregion

        #region Entity Properties

        public int Id => _purchase.Id;

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

        public Supplier Supplier
        {
            get { return _purchase.Supplier; }
            set { _purchase.Supplier = value; }
        }
        public Account Account
        {
            get { return _purchase.Account; }
            set { _purchase.Account = value; }
        }

        public ObservableCollection<CoffeePurchase_Details> Details
        { get; set; }

        #endregion
    }

}
