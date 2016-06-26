using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewRoasting.xaml
    /// </summary>
    public partial class NewRoasting
    {
        public NewRoasting()
        {
            InitializeComponent();
            DataContext = new NewRoastingViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
