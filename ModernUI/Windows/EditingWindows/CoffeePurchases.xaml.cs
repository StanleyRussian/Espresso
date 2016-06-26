using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for CoffeePurchases.xaml
    /// </summary>
    public partial class CoffeePurchases : MetroWindow
    {
        public CoffeePurchases()
        {
            InitializeComponent();
            DataContext = new CoffeePurchasesViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewCoffeePurchase().ShowDialog();
        }
    }
}
