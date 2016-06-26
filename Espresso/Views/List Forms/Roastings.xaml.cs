using System.Windows;

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
