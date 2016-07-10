using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ViewModels.Statistics;
using Core.ViewModels.Statistics.Abstract;
using Core.ViewModels.Statistics.Accounts;

namespace Core.ViewModels.Pages
{
    public class StatisticsViewModel : aTabViewModel
    {
        public StatisticsViewModel()
        {
            Header = "Статистика";
            StatisticTabs = new ObservableCollection<aTabViewModel>
            {
                new ParentAccountsViewModel(),
            };
        }

        public ObservableCollection<aTabViewModel> StatisticTabs { get; private set; }
        public string Header { get; set; }

        protected override void Load() {}
    }
}
