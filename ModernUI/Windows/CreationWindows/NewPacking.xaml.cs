using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewPacking.xaml
    /// </summary>
    public partial class NewPacking : MetroWindow
    {
        public NewPacking()
        {
            InitializeComponent();
            DataContext = new NewPackingViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
