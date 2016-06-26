using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewCoffeeSort.xaml
    /// </summary>
    public partial class NewCoffeeSort : MetroWindow
    {
        public NewCoffeeSort()
        {
            InitializeComponent();
            DataContext = new NewCoffeeSortViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
