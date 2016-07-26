using System.Windows.Controls;

namespace UI.Pages.Statistic.CoffeeSorts
{
    /// <summary>
    /// Interaction logic for Individual.xaml
    /// </summary>
    public partial class IndividualCoffeeSort : UserControl
    {
        public IndividualCoffeeSort()
        {
            InitializeComponent();
        }

        private void UnpaidPurchasesWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            new Windows.UnpaidPurchasesWindow().ShowDialog();
        }
    }
}
