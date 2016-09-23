using System.Windows;
using ViewModels.Windows.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product
    {
        public Product(object argProduct = null)
        {
            InitializeComponent();
            DataContext = new vmWinProduct(argProduct);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
