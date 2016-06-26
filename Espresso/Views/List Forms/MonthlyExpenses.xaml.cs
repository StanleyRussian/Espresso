using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for MonthlyExpenses.xaml
    /// </summary>
    public partial class MonthlyExpenses : Window
    {
        public MonthlyExpenses()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.MonthlyExpensesViewModel();
        }
    }
}
