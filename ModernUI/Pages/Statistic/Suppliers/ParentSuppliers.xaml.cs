using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Statistics.Suppliers;

namespace UI.Pages.Statistic.Suppliers
{
    /// <summary>
    /// Interaction logic for ParentSuppliers.xaml
    /// </summary>
    public partial class ParentSuppliers : UserControl
    {
        public ParentSuppliers()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new Supplier().ShowDialog();
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as IndividualSupplierViewModel;
            if (selected != null)
                new Supplier(selected.Supplier).ShowDialog();
        }
    }
}
