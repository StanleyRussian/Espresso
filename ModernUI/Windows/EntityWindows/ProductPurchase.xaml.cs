using System.Windows;
using ViewModels.Windows.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for ProductPurchase.xaml
    /// </summary>
    public partial class ProductPurchase
    {
        public ProductPurchase(object argPurchase = null)
        {
            InitializeComponent();
            DataContext = new vmWinProductPurchase(argPurchase);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
