using System.Windows;
using System.Windows.Controls;
using Model;
using UI.Windows.EntityWindows;

namespace UI.Controls.Combo
{
    /// <summary>
    /// Interaction logic for ActiveProducts.xaml
    /// </summary>
    public partial class ComboActiveProducts : UserControl
    {
        public ComboActiveProducts()
        {
            InitializeComponent();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new Product().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActiveProducts;
        }
    }
}
