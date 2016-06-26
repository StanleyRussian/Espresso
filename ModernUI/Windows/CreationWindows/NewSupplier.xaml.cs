using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewSupplier.xaml
    /// </summary>
    public partial class NewSupplier : MetroWindow
    {
        public NewSupplier()
        {
            InitializeComponent();
            DataContext = new NewSupplierViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
