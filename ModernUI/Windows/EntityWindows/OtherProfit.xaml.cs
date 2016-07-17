using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewOtherProfit.xaml
    /// </summary>
    public partial class OtherProfit : MetroWindow
    {
        public OtherProfit(object argProfit = null)
        {
            InitializeComponent();
            DataContext = new OtherProfitViewModel(argProfit);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
