using System.Windows;
using Core.ViewModels;
using Core.ViewModels.Pages;

namespace ModernUI.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home
    {
        public Home()
        {
            InitializeComponent();
        }

        private void NewPurchase(object sender, RoutedEventArgs e)
        {
            new Windows.CreationWindows.NewCoffeePurchase().ShowDialog();
        }

        private void NewRoasting(object sender, RoutedEventArgs e)
        {
            new Windows.CreationWindows.NewRoasting().ShowDialog();
        }

        private void NewPacking(object sender, RoutedEventArgs e)
        {
            new Windows.CreationWindows.NewPacking().ShowDialog();
        }

        private void NewSale(object sender, RoutedEventArgs e)
        {
            new Windows.CreationWindows.NewCoffeeSale().ShowDialog();
        }
    }
}
