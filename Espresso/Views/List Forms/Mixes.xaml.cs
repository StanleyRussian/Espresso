using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for Mixes.xaml
    /// </summary>
    public partial class Mixes : Window
    {
        public Mixes()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.MixesViewModel();
        }
    }
}
