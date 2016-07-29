using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.Windows.EntityWindows;

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
            DataContext = new vmWinSupplier(argSupplier);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
