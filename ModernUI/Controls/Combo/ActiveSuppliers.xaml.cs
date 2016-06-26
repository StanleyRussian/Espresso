using System.Windows;
using System.Windows.Controls;
using Core;

namespace ModernUI.Controls.Combo
{
    /// <summary>
    /// Interaction logic for comboSupplier.xaml
    /// </summary>
    public partial class ComboActiveSuppliers : UserControl
    {
        public ComboActiveSuppliers()
        {
            InitializeComponent();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new Windows.CreationWindows.NewSupplier().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActiveSuppliers;
        }
    }
}
