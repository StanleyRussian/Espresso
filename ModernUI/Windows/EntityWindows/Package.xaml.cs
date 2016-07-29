using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.Windows.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewPackage.xaml
    /// </summary>
    public partial class Package : MetroWindow
    {
        public Package(object argPackage = null)
        {
            InitializeComponent();
            DataContext = new vmWinPackage(argPackage);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
