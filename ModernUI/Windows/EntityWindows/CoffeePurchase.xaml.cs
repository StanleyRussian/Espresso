using System.Windows;
using MahApps.Metro.Controls;
using Model;
using ViewModels.Windows.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewCoffeePurchase.xaml
    /// </summary>
    public partial class CoffeePurchase: MetroWindow
    {
        public CoffeePurchase(object argPurchase = null)
        {
            InitializeComponent();
            DataContext = new vmWinCoffeePurchase(argPurchase);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNewSort_Click(object sender, RoutedEventArgs e)
        {
            new CoffeeSort().ShowDialog();
        }
    }
}
