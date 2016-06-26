using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewMix.xaml
    /// </summary>
    public partial class NewMix : MetroWindow
    {
        public NewMix()
        {
            InitializeComponent();
            DataContext = new NewMixViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
