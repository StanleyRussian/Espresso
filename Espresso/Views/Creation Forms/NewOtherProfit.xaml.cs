using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewOtherProfit.xaml
    /// </summary>
    public partial class NewOtherProfit : Window
    {
        public NewOtherProfit()
        {
            InitializeComponent();
            ViewModels.Creational.NewOtherProfitViewModel vm = new ViewModels.Creational.NewOtherProfitViewModel();
            DataContext = vm;
            comboAccount.ItemsSource = vm.Accounts;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
