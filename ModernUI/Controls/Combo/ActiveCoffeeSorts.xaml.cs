using System.Windows;
using System.Windows.Controls;
using Core;

namespace ModernUI.Controls.Combo
{
    /// <summary>
    /// Interaction logic for comboActiveCoffeeSorts.xaml
    /// </summary>
    public partial class ComboActiveCoffeeSorts : UserControl
    {
        public ComboActiveCoffeeSorts()
        {
            InitializeComponent();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new Windows.CreationWindows.NewCoffeeSort().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActiveCoffeeSorts;
        }
    }
}
