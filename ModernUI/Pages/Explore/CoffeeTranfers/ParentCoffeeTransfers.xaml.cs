using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.CoffeeTranfers
{
    /// <summary>
    /// Interaction logic for ParentCoffeeTranfers.xaml
    /// </summary>
    public partial class ParentCoffeeTransfers : UserControl
    {
        public ParentCoffeeTransfers()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new CoffeeTransfer().ShowDialog();
            var viewModel = (vmCoffeeTranfers)DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.CoffeeTransfer;
            if (selected != null)
            {
                new CoffeeTransfer(selected).ShowDialog();
                var viewModel = (vmCoffeeTranfers)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
