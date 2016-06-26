using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for Packing.xaml
    /// </summary>
    public partial class Packing
    {
        public Packing()
        {
            InitializeComponent();
            DataContext = new PackingsViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewPacking().ShowDialog();
        }
    }
}
