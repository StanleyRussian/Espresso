using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewRecipient.xaml
    /// </summary>
    public partial class NewRecipient : MetroWindow
    {
        public NewRecipient()
        {
            InitializeComponent();
            DataContext = new NewRecipientViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
