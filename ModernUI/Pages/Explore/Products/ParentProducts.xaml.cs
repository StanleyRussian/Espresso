using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.Products
{
    /// <summary>
    /// Interaction logic for ParentProducts.xaml
    /// </summary>
    public partial class ParentProducts
    {
        public ParentProducts()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new Product().ShowDialog();
            var viewModel = (vmProducts)DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.Product;
            if (selected != null)
            {
                new Product(selected).ShowDialog();
                var viewModel = (vmProducts)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }

    }
}
