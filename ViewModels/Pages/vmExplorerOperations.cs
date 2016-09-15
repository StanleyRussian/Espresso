using System.Collections.ObjectModel;
using ViewModels.Pages.Explore;

namespace ViewModels.Pages
{
    public class vmExplorerOperations : aTabViewModel
    {
        public vmExplorerOperations()
        {
            Header = "Операции";
            Tabs = new ObservableCollection<aTabViewModel>
            {
                new vmCoffeePurchases(),
                new vmRoastings(),
                new vmPackagePurchases(),
                new vmPackings(),
                new vmSales(),
                new vmMoneyTranfers(),
                new vmPayments(),
                new vmOtherProfits(),
                new vmProductPurchases(),
                new vmCoffeeTranfers()
            };
        }

        public ObservableCollection<aTabViewModel> Tabs { get; private set; }

        protected override void Load() { }
    }
}
