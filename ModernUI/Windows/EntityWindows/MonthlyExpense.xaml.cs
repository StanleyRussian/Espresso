using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewMonthlyExpense.xaml
    /// </summary>
    public partial class MonthlyExpense : MetroWindow
    {
        public MonthlyExpense(object argExpense = null)
        {
            InitializeComponent();
            DataContext = new MonthlyExpenseViewModel(argExpense);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
