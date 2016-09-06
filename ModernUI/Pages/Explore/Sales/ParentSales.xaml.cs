using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.Sales
{
    /// <summary>
    /// Interaction logic for ParentCoffeeSales.xaml
    /// </summary>
    public partial class ParentSales : UserControl
    {
        public ParentSales()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new Sale().ShowDialog();
            var viewModel = (vmSales)DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.Sale;
            if (selected != null)
            {
                new Sale(selected).ShowDialog();
                var viewModel = (vmSales)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
