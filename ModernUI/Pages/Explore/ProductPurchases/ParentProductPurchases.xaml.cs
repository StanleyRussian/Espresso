using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.ProductPurchases
{
    /// <summary>
    /// Interaction logic for ParentProductPurchases.xaml
    /// </summary>
    public partial class ParentProductPurchases : UserControl
    {
        public ParentProductPurchases()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new ProductPurchase().ShowDialog();
            var viewModel = (vmProductPurchases)DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.ProductPurchase;
            if (selected != null)
            {
                new ProductPurchase().ShowDialog();
                var viewModel = (vmProductPurchases)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
