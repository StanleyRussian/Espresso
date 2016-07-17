using System.Collections.ObjectModel;
using ViewModels.Pages;
using ViewModels.Statistics.Abstract;

namespace ViewModels
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
