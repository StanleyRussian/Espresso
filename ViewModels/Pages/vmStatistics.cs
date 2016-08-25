using System.Collections.ObjectModel;
using ViewModels.Pages.Statistics.Accounts;
using ViewModels.Pages.Statistics.CoffeeSorts;
using ViewModels.Pages.Statistics.Mixes;

namespace ViewModels.Pages
{
    public class vmStatistics : aTabViewModel
    {
        public vmStatistics()
        {
            Header = "Статистика";
            Tabs = new ObservableCollection<aTabViewModel>
            {
                new vmStatsAccounts(),
                new vmStatsCoffeeSorts(),
                new vmStatsMixes()
            };
        }

        public ObservableCollection<aTabViewModel> Tabs { get; private set; }

        protected override void Load() {}
    }
}
