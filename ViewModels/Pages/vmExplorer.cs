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
                new vmCoffeePurchases(),
                new vmRoastings(),
                new vmPackings(),
                new vmAccounts(),
                new vmCoffeeSorts(),
                new vmSuppliers(),
                new vmMixes(),
                new vmPackages(),
                new vmPackedCategories(),
                new vmPackagePurchases(),
                new vmCoffeeSales(),
                new vmRecipients(),
                new vmMoneyTranfers(),
                new vmPayments(),
                new vmOtherProfits(),
                new vmCoffeeTranfers()
            };
        }

        public ObservableCollection<aTabViewModel> Tabs { get; private set; }

        protected override void Load() { }
    }
}
