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
    /// Interaction logic for NewMix.xaml
    /// </summary>
    public partial class NewMix : Window
    {
        public NewMix()
        {
            InitializeComponent();
            ViewModels.NewMixViewModel vm = new ViewModels.NewMixViewModel();
            DataContext = vm;

            columnCoffeeSort.ItemsSource = vm.CoffeeSorts;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
