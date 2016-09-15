using System.Windows;
using ViewModels.Windows.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for ProductPurchase.xaml
    /// </summary>
    public partial class ProductPurchase
    {
        public ProductPurchase()
        {
            InitializeComponent();
            DataContext = new vmWinProductPurchase();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
