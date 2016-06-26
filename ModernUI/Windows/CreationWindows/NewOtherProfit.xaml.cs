using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewOtherProfit.xaml
    /// </summary>
    public partial class NewOtherProfit : MetroWindow
    {
        public NewOtherProfit()
        {
            InitializeComponent();
            DataContext = new NewOtherProfitViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
