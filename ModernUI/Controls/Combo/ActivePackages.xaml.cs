using System.Windows;
using System.Windows.Controls;
using Model;
using Package = UI.Windows.EntityWindows.Package;

namespace UI.Controls.Combo
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
            new Package().ShowDialog();
            ComboBox.ItemsSource = ContextManager.ActivePackages;
        }
    }
}
