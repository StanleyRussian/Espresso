using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.Payments
{
    /// <summary>
    /// Interaction logic for ParentPayments.xaml
    /// </summary>
    public partial class ParentPayments : UserControl
    {
        public ParentPayments()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new Payment().ShowDialog();
            var viewModel = (vmPayments)DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.Payment;
            if (selected != null)
            {
                new Payment(selected).ShowDialog();
                var viewModel = (vmPayments)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
