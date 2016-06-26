using System.Windows;
using System.Windows.Controls;
using Core;

namespace ModernUI.Controls.Combo
{
    /// <summary>
    /// Interaction logic for comboActivePackedCategories.xaml
    /// </summary>
    public partial class ComboActivePackedCategories : UserControl
    {
        public ComboActivePackedCategories()
        {
            InitializeComponent();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new Windows.CreationWindows.NewPackedCategory().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActivePackedCategories;
        }
    }
}
