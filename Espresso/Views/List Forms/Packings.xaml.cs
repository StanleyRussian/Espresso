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
    /// Interaction logic for Packings.xaml
    /// </summary>
    public partial class Packings : Window
    {
        public Packings()
        {
            InitializeComponent();

            ViewModels.PackingsViewModel vm = new ViewModels.PackingsViewModel();
            DataContext = vm;
            comboCategory.ItemsSource = vm.PackedCategories;
            comboMix.ItemsSource = vm.Mixes;
            comboPackage.ItemsSource = vm.Packages;
        }
    }
}
