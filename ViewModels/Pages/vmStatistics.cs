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
                /*new vmStatsPackedStocks(),*/
                new vmStatsCoffeeStocks(),
                new vmStatsTransactions(),
                new vmStatsSales()
            };
        }

        public ObservableCollection<aTabViewModel> Tabs { get; private set; }

        protected override void Load() {}
    }
}
