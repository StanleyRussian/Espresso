using System.Windows;
using System.Windows.Controls;
using Core;

namespace ModernUI.Controls.Combo
{
    /// <summary>
    /// Interaction logic for comboActivePackages.xaml
    /// </summary>
    public partial class ComboActivePackages : UserControl
    {
        public ComboActivePackages()
        {
            InitializeComponent();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new Windows.CreationWindows.NewPackage().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActivePackages;
        }
    }
}
