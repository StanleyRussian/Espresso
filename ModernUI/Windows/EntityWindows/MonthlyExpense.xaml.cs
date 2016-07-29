using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.Windows.EntityWindows;

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
            DataContext = new vmWinMonthlyExpense(argExpense);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
