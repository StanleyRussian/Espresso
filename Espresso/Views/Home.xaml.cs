using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            DataContext = new ViewModels.HomeViewModel();
        }

        private void NewAccount(object sender, RoutedEventArgs e)
        {
            new NewAccount().ShowDialog();
        }

        private void Accounts(object sender, RoutedEventArgs e)
        {
            new Accounts().ShowDialog();
        }

        private void NewSupplier(object sender, RoutedEventArgs e)
        {
            new NewSupplier().ShowDialog();
        }

        private void Suppliers(object sender, RoutedEventArgs e)
        {
            new Suppliers().ShowDialog();
        }

        private void NewCoffeeSort(object sender, RoutedEventArgs e)
        {
            new NewCoffeeSort().ShowDialog();
        }

        private void CoffeeSorts(object sender, RoutedEventArgs e)
        {
            new CoffeeSorts().ShowDialog();
        }

        private void NewCoffeePurchase(object sender, RoutedEventArgs e)
        {
            new NewCoffeePurchase().ShowDialog();
        }

        private void CoffeePurchases(object sender, RoutedEventArgs e)
        {
            new CoffeePurchases().ShowDialog();
        }

        private void NewRoasting(object sender, RoutedEventArgs e)
        {
            new NewRoasting().ShowDialog();
        }

        private void Roastings(object sender, RoutedEventArgs e)
        {
            new Roastings().ShowDialog();
        }

        private void NewMix(object sender, RoutedEventArgs e)
        {
            new NewMix().ShowDialog();
        }

        private void Mixes(object sender, RoutedEventArgs e)
        {
            new Mixes().ShowDialog();
        }

        private void NewPackage(object sender, RoutedEventArgs e)
        {
            new NewPackage().ShowDialog();
        }

        private void Packages(object sender, RoutedEventArgs e)
        {
            new Packages().ShowDialog();
        }

        private void NewCategory(object sender, RoutedEventArgs e)
        {
            new NewPackedCategory().ShowDialog();
        }

        private void Categories(object sender, RoutedEventArgs e)
        {
            new PackedCategories().ShowDialog();
        }

        private void NewPacking(object sender, RoutedEventArgs e)
        {
            new NewPacking().ShowDialog();
        }

        private void Packings(object sender, RoutedEventArgs e)
        {
            new Packings().ShowDialog();
        }

        private void NewPackagePurchase(object sender, RoutedEventArgs e)
        {
            new PackagePurchases().ShowDialog();
        }

        private void PackagePurchases(object sender, RoutedEventArgs e)
        {
            new PackagePurchases().ShowDialog();
        }

        private void NewRecipient(object sender, RoutedEventArgs e)
        {
            new NewRecipient().ShowDialog();
        }

        private void Recipients(object sender, RoutedEventArgs e)
        {
            new Recipients().ShowDialog();
        }

        private void NewCoffeeSale(object sender, RoutedEventArgs e)
        {
            new NewCoffeeSale().ShowDialog();
        }

        private void CoffeeSales(object sender, RoutedEventArgs e)
        {
            new CoffeeSales().ShowDialog();
        }

        private void CoffeeTranfers(object sender, RoutedEventArgs e)
        {
            new CoffeeTranfers().ShowDialog();
        }

        private void NewCoffeeTranfer(object sender, RoutedEventArgs e)
        {
            new NewCoffeeTransfer().ShowDialog();
        }

        private void NewPayment(object sender, RoutedEventArgs e)
        {
            new NewPayment().ShowDialog();
        }

        private void Payments(object sender, RoutedEventArgs e)
        {
            new Payments().ShowDialog();
        }

        private void NewMonthlyExpense(object sender, RoutedEventArgs e)
        {
            new NewMonthlyExpense().ShowDialog();
        }

        private void MontlyExpenses(object sender, RoutedEventArgs e)
        {
            new MonthlyExpenses().ShowDialog();
        }
    }
}
