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
    public class CoffeeSalesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CoffeeSaleViewModel> _coffeeSales;
        private Entity.ContextContainer _context;

        public CoffeeSalesViewModel(Entity.Account argAccount = null, Entity.Recipient argRecipient = null)
        {
            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);
            _filterAccount = argAccount;
            _filterRecipient = argRecipient;

            cmdSave = new Auxiliary.Command(cmdSave_Execute);
            cmdDelete = new Auxiliary.Command(cmdDelete_Execute);
            cmdNew = new Auxiliary.Command(cmdNew_Execute);
            cmdFilter30Days = new Auxiliary.Command(cmdFilter30Days_Execute);
            cmdFilterAll = new Auxiliary.Command(cmdFilterAll_Execute);
            cmdClearRecipient = new Auxiliary.Command(cmdClearRecipient_Execute);
            cmdClearAccount = new Auxiliary.Command(cmdClearAccount_Execute);

            _context = new Entity.ContextContainer();
            CoffeeSales = new ObservableCollection<CoffeeSaleViewModel>();

            _context.Accounts.Load();
            _context.Recipients.Load();
            _context.Mixes.Load();
            _context.CoffeeSales.Load();

            Refresh();
        }

        // Refresh viewed items based on current filters without requering database
        private void Refresh()
        {
            var query = _context.CoffeeSales.Local.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (_filterRecipient != null)
                query = query.Where(p => p.Recipient.Id == _filterRecipient.Id);
            if (_filterAccount != null)
                query = query.Where(p => p.Account.Id == _filterAccount.Id);

            CoffeeSales.Clear();
            foreach (var x in query)
                CoffeeSales.Add(new CoffeeSaleViewModel(x));
        }

        #region Binding Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<CoffeeSaleViewModel> CoffeeSales
        {
            get { return _coffeeSales; }
            private set
            {
                _coffeeSales = value;
                OnPropertyChanged("CoffeeSales");
            }
        }

        public ObservableCollection<Entity.Account> Accounts
        {
            get { return _context.Accounts.Local; }
        }

        public ObservableCollection<Entity.Recipient> Recipients
        {
            get { return _context.Recipients.Local; }
        }

        public ObservableCollection<Entity.Mix> Mixes
        {
            get { return _context.Mixes.Local; }
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

        private Entity.Recipient _filterRecipient;
        public Entity.Recipient FilterRecipient
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

        public ICommand cmdSave
        { get; private set; }

        private void cmdSave_Execute()
        {
            foreach (var p in CoffeeSales)
                p.SaveDetails();

            _context.SaveChanges();
            Refresh();
        }

        public ICommand cmdDelete
        { get; private set; }

        private void cmdDelete_Execute(object argSelected)
        {
            if (argSelected == null)
            {
                MessageBox.Show("Вы не выбрали закупку!");
                return;
            }

            CoffeeSaleViewModel selected = argSelected as CoffeeSaleViewModel;
            foreach (var detail in selected.Details)
                _context.CoffeeSale_Details.Remove(_context.CoffeeSale_Details.Find(detail.Id));

            _context.CoffeeSales.Remove(_context.CoffeeSales.Find(selected.Id));
            _context.SaveChanges();
            Refresh();
        }

        public ICommand cmdNew
        { get; private set; }

        private void cmdNew_Execute(object argSelected)
        {
            new Views.NewCoffeeSale().ShowDialog();
            _context.CoffeeSales.Load();
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

        public ICommand cmdClearRecipient
        { get; private set; }

        private void cmdClearRecipient_Execute()
        {
            FilterRecipient = null;
        }

        public ICommand cmdClearAccount
        { get; private set; }

        private void cmdClearAccount_Execute()
        {
            FilterAccount = null;
        }

        #endregion
    }






    // Helper class to represent individual CoffeeSale
    public class CoffeeSaleViewModel /*: INotifyPropertyChanged*/
    {
        private Entity.CoffeeSale _purchase;

        // Constructor
        public CoffeeSaleViewModel(Entity.CoffeeSale argSale)
        {
            _purchase = argSale;

            Details = new ObservableCollection<Entity.CoffeeSale_Details>();
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

        #region UI Properties and INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title
        {
            get { return Date.ToString("d") + " - " + Recipient.Name; }
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

        public Entity.Recipient Recipient
        {
            get { return _purchase.Recipient; }
            set { _purchase.Recipient = value; }
        }
        public Entity.Account Account
        {
            get { return _purchase.Account; }
            set { _purchase.Account = value; }
        }

        public ObservableCollection<Entity.CoffeeSale_Details> Details
        { get; set; }

        #endregion
    }

}
