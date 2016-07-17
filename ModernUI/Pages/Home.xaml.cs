using System.Windows;
using CoffeePurchase = UI.Windows.EntityWindows.CoffeePurchase;
using CoffeeSale = UI.Windows.EntityWindows.CoffeeSale;
using Packing = UI.Windows.EntityWindows.Packing;
using Roasting = UI.Windows.EntityWindows.Roasting;

namespace UI.Pages
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
            new CoffeePurchase().ShowDialog();
        }

        private void NewRoasting(object sender, RoutedEventArgs e)
        {
            new Roasting().ShowDialog();
        }

        private void NewPacking(object sender, RoutedEventArgs e)
        {
            new Packing().ShowDialog();
        }

        private void NewSale(object sender, RoutedEventArgs e)
        {
            new CoffeeSale().ShowDialog();
        }
    }
}
