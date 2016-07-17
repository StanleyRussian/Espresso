using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewPayment.xaml
    /// </summary>
    public partial class Payment : MetroWindow
    {
        public Payment(object argPayment = null)
        {
            InitializeComponent();
            DataContext = new PaymentViewModel(argPayment);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
