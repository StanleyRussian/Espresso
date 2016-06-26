using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewMonthlyExpense.xaml
    /// </summary>
    public partial class NewMonthlyExpense : Window
    {
        public NewMonthlyExpense()
        {
            InitializeComponent();
            DataContext = new ViewModels.Creational.NewMonthlyExpenseViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
