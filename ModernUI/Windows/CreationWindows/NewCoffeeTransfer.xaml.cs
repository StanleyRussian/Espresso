using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewCoffeeTransfer.xaml
    /// </summary>
    public partial class NewCoffeeTransfer : MetroWindow
    {
        public NewCoffeeTransfer()
        {
            InitializeComponent();
            DataContext = new NewCoffeeTransferViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
