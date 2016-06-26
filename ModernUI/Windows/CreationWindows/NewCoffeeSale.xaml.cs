using Core.ViewModels.Creational;
using MahApps.Metro.Controls;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewCoffeeSale.xaml
    /// </summary>
    public partial class NewCoffeeSale : MetroWindow
    {
        public NewCoffeeSale()
        {
            InitializeComponent();
            DataContext = new NewCoffeeSaleViewModel();
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}
