using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for CoffeeSorts.xaml
    /// </summary>
    public partial class CoffeeSorts : Window
    {
        public CoffeeSorts()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.CoffeeSortsViewModel();
        }
    }
}
