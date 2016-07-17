using MahApps.Metro.Controls;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewCoffeeSale.xaml
    /// </summary>
    public partial class CoffeeSale : MetroWindow
    {
        public CoffeeSale(object argSale = null)
        {
            InitializeComponent();
            DataContext = new CoffeeSaleViewModel(argSale);
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}
