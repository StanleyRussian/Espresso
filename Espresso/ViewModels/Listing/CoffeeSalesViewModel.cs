using System;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Entity;
using Core.ViewModels.Listing.Abstract;

namespace Core.ViewModels.Listing
{
    public class CoffeeSalesViewModel : aProcessListingViewModel
    {
        public CoffeeSalesViewModel(Account argAccount = null, Recipient argRecipient = null)
        {
            _filterAccount = argAccount;
            _filterRecipient = argRecipient;
        }

        protected override void Refresh()
        {
            var query = _context.CoffeeSales.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (_filterRecipient != null)
                query = query.Where(p => p.Recipient.Id == _filterRecipient.Id);
            if (_filterAccount != null)
                query = query.Where(p => p.Account.Id == _filterAccount.Id);

            CoffeeSales = new ObservableCollection<CoffeeSaleViewModel>();
            foreach (var x in query)
                CoffeeSales.Add(new CoffeeSaleViewModel(x));
        }

        #region Binding Properties

        private ObservableCollection<CoffeeSaleViewModel> _coffeeSales;
        public ObservableCollection<CoffeeSaleViewModel> CoffeeSales
        {
            get { return _coffeeSales; }
            private set
            {
                _coffeeSales = value;
                OnPropertyChanged("CoffeeSales");
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

        private Recipient _filterRecipient;
        public Recipient FilterRecipient
        {
            get { return _filterRecipient; }
            set
            {
                _filterRecipient = value;
                OnPropertyChanged("FilterRecipient");
                Refresh();
            }
        }

        #endregion

        #region Commands
        protected override void cmdSave_Execute()
        {
            foreach (var p in CoffeeSales)
                p.SaveDetails();
            base.cmdSave_Execute();
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            CoffeeSaleViewModel selected = argSelected as CoffeeSaleViewModel;
            foreach (var detail in selected.Details)
                _context.CoffeeSale_Details.Remove(_context.CoffeeSale_Details.Find(detail.Id));
            _context.CoffeeSales.Remove(_context.CoffeeSales.Find(selected.Id));
            SaveContext();
        }

        #endregion
    }






    // Helper class to represent individual CoffeeSale
    public class CoffeeSaleViewModel
    {
        private CoffeeSale _purchase;

        // Constructor
        public CoffeeSaleViewModel(CoffeeSale argSale)
        {
            _purchase = argSale;
            Details = new ObservableCollection<CoffeeSale_Details>();
            foreach (var x in _purchase.Sale_Details)
            {
                Details.Add(x);
            }
        }

        public void SaveDetails()
        {
            var context_Details = _purchase.Sale_Details.ToList();
            foreach (var detail in Details)
            {
                if (!context_Details.Contains(detail))
                    _purchase.Sale_Details.Add(detail);
            }
        }

        #region UI Properties

        public string Title => Date.ToString("d") + " - " + Recipient.Name;

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

        public Recipient Recipient
        {
            get { return _purchase.Recipient; }
            set { _purchase.Recipient = value; }
        }
        public Account Account
        {
            get { return _purchase.Account; }
            set { _purchase.Account = value; }
        }

        public ObservableCollection<CoffeeSale_Details> Details
        { get; set; }

        #endregion
    }

}
