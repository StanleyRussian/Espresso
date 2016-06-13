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
    /// Interaction logic for CoffeeSales.xaml
    /// </summary>
    public partial class CoffeeSales : Window
    {
        public CoffeeSales()
        {
            InitializeComponent();
            ViewModels.CoffeeSalesViewModel vm = new ViewModels.CoffeeSalesViewModel();
            DataContext = vm;
            comboAccount.ItemsSource = vm.Accounts;
            comboRecipient.ItemsSource = vm.Recipients;
            columnMix.ItemsSource = vm.Mixes;
        }
    }
}
