using System.Windows;
using ViewModels.Windows;

namespace UI.Windows
{
    /// <summary>
    /// Interaction logic for StocksCorrection.xaml
    /// </summary>
    public partial class StocksCorrection
    {
        public StocksCorrection()
        {
            InitializeComponent();
            DataContext = new vmWinStocksCorrection();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
