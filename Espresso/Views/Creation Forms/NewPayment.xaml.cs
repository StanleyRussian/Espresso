using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewPayment.xaml
    /// </summary>
    public partial class NewPayment : Window
    {
        public NewPayment()
        {
            InitializeComponent();
            ViewModels.Creational.NewPaymentViewModel vm = new ViewModels.Creational.NewPaymentViewModel();
            DataContext = vm;
            comboAccount.ItemsSource = vm.Accounts;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
