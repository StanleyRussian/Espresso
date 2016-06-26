using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for Suppliers.xaml
    /// </summary>
    public partial class Suppliers
    {
        public Suppliers()
        {
            InitializeComponent();
            DataContext = new SuppliersViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewSupplier().ShowDialog();
        }
    }
}
