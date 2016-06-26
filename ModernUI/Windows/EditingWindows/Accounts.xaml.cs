using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts
    {
        public Accounts()
        {
            InitializeComponent();
            DataContext = new AccountsViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewAccount().ShowDialog();
        }
    }
}
