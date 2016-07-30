using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.CoffeePurchases
{
    /// <summary>
    /// Interaction logic for ParentCoffeePurchases.xaml
    /// </summary>
    public partial class ParentCoffeePurchases : UserControl
    {
        public ParentCoffeePurchases()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new CoffeePurchase().ShowDialog();
            var viewModel = (vmCoffeePurchases) DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.CoffeePurchase;
            if (selected != null)
            {
                new CoffeePurchase(selected).ShowDialog();
                var viewModel = (vmCoffeePurchases) DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
