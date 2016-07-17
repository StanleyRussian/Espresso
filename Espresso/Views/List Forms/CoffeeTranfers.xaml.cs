using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for CoffeeTranfers.xaml
    /// </summary>
    public partial class CoffeeTranfers : Window
    {
        public CoffeeTranfers()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.CoffeeTransfersViewModel();
        }
    }
}
