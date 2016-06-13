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
    /// Interaction logic for NewCoffeeSale.xaml
    /// </summary>
    public partial class NewCoffeeSale : Window
    {
        public NewCoffeeSale()
        {
            InitializeComponent();
            ViewModels.NewCoffeeSaleViewModel vm = new ViewModels.NewCoffeeSaleViewModel();
            DataContext = vm;
            comboAccount.ItemsSource = vm.Accounts;
            comboRecipient.ItemsSource = vm.Recipients;
            columnMix.ItemsSource = vm.Mixes;
        }
    }
}
