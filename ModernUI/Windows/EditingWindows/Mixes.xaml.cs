using System.Windows;
using Core.ViewModels.Listing;
using MahApps.Metro.Controls;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for Mixes.xaml
    /// </summary>
    public partial class Mixes : MetroWindow
    {
        public Mixes()
        {
            InitializeComponent();
            DataContext = new MixesViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewMix().ShowDialog();
        }
    }
}
