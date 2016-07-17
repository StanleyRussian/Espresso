using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewSupplier.xaml
    /// </summary>
    public partial class Supplier : MetroWindow
    {
        public Supplier(object argSupplier = null)
        {
            InitializeComponent();
            DataContext = new SupplierViewModel(argSupplier);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
