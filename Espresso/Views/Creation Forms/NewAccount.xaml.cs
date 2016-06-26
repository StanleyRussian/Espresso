using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Window
    {
        public NewAccount()
        {
            InitializeComponent();
            DataContext = new ViewModels.Creational.NewAccountViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
