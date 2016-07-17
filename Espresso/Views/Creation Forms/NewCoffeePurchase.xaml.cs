using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewCoffeePurchase.xaml
    /// </summary>
    public partial class NewCoffeePurchase : Window
    {
        public NewCoffeePurchase()
        {
            InitializeComponent();
            DataContext = new ViewModels.Creational.NewCoffeePurchaseViewModel();
        }
    }
}
