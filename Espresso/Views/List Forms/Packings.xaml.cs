using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for Packings.xaml
    /// </summary>
    public partial class Packings : Window
    {
        public Packings()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.PackingsViewModel();
        }
    }
}
