using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.MoneyTransfers
{
    /// <summary>
    /// Interaction logic for ParentMoneyTranfers.xaml
    /// </summary>
    public partial class ParentMoneyTransfers : UserControl
    {
        public ParentMoneyTransfers()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new MoneyTransfer().ShowDialog();
            var viewModel = (vmMoneyTranfers)DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.MoneyTransfer;
            if (selected != null)
            {
                new MoneyTransfer(selected).ShowDialog();
                var viewModel = (vmMoneyTranfers)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
