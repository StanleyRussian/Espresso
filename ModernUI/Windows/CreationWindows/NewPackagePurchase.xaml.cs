using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewPackagePurchase.xaml
    /// </summary>
    public partial class NewPackagePurchase : MetroWindow
    {
        public NewPackagePurchase()
        {
            InitializeComponent();
            DataContext = new NewPackagePurchaseViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
