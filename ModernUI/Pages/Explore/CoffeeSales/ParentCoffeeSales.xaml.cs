using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.CoffeeSales
{
    /// <summary>
    /// Interaction logic for ParentCoffeeSales.xaml
    /// </summary>
    public partial class ParentCoffeeSales : UserControl
    {
        public ParentCoffeeSales()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new CoffeeSale().ShowDialog();
            var viewModel = (vmCoffeeSales)DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.CoffeeSale;
            if (selected != null)
            {
                new CoffeePurchase(selected).ShowDialog();
                var viewModel = (vmCoffeeSales)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
