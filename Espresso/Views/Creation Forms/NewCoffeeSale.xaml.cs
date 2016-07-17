using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewCoffeeSale.xaml
    /// </summary>
    public partial class NewCoffeeSale : Window
    {
        public NewCoffeeSale()
        {
            InitializeComponent();
            ViewModels.Creational.NewCoffeeSaleViewModel vm = new ViewModels.Creational.NewCoffeeSaleViewModel();
            DataContext = vm;
            comboAccount.ItemsSource = vm.Accounts;
            comboRecipient.ItemsSource = vm.Recipients;
            columnMix.ItemsSource = vm.Mixes;
        }
    }
}
