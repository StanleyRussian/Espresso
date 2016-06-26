using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for Recipients.xaml
    /// </summary>
    public partial class Recipients
    {
        public Recipients()
        {
            InitializeComponent();
            DataContext = new RecipientsViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewRecipient().ShowDialog();
        }
    }
}
