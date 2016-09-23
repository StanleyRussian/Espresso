using System.Collections.ObjectModel;
using ViewModels.Pages.Explore;

namespace ViewModels.Pages
{
    public class vmExplorer:aTabViewModel
    {
        public vmExplorer()
        {
            Header = "Объекты";
            Tabs = new ObservableCollection<aTabViewModel>
            {
                new vmAccounts(),
                new vmCoffeeSorts(),
                new vmSuppliers(),
                new vmMixes(),
                new vmPackages(),
                new vmRecipients(),
                new vmProducts(),
            };
        }

        public ObservableCollection<aTabViewModel> Tabs { get; private set; }

        protected override void Load() { }
    }
}
