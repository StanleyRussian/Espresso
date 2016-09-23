using System.Windows;
using ViewModels.Windows.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class Account
    {
        public Account(object argAccount = null)
        {
            InitializeComponent();
            DataContext = new vmWinAccount(argAccount);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
