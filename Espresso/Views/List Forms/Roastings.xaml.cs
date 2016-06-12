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
    /// Interaction logic for Roastings.xaml
    /// </summary>
    public partial class Roastings : Window
    {
        public Roastings()
        {
            InitializeComponent();

            ViewModels.RoastingsViewModel vm = new ViewModels.RoastingsViewModel();
            DataContext = vm;
            comboSort.ItemsSource = vm.CoffeeSorts;
        }
    }
}
