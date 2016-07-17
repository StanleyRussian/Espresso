using System.Windows.Controls;
using ViewModels.Statistics.Accounts;
using Account = UI.Windows.EntityWindows.Account;

namespace UI.Pages.Statistic.Accounts
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

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new Account().ShowDialog();
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as IndividualAccountViewModel;
            if (selected != null)
                new Account(selected.Account).ShowDialog();
        }
    }
}
