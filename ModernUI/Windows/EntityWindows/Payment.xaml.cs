using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.Windows.EntityWindows;

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
            DataContext = new vmWinPayment(argPayment);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
