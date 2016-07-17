using System.Windows;

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

            ViewModels.Creational.NewRoastingViewModel vm = new ViewModels.Creational.NewRoastingViewModel();
            DataContext = vm;
            comboSort.ItemsSource = vm.CoffeeSorts;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
