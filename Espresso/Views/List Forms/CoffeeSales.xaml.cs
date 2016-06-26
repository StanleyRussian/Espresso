using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for CoffeeSales.xaml
    /// </summary>
    public partial class CoffeeSales : Window
    {
        public CoffeeSales()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.CoffeeSalesViewModel();
        }
    }
}
