using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for CoffeeSales.xaml
    /// </summary>
    public partial class CoffeeSales : MetroWindow
    {
        public CoffeeSales()
        {
            InitializeComponent();
            DataContext = new EditCoffeePurchasesViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewCoffeeSale().ShowDialog();
        }
    }
}
