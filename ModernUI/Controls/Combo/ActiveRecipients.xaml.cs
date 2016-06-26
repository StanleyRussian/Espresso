using System.Windows;
using System.Windows.Controls;
using Core;

namespace ModernUI.Controls.Combo
{
    /// <summary>
    /// Interaction logic for comboActiveRecipients.xaml
    /// </summary>
    public partial class ComboActiveRecipients : UserControl
    {
        public ComboActiveRecipients()
        {
            InitializeComponent();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new Windows.CreationWindows.NewRecipient().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActiveRecipients;
        }
    }
}
