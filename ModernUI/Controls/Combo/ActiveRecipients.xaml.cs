using System.Windows;
using System.Windows.Controls;
using Model;
using Recipient = UI.Windows.EntityWindows.Recipient;

namespace UI.Controls.Combo
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
            new Recipient().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActiveRecipients;
        }
    }
}
