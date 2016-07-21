using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.Entity;
using ViewModels.Auxiliary;
using ViewModels.Statistics.Abstract;

namespace ViewModels.Statistics.CoffeeSorts
{
    public class IndividualCoffeeSortViewModel: aTabViewModel
    {
        private ContextContainer _context = ContextManager.Context;

        public IndividualCoffeeSortViewModel(CoffeeSort coffeeSort)
        {
            CoffeeSort = coffeeSort;
            Header = coffeeSort.Name;
            cmdRefreshPurchaseStep = new Command(cmdRefreshPurchaseStep_Execute);
        }

        public CoffeeSort CoffeeSort { get; }
        public double CurrentGreenStocks { get; private set; }
        public double CurrentRoastedStocks { get; private set; }

        public ObservableCollection<hCoffeePurchaseViewModel> Purchases
        {
            get { return _purchases; }
            private set
            {
                _purchases = value; 
                OnPropertyChanged();
            }
        }

        public double PurchaseStep { get; private set; }

        protected override void Load()
        {
            _context = ContextManager.Context;
            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);

            cmdSave = new Command(cmdSave_Execute);
            cmdDelete = new Command(cmdDelete_Execute);
            cmdFilter30Days = new Command(cmdFilter30Days_Execute);
            cmdFilterAll = new Command(cmdFilterAll_Execute);

            CurrentGreenStocks = _context.dGreenStocks.Find(CoffeeSort.Id).Quantity;
            CurrentRoastedStocks = _context.dRoastedStocks.Find(CoffeeSort.Id).Quantity;

            var query = _context.CoffeePurchases
                .Where( p => p.CoffeePurchase_Details
                    .FirstOrDefault(x => x.CoffeeSort.Id == CoffeeSort.Id) != null)
                .OrderBy(p => p.Date)
                .Take(5);

            Purchases = new ObservableCollection<hCoffeePurchaseViewModel>();
            foreach (var purchase in query)
                Purchases.Add(new hCoffeePurchaseViewModel(purchase, CoffeeSort));

            if (CoffeeSort.dPurchaseStep != null)
                PurchaseStep = CoffeeSort.dPurchaseStep.Value;
        }

        private void ReloadPurchases()
        {
            var query = _context.CoffeePurchases
                .Where(p => p.CoffeePurchase_Details
                    .FirstOrDefault(x => x.CoffeeSort.Id == CoffeeSort.Id) != null
                    && p.Date >= _filterFrom && p.Date <= _filterTo);

            Purchases = new ObservableCollection<hCoffeePurchaseViewModel>();
            foreach (var purchase in query)
                Purchases.Add(new hCoffeePurchaseViewModel(purchase, CoffeeSort));

        }

        #region Binding Properties

        protected DateTime _filterFrom;
        public DateTime FilterFrom
        {
            get { return _filterFrom; }
            set
            {
                _filterFrom = value;
                OnPropertyChanged();
                ReloadPurchases();
            }
        }

        protected DateTime _filterTo;
        private ObservableCollection<hCoffeePurchaseViewModel> _purchases;

        public DateTime FilterTo
        {
            get { return _filterTo; }
            set
            {
                _filterTo = value;
                OnPropertyChanged();
                ReloadPurchases();
            }
        }

        #endregion

        #region Commands

        public ICommand cmdRefreshPurchaseStep { get; private set; }
        private void cmdRefreshPurchaseStep_Execute()
        {
            var query = _context.CoffeePurchases
                .Where(p => p.CoffeePurchase_Details
                    .FirstOrDefault(x => x.CoffeeSort.Id == CoffeeSort.Id) != null);
            PurchaseStep = query.Count()/12d;
        }

        public ICommand cmdSave { get; private set; }
        private void cmdSave_Execute()
        {
            _context.SaveChanges();
            DialogCoordinator.Instance.ShowMessageAsync(this, "Успех", "Сохранение завершено");
        }

        public ICommand cmdDelete { get; private set; }
        private void cmdDelete_Execute(object argSelected) { }

        public ICommand cmdFilter30Days { get; private set; }
        private void cmdFilter30Days_Execute()
        {
            _filterTo = DateTime.Now;
            OnPropertyChanged(nameof(FilterTo));
            FilterFrom = DateTime.Now.AddDays(-30);
        }

        public ICommand cmdFilterAll { get; private set; }
        private void cmdFilterAll_Execute()
        {
            _filterTo = DateTime.Now;
            OnPropertyChanged(nameof(FilterTo));
            FilterFrom = DateTime.MinValue;
        }

        #endregion

    }
}
