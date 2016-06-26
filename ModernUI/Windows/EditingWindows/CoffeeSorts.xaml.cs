using MahApps.Metro.Controls;
using System.Windows;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for CoffeeSorts.xaml
    /// </summary>
    public partial class CoffeeSorts : MetroWindow
    {
        public CoffeeSorts()
        {
            InitializeComponent();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewCoffeeSort().ShowDialog();
        }
    }
}
