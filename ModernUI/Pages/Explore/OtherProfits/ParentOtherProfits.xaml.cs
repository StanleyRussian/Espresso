using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.OtherProfits
{
    /// <summary>
    /// Interaction logic for ParentOtherProfits.xaml
    /// </summary>
    public partial class ParentOtherProfits : UserControl
    {
        public ParentOtherProfits()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new OtherProfit().ShowDialog();
            var viewModel = (vmOtherProfits)DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.OtherProfit;
            if (selected != null)
            {
                new OtherProfit(selected).ShowDialog();
                var viewModel = (vmOtherProfits)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
