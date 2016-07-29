using System.Windows.Controls;
using Account = UI.Windows.EntityWindows.Account;

namespace UI.Pages.Explore.Accounts
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
            var selected = tabs.SelectedItem as Model.Entity.Account;
            if (selected != null)
                new Account(selected).ShowDialog();
        }
    }
}
