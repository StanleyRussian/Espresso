using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class Account: MetroWindow
    {
        public Account(object argAccount = null)
        {
            InitializeComponent();
            DataContext = new AccountViewModel(argAccount);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
