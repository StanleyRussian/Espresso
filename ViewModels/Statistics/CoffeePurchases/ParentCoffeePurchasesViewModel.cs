using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ViewModels.Statistics.Abstract;
using ViewModels.Statistics.Accounts;

namespace ViewModels.Statistics.CoffeePurchases
{
    public class ParentCoffeePurchasesViewModel: aProcessListingViewModel
    {
        private ObservableCollection<IndividualCoffeePurchaseViewModel> _tabs;

        public ParentCoffeePurchasesViewModel()
        {
            Header = "Закупки кофе";
        }

        public ObservableCollection<IndividualCoffeePurchaseViewModel> Tabs
        {
            get { return _tabs; }
            private set
            {
                _tabs = value;
                OnPropertyChanged();
            }
        }

        protected override void Load()
        {
            _filterTo = DateTime.Now;
            _filterFrom = DateTime.Now.AddDays(-30);

            Tabs = new ObservableCollection<IndividualCoffeePurchaseViewModel>();
            foreach (var purchase in ContextManager.Context.CoffeePurchases.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo))
                Tabs.Add(new IndividualCoffeePurchaseViewModel(purchase));
        }

        protected override void Refresh()
        {
            Tabs = new ObservableCollection<IndividualCoffeePurchaseViewModel>();
            foreach (var purchase in ContextManager.Context.CoffeePurchases.Where(p => p.Date >= _filterFrom && p.Date <= _filterTo))
                Tabs.Add(new IndividualCoffeePurchaseViewModel(purchase));
        }

        protected override void cmdDelete_Execute(object argSelected)
        {
            if (IsEmpty(argSelected)) return;
            var selected = argSelected as IndividualCoffeePurchaseViewModel;
            ContextManager.Context.CoffeePurchases.Remove(selected.Purchase);
            SaveContext();
            Refresh();
        }
    }
}
