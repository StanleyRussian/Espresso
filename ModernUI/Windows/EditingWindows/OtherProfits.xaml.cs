using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for OtherProfits.xaml
    /// </summary>
    public partial class OtherProfits
    {
        public OtherProfits()
        {
            InitializeComponent();
            DataContext = new EditOtherProfitsViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewOtherProfit().ShowDialog();
        }
    }
}
