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
    /// Interaction logic for NewCoffeeTransfer.xaml
    /// </summary>
    public partial class NewCoffeeTransfer : Window
    {
        public NewCoffeeTransfer()
        {
            InitializeComponent();
            ViewModels.NewCoffeeTransferViewModel vm = new ViewModels.NewCoffeeTransferViewModel();
            DataContext = vm;
            comboMix.ItemsSource = vm.Mixes;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
