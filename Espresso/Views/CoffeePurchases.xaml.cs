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
    /// Interaction logic for CoffeePurchaseList.xaml
    /// </summary>
    public partial class CoffeePurchases : Window
    {
        public CoffeePurchases()
        {
            ViewModels.CoffeePurchasesViewModel vm = new ViewModels.CoffeePurchasesViewModel();

            InitializeComponent();
            DataContext = vm;

            // These are nessesary since DataContext of these items is selected purchase and not list
            columnCoffeeSort.ItemsSource = vm.CoffeeSorts;
            comboAccount.ItemsSource = vm.Accounts;
            comboSupplier.ItemsSource = vm.Suppliers;
        }
    }
}
