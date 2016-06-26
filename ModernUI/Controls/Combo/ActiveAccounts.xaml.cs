using System.Windows;
using System.Windows.Controls;
using Core;

namespace ModernUI.Controls.Combo
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
            new Windows.CreationWindows.NewAccount().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActiveAccounts;
        }
    }
}
