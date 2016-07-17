using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for Packages.xaml
    /// </summary>
    public partial class Packages : Window
    {
        public Packages()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.PackagesViewModel();
        }
    }
}
