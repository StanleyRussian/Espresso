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
    /// Interaction logic for NewRoasting.xaml
    /// </summary>
    public partial class NewRoasting : Window
    {
        public NewRoasting()
        {
            InitializeComponent();

            ViewModels.NewRoastingViewModel vm = new ViewModels.NewRoastingViewModel();
            DataContext = vm;
            comboSort.ItemsSource = vm.CoffeeSorts;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
