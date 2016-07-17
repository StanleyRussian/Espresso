using System.Windows;
using System.Windows.Controls;
using ViewModels.Statistics.CoffeeSorts;
using CoffeeSort = UI.Windows.EntityWindows.CoffeeSort;

namespace UI.Pages.Statistic.CoffeeSorts
{
    /// <summary>
    /// Interaction logic for ParentCoffeeSorts.xaml
    /// </summary>
    public partial class ParentCoffeeSorts : UserControl
    {
        public ParentCoffeeSorts()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, RoutedEventArgs e)
        {
            new CoffeeSort().ShowDialog();
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as IndividualCoffeeSortViewModel;
            if (selected != null)
                new CoffeeSort(selected.CoffeeSort).ShowDialog();
        }
    }
}
