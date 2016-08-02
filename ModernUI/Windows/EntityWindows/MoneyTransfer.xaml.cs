using System.Windows;
using ViewModels.Windows.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for MoneyTranfer.xaml
    /// </summary>
    public partial class MoneyTransfer
    {
        public MoneyTransfer(object argTranfer = null)
        {
            InitializeComponent();
            DataContext = new vmWinMoneyTranfer(argTranfer);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
