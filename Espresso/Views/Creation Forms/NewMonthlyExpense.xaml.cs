using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

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
            DataContext = new ViewModels.NewMonthlyExpenseViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
