using System.Collections.ObjectModel;
using ViewModels.Pages.Explore;

namespace ViewModels.Pages
{
    public class vmExplorer:aTabViewModel
    {
        public vmExplorer()
        {
            Header = "Обзор";
            Tabs = new ObservableCollection<aTabViewModel>
            {
                new vmAccounts(),
                new vmCoffeeSorts(),
                new vmSuppliers(),
                new vmCoffeePurchases(),
                new vmRoastings()
            };
        }

        public ObservableCollection<aTabViewModel> Tabs { get; private set; }

        protected override void Load() { }
    }
}
