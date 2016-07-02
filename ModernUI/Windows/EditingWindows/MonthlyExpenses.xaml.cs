using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for MonthlyExpenses.xaml
    /// </summary>
    public partial class MonthlyExpenses
    {
        public MonthlyExpenses()
        {
            InitializeComponent();
            DataContext = new EditMonthlyExpensesViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewMonthlyExpense().ShowDialog();
        }
    }
}
