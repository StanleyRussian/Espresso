using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for CoffeePurchaseList.xaml
    /// </summary>
    public partial class CoffeePurchases : Window
    {
        public CoffeePurchases()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.CoffeePurchasesViewModel();
        }
    }
}
