using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.Windows.EntityWindows;

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
            DataContext = new vmWinOtherProfit(argProfit);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
