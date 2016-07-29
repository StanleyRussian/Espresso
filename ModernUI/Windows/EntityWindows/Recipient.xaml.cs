using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.Windows.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewRecipient.xaml
    /// </summary>
    public partial class Recipient : MetroWindow
    {
        public Recipient(object argRecipient = null)
        {
            InitializeComponent();
            DataContext = new vmWinRecipient(argRecipient);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
