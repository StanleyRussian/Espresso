using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewMonthlyExpense.xaml
    /// </summary>
    public partial class NewMonthlyExpense : MetroWindow
    {
        public NewMonthlyExpense()
        {
            InitializeComponent();
            DataContext = new NewMonthlyExpenseViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
