using System.Windows;
using System.Windows.Controls;
using Model;
using CoffeeSort = UI.Windows.EntityWindows.CoffeeSort;

namespace UI.Controls.Combo
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
            new CoffeeSort().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActiveCoffeeSorts;
        }
    }
}
