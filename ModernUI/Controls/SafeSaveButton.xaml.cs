using System.Windows;
using System.Windows.Controls;

namespace UI.Controls
{
    /// <summary>
    /// Interaction logic for SafeSaveButton.xaml
    /// </summary>
    public partial class SafeSaveButton : UserControl
    {
        public SafeSaveButton()
        {
            InitializeComponent();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            checkSafe.IsChecked = false;
        }
    }
}
