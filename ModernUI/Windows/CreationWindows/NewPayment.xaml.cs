using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewPayment.xaml
    /// </summary>
    public partial class NewPayment : MetroWindow
    {
        public NewPayment()
        {
            InitializeComponent();
            DataContext = new NewPaymentViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
