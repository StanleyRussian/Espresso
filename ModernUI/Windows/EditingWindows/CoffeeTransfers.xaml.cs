using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for CoffeeTransfers.xaml
    /// </summary>
    public partial class CoffeeTransfers
    {
        public CoffeeTransfers()
        {
            InitializeComponent();
            DataContext = new EditCoffeeTransfersViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewCoffeeTransfer().ShowDialog();
        }
    }
}
