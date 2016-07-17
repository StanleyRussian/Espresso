using System.Windows;
using System.Windows.Controls;
using Model;
using Supplier = UI.Windows.EntityWindows.Supplier;

namespace UI.Controls.Combo
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
            new Supplier().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActiveSuppliers;
        }
    }
}
