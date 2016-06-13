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
    /// Interaction logic for PackagePurchases.xaml
    /// </summary>
    public partial class PackagePurchases : Window
    {
        public PackagePurchases()
        {
            InitializeComponent();
            ViewModels.PackagePurchasesViewModel vm = new ViewModels.PackagePurchasesViewModel();
            DataContext = vm;
            comboAccount.ItemsSource = vm.Accounts;
            comboPackage.ItemsSource = vm.Packages;
            comboSupplier.ItemsSource = vm.Suppliers;
        }
    }
}
