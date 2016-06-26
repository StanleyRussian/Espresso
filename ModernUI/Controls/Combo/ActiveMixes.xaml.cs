using System.Windows;
using System.Windows.Controls;
using Core;

namespace ModernUI.Controls.Combo
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
            new Windows.CreationWindows.NewMix().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActiveMixes;
        }
    }
}
