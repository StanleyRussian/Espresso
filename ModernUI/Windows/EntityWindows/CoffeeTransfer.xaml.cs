using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewCoffeeTransfer.xaml
    /// </summary>
    public partial class CoffeeTransfer : MetroWindow
    {
        public CoffeeTransfer(object argTransfer = null)
        {
            InitializeComponent();
            DataContext = new CoffeeTransferViewModel(argTransfer);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
