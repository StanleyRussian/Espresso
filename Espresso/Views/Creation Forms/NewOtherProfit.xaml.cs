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
    /// Interaction logic for NewOtherProfit.xaml
    /// </summary>
    public partial class NewOtherProfit : Window
    {
        public NewOtherProfit()
        {
            InitializeComponent();
            ViewModels.NewOtherProfitViewModel vm = new ViewModels.NewOtherProfitViewModel();
            DataContext = vm;
            comboAccount.ItemsSource = vm.Accounts;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
