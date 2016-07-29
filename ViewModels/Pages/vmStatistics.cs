using System.Collections.ObjectModel;
using ViewModels.Pages.Statistics.Accounts;
using ViewModels.Pages.Statistics.CoffeeSorts;

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
                new vmStatsCoffeeSorts()
            };
        }

        public ObservableCollection<aTabViewModel> Tabs { get; private set; }

        protected override void Load() {}
    }
}
