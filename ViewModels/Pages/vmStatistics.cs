using System.Collections.ObjectModel;
using ViewModels.Pages.Statistics;

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
                new vmStatsSorts(),
                new vmStatsPacked(),
                new vmStatsRecipients()
            };
        }

        public ObservableCollection<aTabViewModel> Tabs { get; private set; }

        protected override void Load() {}
    }
}
