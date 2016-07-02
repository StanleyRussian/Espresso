using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for Payments.xaml
    /// </summary>
    public partial class Payments
    {
        public Payments()
        {
            InitializeComponent();
            DataContext = new EditPaymentsViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewPayment().ShowDialog();
        }
    }
}
