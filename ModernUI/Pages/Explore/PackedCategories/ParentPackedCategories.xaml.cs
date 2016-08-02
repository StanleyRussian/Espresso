using System.Windows;
using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.PackedCategories
{
    /// <summary>
    /// Interaction logic for ParentPackedCategories.xaml
    /// </summary>
    public partial class ParentPackedCategories : UserControl
    {
        public ParentPackedCategories()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, RoutedEventArgs e)
        {
            new PackedCategory().ShowDialog();
            var viewModel = (vmPackedCategories) DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.PackedCategory;
            if (selected != null)
            {
                new CoffeeSort(selected).ShowDialog();
                var viewModel = (vmPackedCategories)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
