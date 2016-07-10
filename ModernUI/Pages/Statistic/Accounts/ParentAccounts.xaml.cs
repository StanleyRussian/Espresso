using Core.ViewModels.Statistics.Accounts;
using System.Windows.Controls;

namespace ModernUI.Pages.Statistic.Accounts
{
    /// <summary>
    /// Interaction logic for Parent.xaml
    /// </summary>
    public partial class ParentAccounts : UserControl
    {
        public ParentAccounts()
        {
            InitializeComponent();
        }

        private void btnNew_CLick(object sender, System.Windows.RoutedEventArgs e)
        {
            new Windows.CreationWindows.NewAccount().ShowDialog();
        }

        private void btnEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new Windows.EditingWindows.Accounts().ShowDialog();
        }
    }
}
