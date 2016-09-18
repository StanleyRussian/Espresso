using System.Collections.ObjectModel;
using ViewModels.Pages;

namespace ViewModels.Windows
{
    public class vmWinMain
    {
        public vmWinMain()
        {
            Tabs = new ObservableCollection<aTabViewModel>
            {
                new vmHome(),
                new vmStatistics(),
                new vmExplorer(),
                new vmExplorerOperations(),
                new vmUnpaid()
            };
        }

        public ObservableCollection<aTabViewModel> Tabs { get; private set; }
    }
}
