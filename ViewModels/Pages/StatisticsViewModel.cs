using System.Collections.ObjectModel;
using ViewModels.Statistics.Abstract;
using ViewModels.Statistics.Accounts;
using ViewModels.Statistics.CoffeePurchases;
using ViewModels.Statistics.CoffeeSorts;
using ViewModels.Statistics.Suppliers;

namespace ViewModels.Pages
{
    public class StatisticsViewModel : aTabViewModel
    {
        public StatisticsViewModel()
        {
            Header = "Статистика";
            StatisticTabs = new ObservableCollection<aTabViewModel>
            {
                new ParentAccountsViewModel(),
                new ParentCoffeeSortsViewModel(),
                new ParentSuppliersViewModel(),
                new ParentCoffeePurchasesViewModel()
            };
        }

        public ObservableCollection<aTabViewModel> StatisticTabs { get; private set; }

        protected override void Load() {}
    }
}
