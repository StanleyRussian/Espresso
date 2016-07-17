using System.Windows;
using System.Windows.Controls;
using Model;
using Mix = UI.Windows.EntityWindows.Mix;

namespace UI.Controls.Combo
{
    /// <summary>
    /// Interaction logic for comboActiveMixes.xaml
    /// </summary>
    public partial class ComboActiveMixes : UserControl
    {
        public ComboActiveMixes()
        {
            InitializeComponent();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new Mix().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActiveMixes;
        }
    }
}
