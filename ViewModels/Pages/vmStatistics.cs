using System.Collections.ObjectModel;
using ViewModels.Pages.Statistics;

namespace ViewModels.Pages
{
    public class vmStatistics : aTabViewModel
    {
        public vmStatistics()
        {
            Header = "Отчёты";
            Tabs = new ObservableCollection<aTabViewModel>
            {
                new vmStatsTransactions(),
                new vmStatsCoffeePurchases(),
                new vmStatsSales()
            };
        }

        public ObservableCollection<aTabViewModel> Tabs { get; private set; }

        protected override void Load() {}
    }
}
