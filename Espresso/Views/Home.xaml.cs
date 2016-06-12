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
    }
}
