using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewCoffeeSort.xaml
    /// </summary>
    public partial class CoffeeSort : MetroWindow
    {
        public CoffeeSort(object argSort = null)
        {
            InitializeComponent();
            DataContext = new CoffeeSortViewModel(argSort);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
