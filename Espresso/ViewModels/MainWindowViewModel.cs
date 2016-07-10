using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ViewModels.Pages;
using Core.ViewModels.Statistics;
using Core.ViewModels.Statistics.Abstract;

namespace Core.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Tabs = new ObservableCollection<aTabViewModel>
            {
                new HomeViewModel(),
                new StatisticsViewModel()
            };
        }

        public ObservableCollection<aTabViewModel> Tabs { get; private set; }
    }
}
