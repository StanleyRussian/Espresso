using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewCoffeePurchase.xaml
    /// </summary>
    public partial class NewCoffeePurchase: MetroWindow
    {
        public NewCoffeePurchase()
        {
            InitializeComponent();
            DataContext = new NewCoffeePurchaseViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
