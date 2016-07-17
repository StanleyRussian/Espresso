using System.Windows;

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
            ViewModels.Creational.NewCoffeeTransferViewModel vm = new ViewModels.Creational.NewCoffeeTransferViewModel();
            DataContext = vm;
            comboMix.ItemsSource = vm.Mixes;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
