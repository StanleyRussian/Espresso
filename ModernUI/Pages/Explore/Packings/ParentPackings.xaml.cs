using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.Packings
{
    /// <summary>
    /// Interaction logic for ParentPackings.xaml
    /// </summary>
    public partial class ParentPackings : UserControl
    {
        public ParentPackings()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new Packing().ShowDialog();
            var viewModel = (vmPackings) DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.Packing;
            if (selected != null)
            {
                new Packing(selected).ShowDialog();
                var viewModel = (vmPackings)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
