using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.PackagePurchases
{
    /// <summary>
    /// Interaction logic for ParentPackagePurchases.xaml
    /// </summary>
    public partial class ParentPackagePurchases : UserControl
    {
        public ParentPackagePurchases()
        {
            InitializeComponent();
        }


        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new PackagePurchase().ShowDialog();
            var viewModel = (vmPackagePurchases) DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.PackagePurchase;
            if (selected != null)
            {
                new PackagePurchase().ShowDialog();
                var viewModel = (vmPackagePurchases)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
