using System.Windows;

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
            ViewModels.Creational.NewMixViewModel vm = new ViewModels.Creational.NewMixViewModel();
            DataContext = vm;

            columnCoffeeSort.ItemsSource = vm.CoffeeSorts;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
