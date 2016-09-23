using MahApps.Metro.Controls;
using ViewModels.Windows.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewCoffeeSale.xaml
    /// </summary>
    public partial class Sale : MetroWindow
    {
        public Sale(object argSale = null)
        {
            InitializeComponent();
            DataContext = new vmWinSale(argSale);
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}
