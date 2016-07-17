using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewPackagePurchase.xaml
    /// </summary>
    public partial class NewPackagePurchase : Window
    {
        public NewPackagePurchase()
        {
            InitializeComponent();
            ViewModels.Creational.NewPackagePurchaseViewModel vm = new ViewModels.Creational.NewPackagePurchaseViewModel();
            DataContext = vm;
            comboAccount.ItemsSource = vm.Accounts;
            comboPackage.ItemsSource = vm.Packages;
            comboSupplier.ItemsSource = vm.Suppliers;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
