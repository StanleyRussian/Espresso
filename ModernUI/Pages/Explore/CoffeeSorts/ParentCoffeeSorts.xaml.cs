using System.Windows;
using System.Windows.Controls;
using CoffeeSort = UI.Windows.EntityWindows.CoffeeSort;

namespace UI.Pages.Explore.CoffeeSorts
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
            var selected = tabs.SelectedItem as Model.Entity.CoffeeSort;
            if (selected != null)
                new CoffeeSort(selected).ShowDialog();
        }
    }
}
