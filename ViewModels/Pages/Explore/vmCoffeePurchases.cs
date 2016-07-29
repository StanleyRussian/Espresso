using System;
using System.Collections.ObjectModel;
using System.Linq;
using Model;
using Model.Entity;
using ViewModels.Pages.Explore.Abstract;

namespace ViewModels.Pages.Explore
{
    public class vmCoffeePurchases: aProcessListingViewModel
    {
        public vmCoffeePurchases()
        {
            Header = "Закупки кофе";
        }

        protected override void Load()
        {
            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);
            Refresh();
        }

        protected override void Refresh()
        {
            var query = ContextManager.Context.CoffeePurchases.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo);
            if (FilterSupplier != null)
                query = query.Where(p => p.Supplier.Id == FilterSupplier.Id);
            if (FilterAccount != null)
                query = query.Where(p => p.Account.Id == FilterAccount.Id);
            if (FilterCoffeeSort != null)
                query = query.Where(p => p.CoffeePurchase_Details
                    .FirstOrDefault(x => x.CoffeeSort.Id == FilterCoffeeSort.Id) != null);

            Tabs = new ObservableCollection<CoffeePurchase>(query);
        }

        private ObservableCollection<CoffeePurchase> _tabs;

        public ObservableCollection<CoffeePurchase> Tabs
        {
            get { return _tabs; }
            private set
            {
                _tabs = value;
                OnPropertyChanged();
            }
        }

        private Supplier _filterSupplier;
        public Supplier FilterSupplier
        {
            get { return _filterSupplier; }
            set
            {
                _filterSupplier = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private Account _filterAccount;
        public Account FilterAccount
        {
            get { return _filterAccount; }
            set
            {
                _filterAccount = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        private CoffeeSort _filterCoffeeSort;
        public CoffeeSort FilterCoffeeSort
        {
            get { return _filterCoffeeSort; }
            set
            {
                _filterCoffeeSort = value;
                OnPropertyChanged();
                Refresh();
            }
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            //if (IsEmpty(argSelected)) return;
            //var selected = argSelected as IndividualCoffeePurchaseViewModel;
            //ContextManager.Context.CoffeePurchases.Remove(selected.Purchase);
            //SaveContext();
            //Refresh();
        }
    }
}
