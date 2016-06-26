using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewSupplier.xaml
    /// </summary>
    public partial class NewSupplier : Window
    {
        public NewSupplier()
        {
            InitializeComponent();
            DataContext = new ViewModels.Creational.NewSupplierViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
