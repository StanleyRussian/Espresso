using System.Windows;
using System.Windows.Controls;
using Model;
using PackedCategory = UI.Windows.EntityWindows.PackedCategory;

namespace UI.Controls.Combo
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
            new PackedCategory().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActivePackedCategories;
        }
    }
}
