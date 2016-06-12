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
    /// Interaction logic for NewCoffeePurchase.xaml
    /// </summary>
    public partial class NewCoffeePurchase : Window
    {
        public NewCoffeePurchase()
        {
            InitializeComponent();
            ViewModels.NewCoffeePurchaseViewModel vm = new ViewModels.NewCoffeePurchaseViewModel();
            DataContext = vm;

            comboSupplier.ItemsSource = vm.Suppliers;
            comboAccount.ItemsSource = vm.Accounts;
            columnCoffeeSort.ItemsSource = vm.CoffeeSorts;
        }
    }
}
