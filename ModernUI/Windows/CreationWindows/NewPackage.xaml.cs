using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewPackage.xaml
    /// </summary>
    public partial class NewPackage : MetroWindow
    {
        public NewPackage()
        {
            InitializeComponent();
            DataContext = new NewPackageViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
