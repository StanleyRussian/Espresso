using System.Windows;
using System.Windows.Controls;
using Model;
using Account = UI.Windows.EntityWindows.Account;

namespace UI.Controls.Combo
{
    /// <summary>
    /// Interaction logic for comboAccount.xaml
    /// </summary>
    public partial class ComboActiveAccounts : UserControl
    {
        public ComboActiveAccounts()
        {
            InitializeComponent();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new Account().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActiveAccounts;
        }
    }
}
