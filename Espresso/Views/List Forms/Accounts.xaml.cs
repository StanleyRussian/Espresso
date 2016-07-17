using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : Window
    {
        public Accounts()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.AccountsViewModel();
        }
    }
}
