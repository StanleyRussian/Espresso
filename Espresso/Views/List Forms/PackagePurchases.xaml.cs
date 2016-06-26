using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for PackagePurchases.xaml
    /// </summary>
    public partial class PackagePurchases : Window
    {
        public PackagePurchases()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.PackagePurchasesViewModel();
        }
    }
}
