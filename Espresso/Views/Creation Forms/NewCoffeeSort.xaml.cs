using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewCoffeeSort.xaml
    /// </summary>
    public partial class NewCoffeeSort : Window
    {
        public NewCoffeeSort()
        {
            InitializeComponent();
            DataContext = new ViewModels.Creational.NewCoffeeSortViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
