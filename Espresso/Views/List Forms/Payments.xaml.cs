using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for Payments.xaml
    /// </summary>
    public partial class Payments : Window
    {
        public Payments()
        {
            InitializeComponent();
            ViewModels.PaymentsViewModel vm = new ViewModels.PaymentsViewModel();
            DataContext = vm;
            comboAccount.ItemsSource = vm.Accounts;
        }
    }
}
