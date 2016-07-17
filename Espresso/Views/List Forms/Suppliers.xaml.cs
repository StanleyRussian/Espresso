using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for Suppliers.xaml
    /// </summary>
    public partial class Suppliers : Window
    {
        public Suppliers()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.SuppliersViewModel();
        }
    }
}
