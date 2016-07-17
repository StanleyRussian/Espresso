using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewPackagePurchase.xaml
    /// </summary>
    public partial class PackagePurchase : MetroWindow
    {
        public PackagePurchase(object argPurchase = null)
        {
            InitializeComponent();
            DataContext = new PackagePurchaseViewModel(argPurchase);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
