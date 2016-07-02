using MahApps.Metro.Controls;
using System.Windows;
using Core;
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

        private void btnNewSort_Click(object sender, RoutedEventArgs e)
        {
            new NewCoffeeSort().ShowDialog();
            comboColumnSort.ItemsSource = ContextManager.ActiveCoffeeSorts;
        }
    }
}
