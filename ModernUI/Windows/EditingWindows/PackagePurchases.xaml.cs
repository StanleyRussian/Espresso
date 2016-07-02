using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for PackagePurchases.xaml
    /// </summary>
    public partial class PackagePurchases
    {
        public PackagePurchases()
        {
            InitializeComponent();
            DataContext = new EditPackagePurchasesViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewPackagePurchase().ShowDialog();
        }
    }
}
