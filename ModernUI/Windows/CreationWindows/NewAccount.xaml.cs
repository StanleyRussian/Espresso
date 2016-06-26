using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount: MetroWindow
    {
        public NewAccount()
        {
            InitializeComponent();
            DataContext = new NewAccountViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
