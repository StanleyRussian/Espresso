using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for Roastings.xaml
    /// </summary>
    public partial class Roastings
    {
        public Roastings()
        {
            InitializeComponent();
            DataContext = new RoastingsViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewRoasting().ShowDialog();
        }
    }
}
